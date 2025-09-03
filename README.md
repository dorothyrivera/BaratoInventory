# BARATO

The **Barato Product Inventory App** is a full-stack inventory management system built with **ASP.NET Core Web API** and **Blazor**.  
It provides an easy way to manage products with CRUD operations, Redis caching, SQL Server persistence, and full Docker containerization.

## Features
- **Product Management**
  - Add, edit, delete, and view products
  - Display products in a sortable, searchable table
- **Caching**
  - Product lists cached in **Redis** for faster retrieval
  - Cache automatically refreshed on CRUD operations
- **Database**
  - Data stored in **SQL Server**
  - Managed with **Entity Framework Core** migrations
  - Comes with seeded test data
- **API**
  - RESTful API endpoints for all features
  - Includes input validation and structured error handling
  - Swagger UI for API exploration
- **Frontend (Blazor)**
  - User-friendly interface to manage products
  - Integrated form validation and success/error notifications
- **Testing**
  - Unit tests for services using **NUnit + Moq**
- **Deployment**
  - Fully containerized using **Docker Compose**
  - Runs Blazor frontend, API, SQL Server, and Redis together

## Tech Stack
Layer|Technology
-|-
Backend|ASP.NET Core Web API (.NET 8)
Frontend|Blazor (Server)
ORM|Entity Framework Core
Database|SQL Server (Docker)
Caching|Redis
Deployment|Docker & Docker Compose
Testing|NUnit + Moq
Versioning|GitHub

## Project Structure
- **BaratoInventory/**
  - **src/**
    - **BaratoInventory.API/** → ASP.NET Core Web API
    - **BaratoInventory.Blazor/** → Blazor Frontend (Server)
    - **BaratoInventory.Core/** → Entities, DTOs, Interfaces
    - **BaratoInventory.Infrastructure/** → EF Core, Redis, Repositories
  - **tests/**
    - **BaratoInventory.Tests/** → Unit tests
  - **docker-compose.yml**
  - **README.md**

## Getting Started
### 1. Clone the Repository
### 2. Run with Docker Compose
Make sure Docker is installed and opened. Then in **Visual Studio**, right-click the solution **`BaratoInventory`** in **Solution Explorer**, then click **Open in Terminal**, and run:
```bash
docker-compose up -d
```
Then run:
```bash
docker-compose up --build
```
This will start:
```bash
Blazor Frontend → http://localhost:7270
API (Swagger UI) → http://localhost:5250/swagger/index.html
SQL Server (with seeded data)
Redis
```
After running, open your browser and go to:
```bash
http://localhost:7270
```
### 3. Run Tests
In **Visual Studio**, go to the top menu and select:
```bash
Test → Test Explorer
```
From there:
1. Expand  the dropdowns in the Test Explorer tree until you find **GetAllAsync_ReturnsCached_WhenCacheHasValue**.
2. Right-click on that test.
3. Select **Run** to execute it.
```bash
The test will PASS if the cache contains values and the method correctly returns the cached data.
The test will FAIL if the cache is empty, invalid, or the method does not run the expected cached data.
```
