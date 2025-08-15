using MediatR;
using Reservation.Application.Features.Services.Commands;
using Reservation.Application.Interfaces;
using Reservation.Shared.BaseResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Application.Features.Services.Handlers
{
    public class UpdateStatusServiceCommandHandler : IRequestHandler<UpdateStatusServiceCommand, ResponseDto<bool>>
    {
        private readonly IUnitofWork _unitOfWork;

        public UpdateStatusServiceCommandHandler(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<ResponseDto<bool>> Handle(UpdateStatusServiceCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var service = await _unitOfWork.Services.GetByIdAsync(request.serviceId);
                if (service == null)
                    return ResponseDto<bool>.ErrorResponse(new List<string>() { "Xidmət tapılmadı" }, 200);

               

                await _unitOfWork.SaveChangesAsync();

                return ResponseDto<bool>.SuccessResponse(true, 200);
            }
            catch (Exception)
            {
                return ResponseDto<bool>.ErrorResponse(new List<string>() { "Xidmət kateqoriya yenilənmədi" }, 200);
            }
        }
    }
}
