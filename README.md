# 🌌 Star Wars Coding Exercise – ASP.NET Core MVC (.NET 8)

### Developer: Gregory O. Harnach  
*(Built and tested with Visual Studio 2022, .NET 8, and SQL LocalDB)*

---

## 🚀 Overview

**GregHarnach-starWars-CodingExercise** is a C#.NET ASP.NET Core MVC web application that demonstrates data retrieval, seeding, and CRUD operations using data from the **Star Wars API (SWAPI)**.

The project dynamically fetches *Starship* data from [`https://swapi.info/starships`](https://swapi.info/starships), stores it in a local SQL database using **Entity Framework Core**, and displays the data in a **responsive Bootstrap-based table** with full **Create, Read, Update, and Delete (CRUD)** functionality.

---

## 🧩 Project Features

- **Starship Model**  
  Mirrors SWAPI’s `starship` resource structure (fields like `name`, `model`, `manufacturer`, etc.).

- **Data Seeding**  
  Automatically fetches live data from SWAPI and seeds it into a local SQL database via EF Core.

- **Entity Framework Core + SQL LocalDB**  
  Uses EF Core for ORM mapping, migrations, and local persistence.

- **Responsive MVC UI**  
  Bootstrap 5 + DataTables provides responsive sorting, searching, and filtering.

- **Full CRUD Support**
  - Create new Starships manually.
  - View Starship details.
  - Edit and Delete existing records.

- **Structured Logging & Error Handling**
  - Uses `ILogger<T>` for runtime logging to console and Visual Studio debug output.
  - Wraps all database and HTTP operations in safe `try/catch` blocks.

- **Unit Tests (MSTest Framework)**
  - Tests all controller actions (Index, Details, Create, Edit, Delete).
  - Uses EF Core InMemory provider for isolated, repeatable tests.

---

## 🧠 Architecture

```
GregHarnach-starWars-CodingExercise.sln
│
├── GregHarnach_starWars_CodingExercise
│   ├── Controllers/
│   │   └── StarshipsController.cs
│   ├── Data/
│   │   └── AppDbContext.cs
│   ├── Models/
│   │   └── Starship.cs
│   ├── Seeding/
│   │   └── StarshipSeeder.cs
│   ├── Views/
│   │   └── Starships/
│   │       ├── Index.cshtml
│   │       ├── Create.cshtml
│   │       ├── Edit.cshtml
│   │       ├── Details.cshtml
│   │       └── Delete.cshtml
│   ├── appsettings.json
│   ├── Program.cs
│   └── Startup (built-in for .NET 8 minimal hosting)
│
└── GregHarnach_starWars_CodingExercise.Tests
    ├── Controllers/
    │   └── StarshipsControllerTests.cs
    ├── TestUtils/
    │   ├── TestDbFactory.cs
    │   └── StarshipSeedData.cs
    └── Models/
        └── StarshipValidationTests.cs
```

---

## 🧱 Technologies Used

| Category | Technology |
|-----------|-------------|
| **Framework** | .NET 8 (ASP.NET Core MVC) |
| **Language** | C# |
| **Database** | SQL Server LocalDB |
| **ORM** | Entity Framework Core |
| **Frontend** | Bootstrap 5, DataTables.js |
| **Testing** | MSTest |
| **Logging** | Microsoft.Extensions.Logging (Console + Debug providers) |

---

## ⚙️ Setup Instructions

### 1️⃣ Prerequisites
- Visual Studio 2022 (latest)
- .NET 8 SDK
- SQL Server LocalDB (installed by default with Visual Studio)

### 2️⃣ Clone or Extract
Extract the repository zip to your workspace:
```
E:\WORK-Interviews\GoEngineer\WORK\GregHarnach-starWars-CodingExercise
```

### 3️⃣ Build & Restore
In Visual Studio:
- Open the `.sln`
- Right-click the solution → **Restore NuGet Packages**
- Rebuild Solution

### 4️⃣ Apply Migrations
Open **Package Manager Console**:
```powershell
Add-Migration Initial
Update-Database
```

### 5️⃣ Run
Press **F5** or **Ctrl+F5**

By default the app starts at:
```
https://localhost:5294/Starships
```

On first run:
- The `StarshipSeeder` fetches data from SWAPI.
- The seeded data populates the `StarshipsDb` (LocalDB).
- The Index view displays all starships in a responsive table.

---

## 🧪 Running Tests

Open **Test Explorer** in VS2022 → click **Run All Tests**.

Tests include:
- Controller CRUD operations
- Validation for required fields
- In-memory database behavior

All tests use `Microsoft.EntityFrameworkCore.InMemory` for fast, isolated test runs.

---

## 🪵 Logging and Error Handling

- All controller and seeder methods include **try/catch** with structured logging.
- Logs are written to:
  - **Console** (when running from command line)
  - **Debug Output Window** (in Visual Studio)
- Example log output:
  ```
  info: StarshipsController[0]
        Created new Starship: X-wing
  error: StarshipsController[0]
        Error updating Starship ID 4: System.InvalidOperationException: ...
  ```

---

## 💬 Key Learning Objectives

1. Consuming a public REST API (SWAPI) in .NET.
2. Mapping JSON data to EF Core entities.
3. Building a CRUD UI using ASP.NET MVC + Razor Views.
4. Using EF Core migrations and seeding.
5. Writing unit tests with EF Core InMemory.
6. Implementing structured logging and error handling.

---

## 📂 Database Connection

Default connection string in `appsettings.json`:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=StarshipsDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
}
```

---

## 📘 Example API Reference (SWAPI)

Data is fetched from:
- [`https://swapi.info/starships`](https://swapi.info/starships)
- Fallback to [`https://swapi.dev/api/starships/`](https://swapi.dev/api/starships/)

Each starship record typically contains:
```json
{
  "name": "Millennium Falcon",
  "model": "YT-1300 light freighter",
  "manufacturer": "Corellian Engineering Corporation",
  "cost_in_credits": "100000",
  "length": "34.37",
  "crew": "4",
  "passengers": "6",
  "cargo_capacity": "100000",
  "hyperdrive_rating": "0.5",
  "MGLT": "75",
  "starship_class": "Light freighter"
}
```

---

## 🧰 Future Enhancements

- Pagination and server-side filtering
- API endpoints for RESTful access (`/api/starships`)
- Caching of SWAPI responses
- Dockerfile + containerized SQL Server
- CI/CD pipeline for automated testing (GitHub Actions)

---

## 📄 License

This project is for **educational and interview demonstration purposes**.  
No commercial license is required for the SWAPI dataset or bootstrap libraries used.

---

### ✨ Author

**Gregory O. Harnach**  
📍 Raleigh, NC  
💼 C# / .NET Full Stack Developer  
🔗 [LinkedIn Profile](https://www.linkedin.com/in/gregory-harnach)
