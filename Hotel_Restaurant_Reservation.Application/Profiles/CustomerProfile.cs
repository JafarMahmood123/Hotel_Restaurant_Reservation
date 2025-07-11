﻿using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Implementation.Customers.Commands.SignUp;
using Hotel_Restaurant_Reservation.Application.Implementation.Customers.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Presentation.Profiles;

public class CustomerProfile : Profile
{
    public CustomerProfile()
    {
        CreateMap<Customer, CustomerResponse>().ReverseMap();

        CreateMap<SignUpRequest, Customer>();
    }
}
