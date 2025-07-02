# Stage 1: Build the application
# Use the .NET 9 SDK image as the build environment
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /source


# Copy solution and project files and restore dependencies first
# This leverages Docker's layer caching. Dependencies are only re-downloaded if project files change.
COPY *.sln .
COPY Race.Web/*.csproj ./Race.Web/
COPY Race.Service/*.csproj ./Race.Service/
COPY Race.Repo/*.csproj ./Race.Repo/
COPY Race.Model/*.csproj ./Race.Model/
COPY Race.Shared/*.csproj ./Race.Shared/
RUN dotnet restore

# Copy the rest of the source code
COPY . .

# Publish the application, creating release-ready artifacts
RUN dotnet publish -c Release -o /app/publish Race.Web/Race.Web.csproj

# Stage 2: Create the final runtime image
# Use the smaller and more secure ASP.NET runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
ENV ASPNETCORE_ENVIRONMENT=Production

# Install curl for healthcheck
RUN apt-get update && apt-get install -y curl

# Expose the port the app will run on
# Expose port 80 for HTTP traffic
EXPOSE 8080
# Epose port 443 if you plan to use HTTPS
#EXPOSE 443
HEALTHCHECK --interval=30s --timeout=5s --start-period=10s --retries=3 \
  CMD curl --fail http://localhost:8080/healthz || exit 1

  # Set the user to a non-root user for security
USER app

# Copy the published output from the build stage
COPY --from=build /app/publish .

# Define the entry point for the container
ENTRYPOINT ["dotnet", "Race.Web.dll"]
