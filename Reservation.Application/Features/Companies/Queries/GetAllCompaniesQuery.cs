using MediatR;
using Reservation.Application.Features.Companies.ViewModels;

namespace Reservation.Application.Features.Companies.Query
{
    public record GetAllCompaniesQuery:IRequest<Shared.BaseResponse.ResponseDto<List<CompanyVM>>>;
}