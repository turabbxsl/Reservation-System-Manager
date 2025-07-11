using Microsoft.AspNetCore.Mvc;
using Reservation.Shared.BaseResponse;

namespace Reservation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController:ControllerBase
    {
        public IActionResult CreateActionResultInstance<T>(ResponseDto<T> response)
        {
            if (response is null)
                return NotFound();

            if (response.StatusCode == 204)
                return NoContent();

            if (response.IsSuccess)
                return StatusCode(response.StatusCode, response);

            return StatusCode(response.StatusCode, response);
        }
    }
}
