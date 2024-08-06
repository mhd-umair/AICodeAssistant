# AI Code Assistant

AI Code Assistant is a web application that uses OpenAI's Codex model to provide code suggestions. The application is built using ASP.NET Core, Blazor, Entity Framework Core, and Serilog for logging. 

## Table of Contents
- [Prerequisites](#prerequisites)
- [Setup](#setup)
- [Running the Application](#running-the-application)
- [Configuration](#configuration)
- [Technologies Used](#technologies-used)
- [Services](#services)
- [Middleware](#middleware)
- [Contributing](#contributing)
- [License](#license)

## Prerequisites

Before you begin, ensure you have the following installed on your machine:

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [Node.js and npm](https://nodejs.org/) (optional, for frontend tooling)

## Setup

1. **Clone the repository:**
   ```bash
   git clone https://github.com/yourusername/aicodeassistant.git
   cd aicodeassistant

2. **Configure the Database:**
				
	Update the connection string in appsettings.json:
	<br>
	Note: If you are using Local Instance of SQL Server, you can keep the connection string as is.
   ```json
	"ConnectionStrings": {
	  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=AICodeAssistantDB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
	}
 3. **Apply Migrations:**
	```bash
	dotnet ef database update

 4. **Add OpenAI API Key:**
		
	Obtain your OpenAI API key from [OpenAI] (https://www.openai.com/).
    
	Add the API key to your environment variables or update appsettings.json:
	```json
	"OpenAI": {
	  "ApiKey": "YOUR_OPENAI_API_KEY",
	  "BaseUri": "https://api.openai.com/v1/"
	}

## Running the Application

1. **Build and Run the Application:**
   ```bash
   dotnet build
   dotnet run
2. **Access the Application:**
				
	Open your browser and navigate to https://localhost:7087/codeinput to start using the application.
    You can always change the port of the application by making changes to applicationUrl in launchSettings.json file.

## Configuration

1. The appsettings.json file contains the configuration settings for the application, including connection strings and API keys.
   ```json
	   {
	  "ConnectionStrings": {
		"DefaultConnection": "Server=.;Database=AICodeAssistantDB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
	  },
	  "OpenAI": {
		"ApiKey": "",
		"BaseUri": "https://api.openai.com/v1/"
	  },
	  "AppSetting": {
		"SystemUri": "http://localhost:5195/"
	  },
	  "CodeCompletionService": {
		"Provider": "OpenAI" // Change this to "Mock" to use MockCodeCompletionService
	  },
	  "Logging": {
		"LogLevel": {
		  "Default": "Information",
		  "Microsoft": "Warning",
		  "Microsoft.Hosting.Lifetime": "Information"
		}
	  },
	  "AllowedHosts": "*"
	}

## Technologies Used
1. ASP.NET Core: The main framework used to build the web application.
2. Blazor: Used for building interactive web UIs with C#.
3. Entity Framework Core: Used for data access and database management.
4. Serilog: Used for logging application activities.
5. OpenAI Codex: Used for generating code suggestions based on user input.

## Services
1. ICodeCompletionService: Interface for code completion services.
2. OpenAIService: Implementation of code completion using OpenAI's API.
3. MockCodeCompletionService: Mock implementation for testing purposes.
4. CodeCompletionServiceFactory: Factory for creating instances of code completion services.

## Middleware
1. ExceptionHandlingMiddleware: Middleware for handling exceptions globally.
2. AntiforgeryTokenMiddleware: Middleware for handling anti-forgery tokens.

## Contributing

Contributions are welcome! Please fork the repository and submit pull requests.

1. Fork the repository.

2. Create a feature branch.

3. Commit your changes.

4. Push to the branch.

5. Create a new pull request.

## License

This project is licensed under the MIT License. See the LICENSE file for details.







