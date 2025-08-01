﻿namespace Hotel_Restaurant_Reservation.Application.Implementation.Users.Commands.SignUp;

public class SignUpRequest
{

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public DateOnly BirthDate { get; set; }

    public Guid LocationId { get; set; }

    public Guid? RoleId { get; set; }
}