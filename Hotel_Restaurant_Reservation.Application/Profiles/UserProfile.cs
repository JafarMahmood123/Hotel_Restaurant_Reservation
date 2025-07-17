using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Implementation.Users.Commands.SignUp;
using Hotel_Restaurant_Reservation.Application.Implementation.Users.Commands.UpdateCustomer;
using Hotel_Restaurant_Reservation.Application.Implementation.Users.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Presentation.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserResponse>()
            .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.Name)); 

        CreateMap<SignUpRequest, User>();
        CreateMap<UpdateUserRequest, User>();
    }
}