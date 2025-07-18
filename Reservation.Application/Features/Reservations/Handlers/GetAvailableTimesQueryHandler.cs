using MediatR;
using Reservation.Application.Features.Reservations.Queries;
using Reservation.Application.Interfaces;
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

            var company = await _unitofWork.Companies.GetByIdAsync(request.CompanyId);
            if (company == null)
                return ResponseDto<Dictionary<string, bool>>.ErrorResponse("Şirkət tapılmadı", 200);

            var service = await _unitofWork.Services.GetByIdAsync(request.ServiceId);
            if (service == null || service.CompanyId != company.Id)
                return ResponseDto<Dictionary<string, bool>>.ErrorResponse("Xidmət tapılmadı və ya şirkətə aid deyil", 200);

            TimeSpan start = TimeSpan.Parse(company.StartTime);
            TimeSpan end = TimeSpan.Parse(company.EndTime);

            var interval = new TimeSpan(0, 30, 0);

            var workingHours = new List<TimeSpan>();
            for (var time = start; time < end; time += interval)
            {
                workingHours.Add(time);
            }

            // Şirkətdə bu xidməti görən neçə işçi var
            int employeeCount = await _unitofWork.StaffMembers.CountByService(request.CompanyId,request.ServiceId);

            foreach (var time in workingHours)
            {
                var dateTime = request.Date.Date + time;

                // Həmin vaxt üçün neçə rezervasiya var
                int reservationCount = await _unitofWork.Reservations.ExistAsync(company.Id, request.ServiceId, dateTime);

                // Əgər rezervasiya sayı işçi sayından azdırsa - yəni hələ boş yer var
                var isAvailable = reservationCount < employeeCount;
                times[time.ToString(@"hh\:mm")] = isAvailable;
            }

            return ResponseDto<Dictionary<string, bool>>.SuccessResponse(times, 200);
        }


    }
}
