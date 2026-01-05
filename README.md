# 🏋️ Gym Control System (ASP.NET MVC)

A simple and well-structured **Gym Control System** built using **ASP.NET MVC** as a junior-level project, focusing on clean code, 3-Layer Architecture, and core backend concepts.

---

## 📌 Project Description

This project is a web-based system designed to help manage basic gym operations such as members, trainers, workout sessions, and subscription plans.

The goal of this project is to demonstrate understanding of:
- ASP.NET MVC fundamentals  
- Layered architecture  
- Basic design patterns  
- Secure authentication  

---

## 🏗️ Application Architecture

### 🔹 Layered Structure

The application follows a **3-Layer Architecture** to separate responsibilities and improve maintainability.


### 🖥️ Presentation Layer
- ASP.NET MVC Controllers and Views  
- Handles user requests and responses  
- Displays data to users  

### ⚙️ Business Logic Layer
- Contains application rules and validations  
- Processes operations between UI and data  

### 🗄️ Data Access Layer
- Uses Entity Framework Core  
- Responsible for database communication  
- Implements repositories  

---

## 🧠 Applied Concepts

### ✔ Repository Pattern
Used to organize database queries and reduce code duplication.

### ✔ Unit of Work
Manages saving changes across multiple repositories.

### ✔ Dependency Injection
Injects services and repositories into controllers.

### ✔ AutoMapper
Maps database entities to DTOs.

---

## 🗂️ Project Folder Structure

```text
GymManagementSystemSolution/
│
├── RouteGymManagementPL        # Presentation Layer (ASP.NET MVC)
│   ├── Controllers
│   ├── Views
│   └── wwwroot
│
├── RouteGymManagementBLL       # Business Logic Layer
│   ├── Services
│   ├── ViewModels
│   └── Mapping
│
├── RouteGymManagementDAL       # Data Access Layer
│   ├── Entities
│   ├── Repositories
│   ├── UnitOfWork
│   └── DbContext
│
└── GymManagementSystemSolution.sln
```
---

## 🧩 Main Modules

### 🧍 Member Module
- Create, edit, delete members  
- Manage member profiles and health records  
- View membership details and plans

### 🧑‍🏫 Trainer Module
- Manage trainer profiles and schedules  
- Assign trainers to sessions  
- CRUD operations for trainer data

### 🗓️ Session Module
- Manage workout sessions (capacity, timing, trainer)  
- View and update session details  
- Support for session categories and status

### 💳 Plan Module
- Manage gym plans (duration, price, description)  
- Activate or deactivate plans  
- View all plan details

### 📎 Attachment Service
Handles file uploads (photos, documents, etc.) safely and consistently.

---

## 📎 File Upload Feature
Allows uploading profile images or documents.

**Process:**
1. Validate file type  
2. Generate unique file name  
3. Save file on server  
4. Store file reference in database  

---

## 🔐 Authentication & Authorization
Implemented using **ASP.NET Identity**.

**Includes:**
- User login and registration  
- Role-based access control  

---

## 🛠️ Technologies Used

| Tool / Concept | Usage |
|---------------|-------|
| C# | Backend development |
| ASP.NET MVC | Web application framework |
| Entity Framework Core | Object-Relational Mapping (ORM) |
| SQL Server | Relational database |
| AutoMapper | Entity ↔ DTO mapping |
| ASP.NET Identity | Authentication & authorization |
| 3-Layer Architecture | Separation of concerns (Presentation, BLL, DAL) |

---

## 🔄 Request Flow

```
User → Controller → Service → Repository → Entity Framework Core → Database → Response
```
---

## 🎯 Project Purpose

This project was developed as a **Junior ASP.NET MVC portfolio project** to demonstrate backend fundamentals, clean structure, and practical application of design patterns.
