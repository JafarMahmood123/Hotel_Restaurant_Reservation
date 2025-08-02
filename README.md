Hotel & Restaurant Reservation System
Description
This project is an integrated system for booking hotel rooms and restaurant tables, designed to provide a seamless experience for travelers. Unlike many platforms that specialize in either hotel or restaurant bookings, this application combines both into a single, user-friendly interface. It also features a recommendation system to help users discover new places.

Features
User Management:

User Signup and Login

Admin and User Roles

Hotel Management (for Admins):

Add, update, and delete hotels

Add, update, and delete room types

Manage hotel images

Hotel Booking (for Users):

Search for hotels by city and date

View hotel details and available rooms

Book hotel rooms

Restaurant Management (for Admins):

Add, update, and delete restaurants

Manage restaurant images

Restaurant Reservations (for Users):

Search for restaurants by city

View restaurant details

Reserve a table

Recommendation System:

Get personalized restaurant recommendations.

Technologies Used
.NET 8

ASP.NET Core Web API

Entity Framework Core

C#

SQL Server

Architecture
The project is built using Clean Architecture principles, following the Command Query Responsibility Segregation (CQRS) pattern. This ensures a clear separation of concerns, making the application scalable, maintainable, and testable.

Project Structure
The solution is organized into the following layers:

Domain: Contains the core business logic and entities of the application.

Application: Implements the application logic, including CQRS commands and queries.

Infrastructure: Handles external concerns such as database access (using Entity Framework Core), file storage, and other third-party services.

Presentation: Contains the API controllers, which expose the application's functionality via a RESTful API.

API: The main entry point of the application, which ties all the other layers together.

Getting Started
Follow these instructions to get the project up and running on your local machine.

Prerequisites
.NET 8 SDK

SQL Server

Installation
Clone the repository:

Bash

git clone https://github.com/JafarMahmood123/Hotel_Restaurant_Reservation.git
Configure the database connection:

Open the appsettings.json file in the Hotel_Restaurant_Reservation.API project.

Modify the ConnectionString to point to your local SQL Server instance.

Apply database migrations:

Open a terminal or command prompt.

Navigate to the Hotel_Restaurant_Reservation.Infrastructure directory.

Run the following command to create the database schema:

Bash

dotnet ef database update
Run the application:

In your IDE (e.g., Visual Studio, Rider) or from the command line, run the Hotel_Restaurant_Reservation.API project.

API Endpoint Highlight
A key feature of this application is the recommendation system. You can get personalized restaurant recommendations by making a GET request to the following endpoint:

GET /api/RestaurantRecommendation/{userId}

This will call the recommendation system API and return a list of recommended restaurants for the specified user.

Contributing
Contributions are welcome! If you'd like to contribute to the project, please follow these steps:

Fork the repository.

Create a new branch for your feature or bug fix.

Make your changes.

Submit a pull request.
