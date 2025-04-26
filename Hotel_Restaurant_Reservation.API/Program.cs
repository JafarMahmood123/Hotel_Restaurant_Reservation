using Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Queries.GetAllHotels;
using Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Queries.GetHotelById;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Infrastructure;
using Hotel_Restaurant_Reservation.Infrastructure.Repositories;
using MediatR;
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

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
builder.Services.AddScoped<IGenericRepository<Cuisine>, GenericRepository<Cuisine>>();
builder.Services.AddScoped<IGenericRepository<Dish>, GenericRepository<Dish>>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
