using FluentValidation;
using Hotel_Restaurant_Reservation.API.OptionsSetup;
using Hotel_Restaurant_Reservation.Application.Abstractions;
using Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.AddRestaurant;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Infrastructure;
using Hotel_Restaurant_Reservation.Infrastructure.Authentication;
using Hotel_Restaurant_Reservation.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddAutoMapper(typeof(Hotel_Restaurant_Reservation.Application.AssemplyReference).Assembly);

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Hotel_Restaurant_Reservation.Presentation.AssemplyReference).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(Hotel_Restaurant_Reservation.Application.AssemplyReference).Assembly);
});

// Add services to the container.

builder.Services.AddControllers()
    .AddApplicationPart(typeof(Hotel_Restaurant_Reservation.Presentation.AssemplyReference).Assembly);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();

builder.Services.ConfigureOptions<JwtOptionsSetup>();

builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();

builder.Services.AddDbContext<HotelRestaurantDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("HotelRestaurantReservation"));
});


builder.Services.AddScoped<IHotelRepository, HotelRepository>();
builder.Services.AddScoped<IGenericRepository<Restaurant>, GenericRepository<Restaurant>>();
builder.Services.AddScoped<IGenericRepository<WorkTime>, GenericRepository<WorkTime>>();
builder.Services.AddScoped<IGenericRepository<MealType>, GenericRepository<MealType>>();
builder.Services.AddScoped<IGenericRepository<Feature>, GenericRepository<Feature>>();
builder.Services.AddScoped<IGenericRepository<Location>, GenericRepository<Location>>();
builder.Services.AddScoped<IGenericRepository<Tag>, GenericRepository<Tag>>();
builder.Services.AddScoped<IGenericRepository<City>, GenericRepository<City>>();
builder.Services.AddScoped<IGenericRepository<Country>, GenericRepository<Country>>();
builder.Services.AddScoped<IGenericRepository<LocalLocation>, GenericRepository<LocalLocation>>();
builder.Services.AddScoped<IGenericRepository<CityLocalLocations>, GenericRepository<CityLocalLocations>>();
builder.Services.AddScoped<IGenericRepository<Cuisine>, GenericRepository<Cuisine>>();
builder.Services.AddScoped<IGenericRepository<Dish>, GenericRepository<Dish>>();
builder.Services.AddScoped<IGenericRepository<Customer>, GenericRepository<Customer>>();
builder.Services.AddScoped<IGenericRepository<BookingDish>, GenericRepository<BookingDish>>();
builder.Services.AddScoped<IGenericRepository<RestaurantBooking>, GenericRepository<RestaurantBooking>>();
builder.Services.AddScoped<IGenericRepository<RestaurantReview>, GenericRepository<RestaurantReview>>();
builder.Services.AddScoped<IGenericRepository<Cuisine>, GenericRepository<Cuisine>>();
builder.Services.AddScoped<IGenericRepository<RestaurantCuisine>, GenericRepository<RestaurantCuisine>>();
builder.Services.AddScoped<IGenericRepository<CurrencyType>, GenericRepository<CurrencyType>>();
builder.Services.AddScoped<IGenericRepository<RestaurantCurrencyType>, GenericRepository<RestaurantCurrencyType>>();
builder.Services.AddScoped<IGenericRepository<RestaurantDishPrice>, GenericRepository<RestaurantDishPrice>>();
builder.Services.AddScoped<IGenericRepository<RestaurantFeature>, GenericRepository<RestaurantFeature>>();
builder.Services.AddScoped<IGenericRepository<RestaurantMealType>, GenericRepository<RestaurantMealType>>();
builder.Services.AddScoped<IGenericRepository<RestaurantTag>, GenericRepository<RestaurantTag>>();
builder.Services.AddScoped<IGenericRepository<RestaurantWorkTime>, GenericRepository<RestaurantWorkTime>>();
builder.Services.AddScoped<IRestaurantRespository, RestaurantRepository>();
builder.Services.AddScoped<IGenericRepository<Room>, GenericRepository<Room>>();
builder.Services.AddScoped<IGenericRepository<RoomType>, GenericRepository<RoomType>>();
builder.Services.AddScoped<IGenericRepository<Amenity>, GenericRepository<Amenity>>();
builder.Services.AddScoped<IGenericRepository<Event>, GenericRepository<Event>>();
builder.Services.AddScoped<IGenericRepository<EventRegistration>, GenericRepository<EventRegistration>>();
builder.Services.AddScoped<IGenericRepository<HotelReservation>, GenericRepository<HotelReservation>>();
builder.Services.AddScoped<IGenericRepository<PropertyType>, GenericRepository<PropertyType>>();
builder.Services.AddScoped<IHotelRepository, HotelRepository>();
builder.Services.AddValidatorsFromAssemblyContaining<AddRestaurantValidator>();
builder.Services.AddScoped<IGenericRepository<EventRegistration>, GenericRepository<EventRegistration>>();
builder.Services.AddScoped<IGenericRepository<HotelReview>, GenericRepository<HotelReview>>();
builder.Services.AddScoped<IGenericRepository<HotelReservation>, GenericRepository<HotelReservation>>();

builder.Services.AddScoped<IJwtProvider, JwtProvider>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
