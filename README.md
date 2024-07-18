# JobCandidateHub

## Project Overview

JobCandidateHub is a .NET 8 web API application designed to manage job candidates. The application adheres to clean architecture principles and incorporates various technologies and best practices, including Entity Framework Core, dependency injection, caching, custom validation, and logging with Serilog.

### API Link

You can access the API here: [JobCandidateHub API](https://jobcandidatehub-api.azurewebsites.net/swagger)

## Project Structure

1. **JobCandidateHub.WebAPI**: Contains the web API controllers.
2. **JobCandidateHub.Models**: Contains the view models.
3. **JobCandidateHub.DataAccess**: Contains the data access layer, including the database context, entities, and repositories.
4. **JobCandidateHub.Services**: Contains the business logic and services.

## Features

- **Add and Update Candidate Information**: Endpoints to upsert candidate details.
- **Caching**: Memory caching implemented for efficient data retrieval.
- **Custom Validation**: Custom model validation attributes.
- **Logging**: Integrated logging with Serilog for monitoring and debugging.
- **Database Migrations**: Automated database migrations using Entity Framework Core.
- **Dependency Injection**: Configured DI for service and repository layers.
- **CI/CD**: Build and deploy the API to Azure App Service.

## Recommendations

1. **JWT Authentication**: Implement JWT authentication to secure the API endpoints.
   - Configure JWT Bearer authentication.
   - Protect sensitive endpoints to ensure only authorized users can access them.

2. **Enhanced Caching**: Improve the caching strategy to optimize performance.
   - Cache the list of candidates and retrieve from cache to handle frequent read operations efficiently.
   - Implement distributed caching using Redis for a scalable caching solution.

4. **Additional Recommendations**:
   - Implement rate limiting to prevent abuse of the API.
   - Add more unit and integration tests to ensure code quality and reliability.
   - Utilize Azure Key Vault to manage sensitive configuration settings securely.


### Time Spent

Total time spent on the project: **7 hours**

