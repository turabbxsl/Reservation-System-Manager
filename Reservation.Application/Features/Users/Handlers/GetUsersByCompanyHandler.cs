using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Reservation.Application.Features.Users.Queries;
using Reservation.Application.Features.Users.ViewModels;
using Reservation.Application.Interfaces;
using Reservation.Domain.Entities;
using Reservation.Shared.BaseResponse;
using System.Linq;

namespace Reservation.Application.Features.Users.Handlers
{
    public class GetUsersByCompanyHandler : IRequestHandler<GetUsersByCompanyIdQuery, ResponseDto<List<GetUsersByCompanyVM>>>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly UserManager<User> _userManager;

        public GetUsersByCompanyHandler(IUnitofWork unitofWork, UserManager<User> userManager)
        {
            _unitofWork = unitofWork;
            _userManager = userManager;
        }

        public async Task<ResponseDto<List<GetUsersByCompanyVM>>> Handle(GetUsersByCompanyIdQuery request, CancellationToken cancellationToken)
        {
            var userList = new List<GetUsersByCompanyVM>();

            try
            {
                var company = await _unitofWork.Companies.GetByIdAsync(request.CompanyId);
                if (company == null)
                    return ResponseDto<List<GetUsersByCompanyVM>>.ErrorResponse("Şirkət tapılmadı", 200);

                var allUsers = await _unitofWork.StaffMembers.FindAsync(x => x.CompanyId == company.Id);

                foreach (var user in allUsers)
                {
                    var userData = await _userManager.FindByIdAsync(user.Id.ToString());
                    if (userData == null) continue;

                    var staffMember = await _unitofWork.StaffMembers.GetByIdAsync(user.Id);

                    var services = await _unitofWork.StaffMembers.GetServicesByStaffMemberIdAsync(staffMember.Id);

                    var serviceList = services.Select(s => new Dictionary<Guid, string>
                                                                {
                                                                    { s.Id, s.Name }
                                                                }).ToList();            

                    var userItem = new GetUsersByCompanyVM()
                    {
                        Id = user.Id,
                        Fullname = user.FullName,
                        Email = userData.Email,
                        PhoneNumber = userData.PhoneNumber,
                        Username = userData.UserName,
                        CreatedDate = user.CreatedAt,
                        UpdateDate = user.UpdatedAt,
                        Services = serviceList
                        //Roles = roles?.Any() == true ? roles.ToList() : new List<string>()
                    };

                    userList.Add(userItem);
                }

                var result = userList;

                return ResponseDto<List<GetUsersByCompanyVM>>.SuccessResponse(result, 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<List<GetUsersByCompanyVM>>.ErrorResponse(new List<string>() { ex.InnerException?.Message }, "Gözlənilməz xəta baş verdi", 200);
            }

        }
    }
}
