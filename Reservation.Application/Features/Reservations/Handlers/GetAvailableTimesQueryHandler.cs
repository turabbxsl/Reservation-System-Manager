using MediatR;
using Microsoft.EntityFrameworkCore;
using Reservation.Application.Features.Reservations.Queries;
using Reservation.Application.Interfaces;
using Reservation.Domain.Entities;
using Reservation.Shared.BaseResponse;

namespace Reservation.Application.Features.Reservations.Handlers
{
    public class GetAvailableTimesQueryHandler : IRequestHandler<GetAvailableReservationTimesQuery, ResponseDto<Dictionary<string, bool>>>
    {
        private readonly IUnitofWork _unitofWork;

        public GetAvailableTimesQueryHandler(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }


        public async Task<ResponseDto<Dictionary<string, bool>>> Handle(GetAvailableReservationTimesQuery request, CancellationToken cancellationToken)
        {
            var times = new Dictionary<string, bool>();

            try
            {
                var company = await _unitofWork.Companies.GetByIdAsync(request.CompanyId);
                if (company == null)
                    return ResponseDto<Dictionary<string, bool>>.ErrorResponse("Şirkət tapılmadı", 200);

                var spec = await _unitofWork.CompanySpecialities.FindAsync(x => x.SpecialtyId == request.SpecialtyId);
                var dbSpecList = spec.FirstOrDefault();
                if (spec == null || dbSpecList.CompanyId != company.Id)
                    return ResponseDto<Dictionary<string, bool>>.ErrorResponse("Xidmət kateqoriyasi tapılmadı və ya şirkətə aid deyil", 200);

                // Xidmetin sirkete aid olub olmadigini COmpanyService cedvelinden yoxlayiriq
                var services = await _unitofWork.CompaniesService.FindAsync(x => x.CompanyId == request.CompanyId);
                foreach (var serv in services)
                {
                    bool isServiceInCompany = await _unitofWork.CompaniesService.ExistsAsync(request.CompanyId, serv.ServiceId);
                    if (!isServiceInCompany)
                        return ResponseDto<Dictionary<string, bool>>.ErrorResponse("Xidmət bu şirkətə aid deyil", 200);
                }


                TimeSpan start = TimeSpan.Parse(company.StartTime);
                TimeSpan end = TimeSpan.Parse(company.EndTime);

                var interval = new TimeSpan(0, 30, 0);

                var workingHours = new List<TimeSpan>();
                for (var time = start; time < end; time += interval)
                {
                    workingHours.Add(time);
                }

                // Sirketde bu xidmet kateqoriyasini goren nece isci var
                int employeeCount = await _unitofWork.StaffMembers.CountBySpeciality(request.CompanyId, request.SpecialtyId);

                var db = await _unitofWork.CompanySpecialities
                                          .FindWithIncludeAsync(
                            cs => cs.SpecialtyId == request.SpecialtyId && cs.CompanyId == request.CompanyId,
                            query => query.Include(s => s.Specialty)
                                            .ThenInclude(z => z.Services)
                                            .ThenInclude(b => b.CompanyServices)
                                          );
                var totalSpecDuration = db?.ToList()?
                                           .SelectMany(cs => cs.Specialty.Services
                                                                          .Where(s => s.CompanyServices.Any(cserv => cserv.CompanyId == request.CompanyId)))
                                           .Sum(s => s.DurationInMinutes);

                var currentTime = start;

                while(currentTime /*+ TimeSpan.FromMinutes(Convert.ToDouble(totalSpecDuration))*/ <= end)
                {
                    var endTime = currentTime + TimeSpan.FromMinutes(Convert.ToDouble(totalSpecDuration));
                    var endDateTime = request.Date.Date + endTime;
                    var reservDateTime = request.Date.Date + currentTime;

                    // Hemin vaxt ucun rezervasiya var mi
                    int reservationCount = await _unitofWork.Reservations
    .ExistAsync(company.Id, request.SpecialtyId, reservDateTime);

                    var dateTime = request.Date.Date + currentTime;
                    var isAvailable = false;

                    // eger bu xidmetin bitme vaxti isin bitme saatini kecirse.loop dayandir
                    if (endTime > end)
                    {
                        if (employeeCount == 0) // İşçi təyin edilməyibse
                            isAvailable = false;
                        else if (reservationCount > 0)
                            isAvailable = false;
                        else  // Eger rezervasiya sayi isci sayindan azdirsa - yeni hele bos yer var
                            isAvailable = reservationCount < employeeCount;

                        times[currentTime.ToString(@"hh\:mm")] = isAvailable;
                        break;
                    }                

                    if (employeeCount == 0) // İşçi təyin edilməyibse
                      isAvailable = false;
                    else if (reservationCount > 0 )
                        isAvailable = false;
                    else  // Eger rezervasiya sayi isci sayindan azdirsa - yeni hele bos yer var
                        isAvailable = reservationCount <= employeeCount;

                    times[currentTime.ToString(@"hh\:mm")] = isAvailable;

                    currentTime = currentTime.Add(TimeSpan.FromMinutes(Convert.ToDouble(totalSpecDuration)));
                }

                return ResponseDto<Dictionary<string, bool>>.SuccessResponse(times, 200);
            }
            catch (Exception)
            {
                return ResponseDto<Dictionary<string, bool>>.ErrorResponse(new List<string>() { $"Rezervasiya yaradılmadı" }, 200);
            }
        }


    }
}
