# CreditCardApi

A secure and modular **.NET Web API** for managing credit card information, including encryption of sensitive data, clean architecture, and CI/CD.

---

## Features

- **Credit Card Management**
  - Create, qurey
  - Card numbers, CVV, and expiry dates **encrypted at rest**
- **Entity Framework Core**
  - Code-first database schema
- **Clean, layered architecture**


## 🛠️ Tech Stack

- **.NET 8 Web API**
- **Entity Framework Core**
- **SQL Server / SQLite (configurable)**
- **ASP.NET Core Identity (optional)**
- **Data encryption**

---

## 📦 Project Structure

```
Assignment_CreditCardApi
 ├── CreditCardApi/
 │     │ Program.cs
 │     │
 │     ├── Endpoints/
 │     │   └── CreditCardEndpoints.cs
 │     │
 │     ├── Models/
 │     │   └── CreditCard.cs
 │     │
 │     ├── DTOs/
 │     │   └── CreditCardDto.cs
 │     │
 │     ├── Interfaces/
 │     │   └── ICreditCardService.cs
 │     │
 │     ├── Services/
 │     │   └── CreditCardService.cs
 │     │
 │     ├── Data/
 │     │   └── AppDbContext.cs
 │     │
 │     └── Utils/
 │         └── EncryptionHelper.cs
 │         └── CreditCardValidator.cs
 │
 └── CreditCardApiTests/
       │ UnitTest1.cs
       ├── Services/
       │   └── CreditCardServiceTests.cs
       │       
       └── Utils/
           └── DbHelper.cs
           └── EncryptionHelperTests.cs
```

🚀 Getting Started
1️⃣ Install Dependencies
dotnet restore

2️⃣ Apply EF Core Migration
dotnet ef migrations add InitialCreate
dotnet ef database update

3️⃣ Run the Application
dotnet run


API will run at:

https://localhost:5001
http://localhost:5000

🧪 API Endpoints
➕ Create Credit Card

POST /api/cards

```json
{
  "cardHolder": "John Doe",
  "cardNumber": "4111111111111111",
  "expiry": "12/28",
  "cvc": "123"
}
```


📄 Get All Cards

GET /api/cards

🔍 Get Card by ID

GET /api/cards/{id}

🔒 Encryption

Base64：

Card Number

CVC


🛠️ Technologies Used

.NET 8 Minimal API

Entity Framework Core

SQLite Database


⚙️ CI/CD (GitHub Actions)

Place the following file in:

.github/workflows/dotnet.yml

```yaml
name: .NET CI/CD

on:
  push:
    branches: [ "main" ]
  pull_request:
    branches: [ "main" ]

jobs:
  build:
    runs-on: ubuntu-latest

    steps:
    - name: Checkout code
      uses: actions/checkout@v3
      with:
          token: ${{ secrets.GH_TOKEN }}

    - name: Setup .NET
      uses: actions/setup-dotnet@v3
      with:
        dotnet-version: 8.0.x

    - name: Restore dependencies
      run: dotnet restore

    - name: Build
      run: dotnet build --no-restore

    - name: Run Tests
      run: dotnet test --no-build
```

📘 License

This project is open-source and free to use.

