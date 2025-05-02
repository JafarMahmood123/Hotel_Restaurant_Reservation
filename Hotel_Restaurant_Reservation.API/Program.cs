using Hotel_Restaurant_Reservation.API.OptionsSetup;
using Hotel_Restaurant_Reservation.Application.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Infrastructure;
using Hotel_Restaurant_Reservation.Infrastructure.Authentication;
using Hotel_Restaurant_Reservation.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddAutoMapper(typeof(Hotel_Restaurant_Reservation.Presentation.AssemplyReference).Assembly);

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Hotel_Restaurant_Reservation.Presentation.AssemplyReference).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(Hotel_Restaurant_Reservation.Application.AssemplyReference).Assembly);
});

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();

builder.Services.ConfigureOptions<JwtOptionsSetup>();

builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();

builder.Services.AddDbContext<HotelRestaurantDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("HotelRestaurantReservation"));
});


builder.Services.AddScoped<IGenericRepository<Hotel>, GenericRepository<Hotel>>();
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
builder.Services.AddScoped<IGenericRepository<RestaurantBooking>,  GenericRepository<RestaurantBooking>>();
builder.Services.AddScoped<IGenericRepository<Role>,  GenericRepository<Role>>();
builder.Services.AddScoped<IGenericRepository<CustomerRoles>,  GenericRepository<CustomerRoles>>();
builder.Services.AddScoped<IRestaurantRespository, RestaurantRepository>();

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
