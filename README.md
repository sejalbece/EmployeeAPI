# Employee Management API

A full-stack employee management app built with **ASP.NET Core 8 Web API**, **Entity Framework Core**, **SQL Server**, and an **Angular 19** front-end.

The project was built step by step. Each version adds a new layer on top of the previous one.

<!-- Add a screenshot of the UI here, e.g.:
![Employee Management UI](docs/screenshot.png)
-->

---

## Features

### v1 – Basic CRUD
- Create, read, update and delete employees
- Layered architecture: Controller → Service → Repository
- DTOs for requests and responses
- Global exception handling middleware
- Swagger / OpenAPI documentation

### v2 – Querying & Rate Limiting
- Pagination (`pageNumber`, `pageSize`) with total records and total pages
- Search by name or email
- Filter by department
- Sorting by name, email, salary or created date (ascending / descending)
- Rate limiting: fixed window of 20 requests per 10 seconds, returns `429 Too Many Requests`
- CORS policy for front-end clients

### v3 – Angular UI
- Angular UI:
  - Create / update form with validation
  - Employee table with search, department filter, sorting and pagination
  - Edit and delete actions
  - Responsive layout

---

## Tech Stack

| Layer     | Technology                                      |
|-----------|-------------------------------------------------|
| Back-end  | ASP.NET Core 8 Web API, C#                      |
| Data      | Entity Framework Core 8, SQL Server             |
| Front-end | Angular 19, TypeScript, Reactive Forms, RxJS    |
| Docs      | Swagger (Swashbuckle)                           |

---

## Project Structure

```
EmployeeAPI/
├── Interview_API/               # ASP.NET Core Web API
│   ├── Controllers/             # API endpoints
│   ├── Services/                # Business logic
│   ├── Repositories/            # Data access
│   ├── DTOs/                    # Request / response models
│   ├── Entities/                # EF Core entities
│   ├── Data/                    # DbContext and configurations
│   ├── Middleware/              # Global exception handling
│   └── Migrations/              # EF Core migrations
└── EmployeeManagement.UI/
    └── EmployeeUI/              # Angular app
        └── src/app/
            ├── components/      # Employee page
            ├── services/        # API client
            └── models/          # TypeScript interfaces
```

---

## API Endpoints

Base URL: `https://localhost:7055/api/Employee`

| Method | Endpoint             | Description                                  |
|--------|----------------------|----------------------------------------------|
| GET    | `/api/Employee`      | Get employees (paged, searchable, sortable)  |
| GET    | `/api/Employee/{id}` | Get an employee by ID                        |
| POST   | `/api/Employee`      | Create an employee                           |
| PUT    | `/api/Employee`      | Update an employee                           |
| DELETE | `/api/Employee/{id}` | Delete an employee                           |

**Query parameters for `GET /api/Employee`:**

| Parameter    | Example      | Description                                  |
|--------------|--------------|----------------------------------------------|
| `pageNumber` | `1`          | Page number (default `1`)                    |
| `pageSize`   | `10`         | Records per page (default `10`)              |
| `search`     | `john`       | Matches name or email                        |
| `department` | `IT`         | `IT`, `HR` or `Account`                      |
| `sortBy`     | `salary`     | `name`, `email`, `salary`, `createdDate`     |
| `sortOrder`  | `desc`       | `asc` or `desc`                              |

Example:

```
GET /api/Employee?pageNumber=1&pageSize=10&search=john&department=IT&sortBy=salary&sortOrder=desc
```

---

## Getting Started

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- SQL Server (LocalDB, Express or full)
- [Node.js](https://nodejs.org/) 18+ and npm
- Angular CLI: `npm install -g @angular/cli`

### 1. Run the API

1. Update the connection string in `Interview_API/appsettings.json`:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=YOUR_SERVER;Database=Interview_Employee;Trusted_Connection=true;TrustServerCertificate=true;"
   }
   ```
2. Apply the migrations and start the API:
   ```bash
   cd Interview_API
   dotnet ef database update
   dotnet run --launch-profile https
   ```
3. Open Swagger at `https://localhost:7055/swagger`.

### 2. Run the Angular UI

```bash
cd EmployeeManagement.UI/EmployeeUI
npm install
ng serve
```

Open `http://localhost:4200`.

> The UI calls the API at `https://localhost:7055/api` (set in `src/environments/`). If your browser blocks the request, open `https://localhost:7055/swagger` once and trust the development certificate (`dotnet dev-certs https --trust`).

---

## Roadmap

- [ ] Authentication and authorization (JWT)
- [ ] Unit and integration tests
- [ ] Docker support
- [ ] Toast notifications and loading states in the UI

---

## Author

**Sejalben Jiyani** – [GitHub](https://github.com/sejalbece)
