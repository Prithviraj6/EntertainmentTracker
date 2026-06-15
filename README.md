# EntertainmentTracker

A clean full-stack anime tracking app for discovering shows, managing a personal watch list, and keeping progress, status, and scores in one place.

## Overview

EntertainmentTracker is built as a modern web application with a React frontend and an ASP.NET Core API. The backend integrates with the Jikan API for anime search and details, stores user libraries in PostgreSQL, and secures user sessions with JWT access and refresh tokens.

## Features

- User registration, login, refresh, and logout flows
- Protected dashboard and personal list views
- Anime search powered by Jikan
- Anime detail pages using MyAnimeList IDs
- Personal anime library with status filtering
- Progress, score, and status updates
- User stats endpoint for dashboard summaries
- Swagger UI for API exploration in development

## Tech Stack

| Layer | Technology |
| --- | --- |
| Frontend | React, Vite, React Router, Axios |
| Backend | ASP.NET Core, C#, Swagger/OpenAPI |
| Data | PostgreSQL, Entity Framework Core |
| Auth | JWT access tokens, refresh tokens, BCrypt |
| External API | Jikan API |

## Project Structure

```text
EntertainmentTracker/
|-- Backend/
|   `-- EntertainmentTracker/
|       |-- EntertainmentTracker.API/             # Controllers, middleware, API host
|       |-- EntertainmentTracker.Application/     # Services, DTOs, validation, contracts
|       |-- EntertainmentTracker.Domain/          # Entities and enums
|       `-- EntertainmentTracker.Infrastructure/  # EF Core, repositories, auth, Jikan client
|-- Frontend/
|   `-- entertainment-tracker-web/
|       |-- src/components/                       # Shared UI components
|       |-- src/pages/                            # App pages and route screens
|       |-- src/services/                         # API-facing service modules
|       `-- src/api/                              # Axios client
`-- README.md
```

## Getting Started

### Prerequisites

- .NET SDK compatible with `net10.0`
- Node.js and npm
- PostgreSQL

### Backend Setup

1. Create a PostgreSQL database named `entertainment_tracker`.
2. Update the database connection string and JWT settings in:

```text
Backend/EntertainmentTracker/EntertainmentTracker.API/appsettings.json
```

3. Run the API:

```bash
cd Backend/EntertainmentTracker
dotnet restore
dotnet ef database update --project EntertainmentTracker.Infrastructure --startup-project EntertainmentTracker.API
dotnet run --project EntertainmentTracker.API
```

The API is configured for HTTPS on `https://localhost:7205`.

In development, Swagger is available at:

```text
https://localhost:7205/swagger
```

### Frontend Setup

```bash
cd Frontend/entertainment-tracker-web
npm install
npm run dev
```

The frontend runs on:

```text
http://localhost:5173
```

The Axios client currently points to:

```text
https://localhost:7205/api
```

## API Routes

### Auth

| Method | Route | Description |
| --- | --- | --- |
| `POST` | `/api/auth/register` | Create a user account |
| `POST` | `/api/auth/login` | Log in and receive tokens |
| `GET` | `/api/auth/me` | Get the current authenticated user |
| `POST` | `/api/auth/refresh` | Refresh access and refresh tokens |
| `POST` | `/api/auth/logout` | Revoke a refresh token |

### Anime

| Method | Route | Description |
| --- | --- | --- |
| `GET` | `/api/anime/search?q={query}` | Search anime through Jikan |
| `GET` | `/api/anime/{malId}` | Get anime details by MyAnimeList ID |

### User Anime

| Method | Route | Description |
| --- | --- | --- |
| `POST` | `/api/user-anime` | Add anime to the authenticated user's list |
| `GET` | `/api/user-anime` | Get the authenticated user's anime list |
| `GET` | `/api/user-anime?status={status}` | Filter the list by status |
| `GET` | `/api/user-anime/stats` | Get list statistics |
| `GET` | `/api/user-anime/{animeId}` | Get one saved anime entry |
| `PATCH` | `/api/user-anime/{animeId}/progress` | Update watched episode progress |
| `PATCH` | `/api/user-anime/{animeId}/status` | Update watch status |
| `PATCH` | `/api/user-anime/{animeId}/score` | Update score |
| `DELETE` | `/api/user-anime/{animeId}` | Remove anime from the list |

## Development Commands

### Frontend

```bash
npm run dev
npm run build
npm run lint
npm run preview
```

### Backend

```bash
dotnet restore
dotnet build
dotnet run --project EntertainmentTracker.API
dotnet ef database update --project EntertainmentTracker.Infrastructure --startup-project EntertainmentTracker.API
```

## Notes

- Keep production secrets out of `appsettings.json`; use user secrets, environment variables, or a secret manager.
- The frontend is configured for the API origin allowed by the backend CORS policy: `http://localhost:5173`.
- Jikan is a public API, so search/detail availability may depend on its rate limits and uptime.
