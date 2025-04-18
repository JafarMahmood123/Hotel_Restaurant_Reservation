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

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Hotel_Restaurant_Reservation.Presentation.AssemplyReference).Assembly);
});

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<HotelRestaurantDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("HotelRestaurantReservation"));
});

builder.Services.AddScoped<IRequestHandler<GetHotelByIdQuery, Hotel?>, GetHotelByIdQueryHandler>();
builder.Services.AddScoped<IRequestHandler<GetAllHotelsQuery, IEnumerable<Hotel>?>, GetAllHotelsQueryHandler>();
builder.Services.AddScoped<IGenericRepository<Hotel>, GenericRepository<Hotel>>();

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
