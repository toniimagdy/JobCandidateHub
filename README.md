# JobCandidateHub

## Project Overview

This project is a .NET 8 web API application designed to manage job candidates. The application follows clean architecture principles and includes features for adding and updating candidate information. The project demonstrates the use of various technologies and best practices including Entity Framework Core, dependency injection, caching, custom validation, and logging with Serilog.

### API Link

You can access the API here: [JobCandidateHub API](https://jobcandidatehub-api.azurewebsites.net/)

## Project Structure

1. **JobCandidateHub.WebAPI**: Contains the web API controllers.
2. **JobCandidateHub.Models**: Contains the view models.
3. **JobCandidateHub.DataAccess**: Contains the data access layer including the database context, entities, and repositories.
4. **JobCandidateHub.Services**: Contains the business logic and services.

## Features

- **Add and Update Candidate Information**: Endpoints to upsert candidate details.
- **Caching**: Memory caching implemented for efficient data retrieval.
- **Custom Validation**: Custom model validation attributes.
- **Logging**: Integrated logging with Serilog for monitoring and debugging.
- **Database Migrations**: Automated database migrations using Entity Framework Core.
- **Dependency Injection**: Configured DI for service and repository layers.
- **CI/CD**: Build and Deploy the API to Azure App Service.


  ##### For caching I used MemoryCaching to cache the inserted/updated candidate for 5 minutes to handle multiple updates within this time range, another way is to cache the candidates list and search within this list.


