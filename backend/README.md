# Todo REST API

This project is a RESTful API for managing Todo items built using C# and .NET Core. It provides endpoints for creating, retrieving, updating, and deleting Todo items.

## Project Structure

- **Controllers**: Contains the API controllers.
  - `TodoController.cs`: Defines the API endpoints for Todo items.
  
- **Models**: Contains the data models.
  - `TodoItem.cs`: Represents the Todo item data structure.
  
- **Data**: Contains the database context.
  - `TodoContext.cs`: Manages database interactions for Todo items.
  
- **Services**: Contains the business logic.
  - `TodoService.cs`: Implements methods for managing Todo items.
  
- **Tests**: Contains unit tests for the service layer.
  - `TodoServiceTests.cs`: Tests the functionality of the TodoService.
  
- **Program.cs**: Entry point of the application.
  
- **Startup.cs**: Configures services and the request pipeline.
  
- **appsettings.json**: Configuration settings for the application.
  
- **appsettings.Development.json**: Development-specific configuration settings.
  
- **backend.csproj**: Project file containing dependencies and settings.

## Setup Instructions

1. **Clone the Repository**
   ```bash
   git clone <repository-url>
   cd backend
   ```

2. **Install Dependencies**
   Ensure you have the .NET SDK installed. Run the following command to restore dependencies:
   ```bash
   dotnet restore
   ```

3. **Configure Database**
   Update the connection string in `appsettings.json` to point to your SQL Server or MySQL database.

4. **Run Migrations**
   If using Entity Framework, run the following command to apply migrations:
   ```bash
   dotnet ef database update
   ```

5. **Run the Application**
   Start the application using:
   ```bash
   dotnet run
   ```

6. **Access the API**
   The API will be available at `http://localhost:5000` (or the configured port). You can use tools like Postman or curl to interact with the endpoints.

## API Endpoints

- **GET /api/todo**: Retrieve all Todo items.
- **POST /api/todo**: Create a new Todo item.
- **PUT /api/todo/{id}**: Update an existing Todo item.
- **PATCH /api/todo/{id}**: Partially update an existing Todo item.
- **DELETE /api/todo/{id}**: Delete a Todo item.

## Testing

Run the unit tests using:
```bash
dotnet test
```

## Logging and Error Handling

The application includes proper error handling and logging mechanisms to ensure smooth operation and easy debugging.

## License

This project is licensed under the MIT License.