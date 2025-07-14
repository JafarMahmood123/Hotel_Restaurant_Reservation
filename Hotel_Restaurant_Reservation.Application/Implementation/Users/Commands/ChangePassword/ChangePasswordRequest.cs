namespace Hotel_Restaurant_Reservation.Application.Implementation.Users.Commands.ChangePassword;

public class ChangePasswordRequest
{
    public string Email { get; set; }
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
}