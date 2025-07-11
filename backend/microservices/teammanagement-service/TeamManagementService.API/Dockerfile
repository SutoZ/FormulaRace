# Stage 1: Build the application
# Use the .NET 9 SDK image, as your project targets .NET 9
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy all .csproj files and restore dependencies first to leverage Docker layer caching
COPY ["TeamManagementService.API/TeamManagementService.API.csproj", "TeamManagementService.API/"]
COPY ["TeamManagementService.Application/TeamManagementService.Application.csproj", "TeamManagementService.Application/"]
COPY ["TeamManagementService.Domain/TeamManagementService.Domain.csproj", "TeamManagementService.Domain/"]
COPY ["TeamManagementService.Infrastructure/TeamManagementService.Infrastructure.csproj", "TeamManagementService.Infrastructure/"]
# If you have other projects (like shared libraries), copy them here as well
# COPY ["shared/Race.Shared/Race.Shared.Utilities/Race.Shared.Utilities.csproj", "shared/Race.Shared/Race.Shared.Utilities/"]

RUN dotnet restore "TeamManagementService.API/TeamManagementService.API.csproj"

# Copy the rest of the source code
COPY . .
WORKDIR "/src/TeamManagementService.API"

# Build the project in Release configuration
RUN dotnet build "TeamManagementService.API.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "TeamManagementService.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 2: Create the final runtime image
# Use the smaller ASP.NET runtime image for the final stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Expose port 8080 for the application to listen on
EXPOSE 8080

# Define the entry point for the container
ENTRYPOINT ["dotnet", "TeamManagementService.API.dll"]