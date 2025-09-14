# 🐞 Viseral Bug Tracking System

A web-based bug tracking and project management system built with **ASP.NET Core 8.0**, **React.js**, and **SQLite**.  
The system streamlines bug reporting, tracking, and resolution while supporting project and task management, notifications, and analytics.

---

## 📌 Features

- **User Management**
  - Multi-role authentication (Admin, Developer, Tester)
  - JWT-based secure login and registration
  - Role-based access control

- **Project Management**
  - Create and manage projects
  - Assign developers and testers
  - Track milestones and progress

- **Bug Tracking**
  - Report and document bugs with attachments
  - Bug lifecycle: `OPEN → ASSIGNED → IN_PROGRESS → RESOLVED → CLOSED`
  - Priority classification: Critical, High, Medium, Low
  - Audit logs and reassignment support

- **Task Management**
  - Create and assign tasks
  - Track status and maintain logs
  - Attachments support

- **Notifications**
  - Email alerts for assignments and status updates
  - Project assignment notifications

- **Reporting & Analytics**
  - Bug trend analysis
  - Project progress dashboards
  - Productivity metrics
  - SLA breach detection

---

## 🛠️ Tech Stack

### Frontend
- React.js 18
- Axios (HTTP requests)
- CSS3 (custom styles)

### Backend
- ASP.NET Core 8.0 Web API
- Entity Framework Core (ORM)
- JWT for authentication
- Swagger/OpenAPI for API docs
- SMTP for email notifications

### Database
- SQLite 3.x
- EF Core Migrations

---

## 🚀 Getting Started

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [Node.js & npm](https://nodejs.org/) (for frontend)
- SQLite 3.x

### Clone the Repository
```bash
git clone https://github.com/RahulPatil-Tech/Visceral_bug_45.git
cd Visceral_bug_45
```

# 🚀 Backend Setup
```
cd Viseralbug
dotnet restore
dotnet ef database update   # Apply migrations
dotnet run
```

# Backend runs on:
```
https://localhost:5001
```

# 🎨 Frontend Setup
```
cd client
npm install
npm start
```
# Frontend runs on:
```
http://localhost:3000
```

⚙️ Configuration
----------------
```
appsettings.json              → Database, JWT, Email, File Upload settings
appsettings.Development.json  → Local dev configs 
ADMIN_SETUP.md                → Admin credentials setup
```

📂 Project Structure
--------------------
```
/Controllers        → API Controllers
/Data               → EF DbContext
/Middleware         → Global error handling
/Models             → Entity models & settings
/Repositories       → Data access layer
/Services           → Business logic
/Validators         → Input validation
/Properties         → Launch settings
```

🔒 Security
-----------
- Passwords hashed with bcrypt
- JWT expires after 24 hours
- HTTPS enforced
- Role-based authorization

📊 Reporting
------------
- Bug trends
- SLA breach detection
- Project dashboards
- Exportable reports

👨‍💻 User Roles
---------------
```
Admin:     Manage users, projects, and system configs
Developer: Fix bugs, create/assign tasks
Tester:    Report and verify bugs
```

📜 License
----------
This project is licensed under the MIT License.


