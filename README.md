# ProjectManager-01.WebAPI

The purpose of the ProjectManager application is to help manage projects in an organized and efficient way. It features a well-known ticketing system that allows companies and teams to track tasks, issues, and organize the work within different projects.

## How to Run

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
- [Git](https://git-scm.com/)
- [MSSQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

---

### Steps

1. **Clone the database setup repository**

```bash
git clone https://github.com/jedrzejkoriat/ProjectManager-02.DatabaseSetup.git
```

2. **Add your database server name in Program.cs of ProjectManager-02.DatabaseSetup**

3. **Run the application**

```bash
cd ProjectManager-02.DatabaseSetup
dotnet run
cd ..
```

4. **Clone the main project repository**

```bash
git clone https://github.com/jedrzejkoriat/ProjectManager-01.WebAPI.git
```

5. **Run the application**

```bash
cd ProjectManager-01.WebAPI
dotnet run
```