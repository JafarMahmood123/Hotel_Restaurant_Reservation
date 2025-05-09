﻿using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Cities.Commands.AddCity;

public class AddCityCommand : ICommand<City>
{
    public City City { get; set; }
    public Guid CountryId { get; }

    public AddCityCommand(City city, Guid countryId)
    {
        City = city;
        CountryId = countryId;
    }
}
