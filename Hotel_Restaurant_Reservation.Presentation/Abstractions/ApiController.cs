using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Restaurant_Reservation.Presentation.Abstractions;

[ApiController]
[Route("[controller]")]
public abstract class ApiController : ControllerBase
{
    private ISender _sender;

    protected ApiController(ISender sender)
    {
        _sender = sender;
    }
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
