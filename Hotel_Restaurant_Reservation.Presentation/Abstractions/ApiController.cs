using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Hotel_Restaurant_Reservation.Presentation.Abstractions;

[ApiController]
[Route("api/[controller]")]
public abstract class ApiController : ControllerBase
{
    private ISender _sender;

    protected ISender Sender
    {
        get
        {
            if(_sender == null)
            {
                _sender = HttpContext.RequestServices.GetService<ISender>();
            }

            return _sender;
        }
    }
}
