# ProjectManager-01.WebAPI

The purpose of the ProjectManager application is to help manage projects in an organized and efficient way. It features a well-known ticketing system that allows companies and teams to track tasks, issues, and organize the work within different projects.

![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-5C2D91?style=for-the-badge&logo=dotnet&logoColor=white)
![MSSQL](https://img.shields.io/badge/MSSQL-CC2927?style=for-the-badge&logo=microsoftsqlserver&logoColor=white)
![SignalR](https://img.shields.io/badge/SignalR-512BD4?style=for-the-badge&logo=signalr&logoColor=white)
![Dapper](https://img.shields.io/badge/Dapper-0082C9?style=for-the-badge&logoColor=white)
![xUnit](https://img.shields.io/badge/xUnit-5C2D91?style=for-the-badge&logo=xunit&logoColor=white)
![Moq](https://img.shields.io/badge/Moq-8A2BE2?style=for-the-badge&logoColor=white)
![JWT](https://img.shields.io/badge/JWT-000000?style=for-the-badge&logo=jsonwebtokens&logoColor=white)
![AutoMapper](https://img.shields.io/badge/AutoMapper-DD0031?style=for-the-badge&logoColor=white)
![Swagger](https://img.shields.io/badge/Swagger-5AE1FF?style=for-the-badge&logo=swagger&logoColor=white)
![Postman](https://img.shields.io/badge/Postman-FF6C37?style=for-the-badge&logo=postman&logoColor=white)

---

## Useful links

### [Authorization and Authentication Document](https://github.com/jedrzejkoriat/ProjectManager-01.WebAPI/blob/main/AUTHDOC.md) - IMPORTANT❗
### [Database Setup](https://github.com/jedrzejkoriat/ProjectManager-02.DatabaseSetup)

---

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
- [Git](https://git-scm.com/)
- [MSSQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

## How to Run

1. **Clone the database setup repository**

```powershell
git clone https://github.com/jedrzejkoriat/ProjectManager-02.DatabaseSetup.git
```

2. **Navigate to the project directory**

```powershell
cd ProjectManager-02.DatabaseSetup
```

3. **Run the setup application**

```powershell
dotnet run
```

4. **Follow the steps in commandline**

- Add database server name
- Choose whether to add initial admin
- Choose whether to add test data

5. **Leave the setup project directory**

```powershell
cd ..
```

6. **Clone the main project repository**

```powershell
git clone https://github.com/jedrzejkoriat/ProjectManager-01.WebAPI.git
```

7. **Navigate to the project**

```powershell
cd ProjectManager-01.WebAPI\ProjectManager_01.WebAPI.API
```

8. **Set the database connection string and JWT secret key as an environment variables or in appsettings.json**

Replace `[SERVERNAME]` with your SQL Server instance name (f.ex. `(localdb)\MSSQLLocalDB`):

```powershell
setx ConnectionStrings__DefaultConnection "Server=[SERVERNAME];Database=ProjectManagerDB;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=True"
```

> ❗ If your account has elevated privileges (f. ex. `sysadmin`), it will override database-level DENY permissions.

Replace `[SECRETKEY]` with your JWT secret key:

```powershell
setx Jwt__Key "[SECRETKEY]"
```

Or manually open `appsettings.json` in any text editor and paste this with replaced `[SERVERNAME]` and `[SECRETKEY]`:

```json
{
    "ConnectionStrings": {
        "DefaultConnection": "Server=[SERVERNAME];Database=ProjectManagerDB;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=True"
    },
    "Jwt": {
        "Key": "[SECRETKEY]",
        "Issuer": "projectmanager-api",
        "Audience": "projectmanager-clients"
    },
    "Logging": {
        "LogLevel": {
            "Default": "Information",
            "Microsoft.AspNetCore": "Warning"
        }
    },
    "AllowedHosts": "*"
}

```

9. **Open new CLI window.**

10. **Run the application.**

```powershell
dotnet run --launch-profile https
```

11. **Open new CLI window.**

12. **Run Swagger in browser.**

```powershell
start https://localhost:7005/swagger/index.html
```

---

## Database schema

![schema](https://github.com/user-attachments/assets/96a664f6-3101-4c68-b474-cbb311a466ac)
