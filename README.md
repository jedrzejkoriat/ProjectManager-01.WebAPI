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

4. **Provide your SQL Server name**

When prompted, enter your SQL Server instance name (e.g. `(localdb)\MSSQLLocalDB`):

```powershell
Provide your SQL server name: [YOUR SERVER NAME]
```

5. **Choose whether to add an initial admin (recommended)**

Users can only be added by an admin, so it's recommended to create one now:

```powershell
Add initial admin? (y/n): [y/n]
```

6. **Choose whether to seed test data**

```powershell
Add test data? (y/n): [y/n]
```

7. **Leave the setup directory**

```powershell
cd ..
```

8. **Clone the main project repository**

```powershell
git clone https://github.com/jedrzejkoriat/ProjectManager-01.WebAPI.git
```

9. **Set the database connection string as an environment variable**

Replace `[SERVERNAME]` with your SQL Server instance name:

```powershell
setx ConnectionStrings__DefaultConnection "Server=[SERVERNAME];Database=ProjectManagerDB;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=True"
```

> ❗ If your account has elevated privileges (e.g. `sysadmin`), it will override database-level DENY permissions.

10. **Set the JWT key as an environment variable**

Replace `[SECRETKEY]` with your JWT secret key:

```powershell
setx Jwt__Key "[SECRETKEY]"
```

11. **Run the application.**

```powershell
dotnet run
```

---

## Database schema

![image](https://github.com/user-attachments/assets/75b0f676-0969-42d5-88c1-38b26aedbcec)
