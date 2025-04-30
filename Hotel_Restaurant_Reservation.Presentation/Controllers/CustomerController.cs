using AutoMapper;
using Hotel_Restaurant_Reservation.Application.DTOs.CustomerDTOs;
using Hotel_Restaurant_Reservation.Application.Implementation.Customers.Queries.LogIn;
using Hotel_Restaurant_Reservation.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Restaurant_Reservation.Presentation.Controllers;

public class CustomerController : ApiController
{
    private readonly IMapper mapper;

    public CustomerController(ISender sender, IMapper mapper) : base(sender)
    {
        this.mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> LogIn(string email, string password, CancellationToken cancellationToken)
    {
        var query = new LogInQuery(email, password);

        var customer = await Sender.Send(query, cancellationToken);

        if(customer is not null)
        {
            var customerResponse = mapper.Map<CustomerResponse>(customer);

            return Ok(customerResponse);
        }

        return NotFound();
    }
}
