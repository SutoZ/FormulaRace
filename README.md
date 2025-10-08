# FormulaRace

> A full-stack web application for managing Formula 1 pilots and teams, built with a modern, containerized microservice architecture.

## Table of Contents
* [About The Project](#about-the-project)
* [Project Structure](#project-structure)
* [Technologies Used](#technologies-used)
* [Getting Started](#getting-started)
  * [Prerequisites](#prerequisites)
  * [Configuration](#configuration)
  * [Running the Application](#running-the-application)
* [Features](#features)
* [Status](#status)
* [Contact](#contact)

## About The Project

FormulaRace is a comprehensive web application that provides CRUD (Create, Read, Update, Delete) functionality for Formula 1 pilots and their respective teams. The backend is a robust ASP.NET Core Web API, and the frontend is a dynamic single-page application built with Angular.

The entire solution is containerized using Docker and orchestrated with Docker Compose, making setup and deployment streamlined and consistent across different environments.

## Project Structure

The solution is organized into distinct parts to maintain a clean separation of concerns:

```
/
├── backend/
│   └── microservices/
│       └── teammanagement-service/   # .NET API for managing teams and pilots
├── frontend/                         # Angular SPA for the user interface
├── .dockerignore
├── docker-compose.yaml               # Orchestrates all services
└── README.md
```

## Technologies Used

This project leverages a modern tech stack:

*   **Backend:**
    *   .NET 9
    *   ASP.NET Core Web API
    *   Entity Framework Core (for data access)
    *   Serilog (for structured logging)
*   **Frontend:**
    *   Angular
    *   TypeScript
    *   Angular Material (for UI components)
*   **Database:**
    *   Microsoft SQL Server (default)
    *   PostgreSQL (supported, configurable)
*   **DevOps & Tooling:**
    *   Docker & Docker Compose
    *   Seq (for log analysis)

## Getting Started

Follow these instructions to get the application running on your local machine.

### Prerequisites

Ensure you have the following software installed:
*   [Docker Desktop](https://www.docker.com/products/docker-desktop/)
*   [.NET SDK](https://dotnet.microsoft.com/download) (for running EF Core migrations locally)
*   [Node.js and npm](https://nodejs.org/) (for managing frontend packages)

### Configuration

1.  **Create an Environment File:**
    In the root directory (`c:\source\FormulaRace`), create a file named `.env`. This file will hold your local configuration secrets.

2.  **Add Configuration Variables:**
    Copy the following content into your new `.env` file. Choose a strong, secure password for `DB_PASSWORD`.

    ```env
    # .env

    # Password for the database user (both SQL Server and PostgreSQL)
    DB_PASSWORD=YourStrong(!)Password

    # Default environment for ASP.NET Core
    ASPNETCORE_ENVIRONMENT=Development

    # URLs for the backend API service inside the container
    ASPNETCORE_URLS=http://+:8080

    # URL for the Seq logging service
    Seq_ServerUrl=http://seq:5341
    ```

### Running the Application

The entire application stack can be launched with a single command from the root directory.

1.  **Build and Run Containers:**
    Open a terminal in the root of the project (`c:\source\FormulaRace`) and run:
    ```bash
    docker-compose up --build -d
    ```
    This command will:
    *   Build the Docker images for the backend API and frontend Angular app.
    *   Pull images for SQL Server and Seq.
    *   Start all services in detached mode (`-d`).

2.  **Accessing the Services:**
    Once the containers are running, you can access the different parts of the application:
    *   **Frontend Application:** [http://localhost:4200](http://localhost:4200)
    *   **Backend API (Swagger):** [http://localhost:8080/swagger](http://localhost:8080/swagger)
    *   **Seq Log Viewer:** [http://localhost:5341](http://localhost:5341)

3.  **Stopping the Application:**
    To stop all running containers, use the command:
    ```bash
    docker-compose down
    ```

## Features

*   **Pilot Management:** Full CRUD functionality for pilots.
*   **Team Management:** Full CRUD functionality for teams.
*   **Data Presentation:** Server-side paging, sorting, and filtering for pilot data.
*   **Containerized:** Fully containerized with Docker for easy setup and deployment.
*   **Structured Logging:** Integrated with Seq for easy log monitoring and analysis.

## Status

Project is: _in progress_

## Contact

Created by [@SutoZ]
