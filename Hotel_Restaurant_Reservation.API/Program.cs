using FluentValidation;
using Hotel_Restaurant_Reservation.Application.Abstractions.JwtProvider;
using Hotel_Restaurant_Reservation.Application.Abstractions.PasswordHasher;
using Hotel_Restaurant_Reservation.Application.Abstractions.Payment;
using Hotel_Restaurant_Reservation.Application.Abstractions.Recommendation;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Application.Abstractions.Storage;
using Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.AddRestaurant;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Infrastructure;
using Hotel_Restaurant_Reservation.Infrastructure.Authentication;
using Hotel_Restaurant_Reservation.Infrastructure.PasswordHasher;
using Hotel_Restaurant_Reservation.Infrastructure.Payment;
using Hotel_Restaurant_Reservation.Infrastructure.Recommendations;
using Hotel_Restaurant_Reservation.Infrastructure.Repositories;
using Hotel_Restaurant_Reservation.Infrastructure.Storage;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddAutoMapper(typeof(Hotel_Restaurant_Reservation.Application.AssemplyReference).Assembly);

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Hotel_Restaurant_Reservation.Presentation.AssemplyReference).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(Hotel_Restaurant_Reservation.Application.AssemplyReference).Assembly);
});


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        // These values must EXACTLY MATCH the ones used to create the token
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"], // e.g., "https://your-api.com"

        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"], // e.g., "https://your-app.com"

        ValidateLifetime = true, // Checks for token expiration

        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});


var myAllowSpecificOrigins = "AllowAll";

// Add the CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy(myAllowSpecificOrigins,
        builder =>
        {
            builder.WithOrigins(
                "http://localhost:3000",
                "http://localhost:8080",
                "http://192.168.137.1:8080"
                ).AllowAnyHeader().AllowAnyMethod();
        });
});

// Add services to the container.
builder.Services.AddHttpClient();

builder.Services.AddControllers()
    .AddApplicationPart(typeof(Hotel_Restaurant_Reservation.Presentation.AssemplyReference).Assembly);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<HotelRestaurantDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("HotelRestaurantReservation"));
});

// Corrected and cleaned up service registrations
builder.Services.AddScoped<IHotelRepository, HotelRepository>();
builder.Services.AddScoped<IGenericRepository<Hotel>, GenericRepository<Hotel>>();
builder.Services.AddScoped<IGenericRepository<Restaurant>, GenericRepository<Restaurant>>();
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
builder.Services.AddScoped<IGenericRepository<User>, GenericRepository<User>>();
builder.Services.AddScoped<IGenericRepository<BookingDish>, GenericRepository<BookingDish>>();
builder.Services.AddScoped<IGenericRepository<RestaurantBooking>, GenericRepository<RestaurantBooking>>();
builder.Services.AddScoped<IGenericRepository<RestaurantReview>, GenericRepository<RestaurantReview>>();
builder.Services.AddScoped<IGenericRepository<RestaurantCuisine>, GenericRepository<RestaurantCuisine>>();
builder.Services.AddScoped<IGenericRepository<CurrencyType>, GenericRepository<CurrencyType>>();
builder.Services.AddScoped<IGenericRepository<RestaurantCurrencyType>, GenericRepository<RestaurantCurrencyType>>();
builder.Services.AddScoped<IGenericRepository<RestaurantDish>, GenericRepository<RestaurantDish>>();
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
builder.Services.AddValidatorsFromAssemblyContaining<AddRestaurantValidator>();
builder.Services.AddScoped<IGenericRepository<HotelReview>, GenericRepository<HotelReview>>();
builder.Services.AddScoped<IGenericRepository<EventReview>, GenericRepository<EventReview>>();
builder.Services.AddScoped<IGenericRepository<Role>, GenericRepository<Role>>();
builder.Services.AddScoped<IGenericRepository<HotelAmenityPrice>, GenericRepository<HotelAmenityPrice>>();
builder.Services.AddScoped<IGenericRepository<RoomImage>, GenericRepository<RoomImage>>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

builder.Services.AddScoped<IGenericRepository<UserImage>, GenericRepository<UserImage>>();
builder.Services.AddScoped<IGenericRepository<RestaurantImage>, GenericRepository<RestaurantImage>>();
builder.Services.AddScoped<IGenericRepository<HotelImage>, GenericRepository<HotelImage>>();
builder.Services.AddScoped<IGenericRepository<EventImage>, GenericRepository<EventImage>>();
builder.Services.AddScoped<IGenericRepository<HotelReservationPayment>, GenericRepository<HotelReservationPayment>>();
builder.Services.AddScoped<IGenericRepository<RestaurantBookingPayment>, GenericRepository<RestaurantBookingPayment>>();
builder.Services.AddScoped<IGenericRepository<EventRegistrationPayment>, GenericRepository<EventRegistrationPayment>>();
builder.Services.AddScoped<IGenericRepository<RoomAmenity>, GenericRepository<RoomAmenity>>();
builder.Services.AddScoped<IFileStorageService, FileStorageService>();
builder.Services.AddScoped<IPayPalService, PayPalService>();
builder.Services.AddScoped<IRecommendationService, RecommendationService>();

builder.Services.AddValidatorsFromAssemblyContaining<AddRestaurantValidator>();


builder.Services.AddScoped<IJwtProvider, JwtProvider>();
//builder.Services.AddScoped<DataSeeder>();
builder.Services.AddScoped<IPayPalService, PayPalService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    //var seeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();
    var dbContext = scope.ServiceProvider.GetRequiredService<HotelRestaurantDbContext>();
    await dbContext.Database.MigrateAsync();
    //await seeder.SeedAsync();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(myAllowSpecificOrigins);

app.UseStaticFiles(); // Serves files from wwwroot
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
           Path.Combine(builder.Environment.ContentRootPath, "wwwroot/images")),
    RequestPath = "/images"
});


app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();