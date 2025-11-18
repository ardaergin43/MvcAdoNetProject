# 🛒 MVC ADO.NET Product Management System

A comprehensive ASP.NET Core MVC web application built with ADO.NET for direct database operations. This project demonstrates a complete CRUD (Create, Read, Update, Delete) system for product management with user authentication, image upload capabilities, and an intuitive user interface.

![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-6.0+-purple.svg)
![C#](https://img.shields.io/badge/C%23-10.0-blue.svg)
![ADO.NET](https://img.shields.io/badge/ADO.NET-Direct%20Database-green.svg)
![SQL Server](https://img.shields.io/badge/SQL%20Server-2019+-red.svg)

## 📋 Table of Contents

- [Overview](#overview)
- [Features](#features)
- [Technology Stack](#technology-stack)
- [Project Architecture](#project-architecture)
- [Database Schema](#database-schema)
- [Installation & Setup](#installation--setup)
- [Usage Guide](#usage-guide)
- [Project Structure](#project-structure)
- [Key Components](#key-components)
- [Screenshots](#screenshots)
- [Future Enhancements](#future-enhancements)

## 🎯 Overview

This project is a full-featured product management system developed during my second year of Computer Programming studies. It showcases the fundamental concepts of ASP.NET Core MVC architecture while utilizing ADO.NET for efficient database operations without the overhead of ORM frameworks like Entity Framework.

The application serves dual purposes:
1. **Public Product Showcase** - A landing page displaying products with advanced filtering capabilities
2. **Admin Dashboard** - A secure management panel for authenticated users to perform CRUD operations on products

## ✨ Features

### 🔐 Authentication System
- **User Registration** - New users can create accounts with email validation
- **Secure Login** - Email and password-based authentication
- **Session Management** - Persistent user sessions with automatic timeout
- **Duplicate Email Prevention** - Built-in validation to prevent duplicate registrations

### 📦 Product Management
- **Create Products** - Add new products with images, descriptions, pricing, and stock information
- **Read Products** - View all products in a responsive grid layout with detailed information
- **Update Products** - Edit existing product details and replace product images
- **Delete Products** - Remove products with confirmation dialogs to prevent accidental deletions
- **Image Upload & Management** - Upload product images with automatic file handling and cleanup

### 🔍 Advanced Filtering & Search
- **Name-based Search** - Real-time product filtering by name
- **Price Range Filter** - Min/max price sliders for budget-based searching
- **Status Filter** - Filter products by active/inactive status
- **Dynamic UI Updates** - Client-side filtering without page reloads

### 💡 User Experience
- **Responsive Design** - Bootstrap 5-based responsive layout for all devices
- **Modal Dialogs** - Clean UX with modal popups for login, registration, and confirmations
- **Product Status Indicators** - Visual badges showing product availability
- **"New Arrival" Badges** - Highlights for the latest 3 products
- **FAQ Section** - Accordion-based frequently asked questions

## 🛠 Technology Stack

### Backend
- **ASP.NET Core MVC** - Web application framework
- **C# 10** - Primary programming language
- **ADO.NET** - Direct database access layer
- **SQL Server** - Relational database management system
- **Session State** - User session management

### Frontend
- **HTML5** - Semantic markup
- **CSS3** - Custom styling
- **Bootstrap 5.3.3** - Responsive UI framework
- **JavaScript (Vanilla)** - Client-side interactivity
- **jQuery 3.7.1** - DOM manipulation and AJAX
- **jQuery UI 1.13.2** - Enhanced UI components

### Database
- **Microsoft SQL Server** - Database engine
- **T-SQL** - Query language for stored procedures and operations

## 🏗 Project Architecture

The project follows the **Model-View-Controller (MVC)** architectural pattern:

```
MvcAdoNetProject/
│
├── Controllers/
│   ├── AccountController.cs      # Authentication & user management
│   └── HomeController.cs          # Product CRUD operations
│
├── Models/
│   ├── User.cs                    # User entity model
│   ├── Product.cs                 # Product entity model
│   ├── DBHelper.cs                # Database access layer (ADO.NET)
│   └── ErrorViewModel.cs          # Error handling model
│
├── Views/
│   ├── Account/
│   │   └── Login.cshtml           # Public product showcase & auth modals
│   ├── Home/
│   │   └── Index.cshtml           # Admin product management dashboard
│   └── Shared/
│       └── _Layout.cshtml         # Master layout template
│
├── wwwroot/
│   ├── css/
│   │   └── site.css               # Custom styles
│   ├── js/
│   │   └── site.js                # Custom JavaScript
│   ├── images/                    # Uploaded product images
│   └── lib/                       # Client-side libraries
│
└── Program.cs                     # Application entry point & configuration
```

## 🗄 Database Schema

The application uses two primary tables in SQL Server:

### Table_1: Users
```sql
CREATE TABLE dbo.Table_1 (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Surname NVARCHAR(100) NOT NULL,
    Email NVARCHAR(255) UNIQUE NOT NULL,
    Password NVARCHAR(255) NOT NULL
);
```

### Table_2: Products
```sql
CREATE TABLE dbo.Table_2 (
    ProductID INT PRIMARY KEY IDENTITY(1,1),
    ProductName NVARCHAR(255) NOT NULL,
    Description NVARCHAR(MAX),
    Price DECIMAL(18,2) NOT NULL,
    Stock INT NOT NULL,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    IsActive BIT NOT NULL DEFAULT 1,
    ImageUrl NVARCHAR(500)
);
```

## 🚀 Installation & Setup

### Prerequisites
- [.NET 6 SDK or higher](https://dotnet.microsoft.com/download)
- [SQL Server 2019 or higher](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (Express edition is sufficient)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)
- SQL Server Management Studio (SSMS) - Optional but recommended

### Step 1: Clone the Repository
```bash
git clone https://github.com/ardaergin43/MvcAdoNetProject.git
cd MvcAdoNetProject
```

### Step 2: Database Setup

1. Open SQL Server Management Studio (SSMS)
2. Connect to your local SQL Server instance
3. Create a new database:
```sql
CREATE DATABASE ProductManagementDB;
GO
```

4. Execute the following scripts to create tables:

```sql
USE ProductManagementDB;
GO

-- Users Table
CREATE TABLE dbo.Table_1 (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Surname NVARCHAR(100) NOT NULL,
    Email NVARCHAR(255) UNIQUE NOT NULL,
    Password NVARCHAR(255) NOT NULL
);

-- Products Table
CREATE TABLE dbo.Table_2 (
    ProductID INT PRIMARY KEY IDENTITY(1,1),
    ProductName NVARCHAR(255) NOT NULL,
    Description NVARCHAR(MAX),
    Price DECIMAL(18,2) NOT NULL,
    Stock INT NOT NULL,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    IsActive BIT NOT NULL DEFAULT 1,
    ImageUrl NVARCHAR(500)
);

-- Insert sample data (optional)
INSERT INTO dbo.Table_1 (Name, Surname, Email, Password)
VALUES ('Admin', 'User', 'admin@example.com', 'admin123');

INSERT INTO dbo.Table_2 (ProductName, Description, Price, Stock, CreatedDate, IsActive, ImageUrl)
VALUES 
('Sample Product 1', 'This is a sample product description', 99.99, 10, GETDATE(), 1, '/images/default.jpg'),
('Sample Product 2', 'Another sample product', 149.99, 5, GETDATE(), 1, '/images/default.jpg');
```

### Step 3: Configure Connection String

1. Open `appsettings.json` in the project root
2. Update the connection string with your SQL Server details:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=ProductManagementDB;Trusted_Connection=True;TrustServerCertificate=True;"
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

**Connection String Options:**

For **Windows Authentication**:
```
Server=localhost;Database=ProductManagementDB;Trusted_Connection=True;TrustServerCertificate=True;
```

For **SQL Server Authentication**:
```
Server=localhost;Database=ProductManagementDB;User Id=your_username;Password=your_password;TrustServerCertificate=True;
```

### Step 4: Restore Dependencies
```bash
dotnet restore
```

### Step 5: Create Images Directory

Ensure the `wwwroot/images` directory exists:
```bash
mkdir -p wwwroot/images
```

### Step 6: Run the Application
```bash
dotnet run
```

Or press **F5** in Visual Studio to run with debugging.

### Step 7: Access the Application

Open your browser and navigate to:
- **HTTP**: `http://localhost:5096`
- **HTTPS**: `https://localhost:7013`

## 📖 Usage Guide

### For End Users

1. **Browse Products**
   - Visit the landing page to see all available products
   - Use the search bar to filter by product name
   - Set price range filters to find products within your budget
   - Filter by active/inactive status

2. **Create an Account**
   - Click the "Register" button in the top-right corner
   - Fill in your name, surname, email, and password
   - Submit the registration form
   - You'll be redirected to the login page

3. **Login**
   - Click the "Login" button
   - Enter your registered email and password
   - Upon successful login, you'll be redirected to the admin dashboard

### For Administrators

1. **Add New Products**
   - After logging in, you'll see the product management dashboard
   - Fill in the product form with:
     - Product name
     - Description
     - Price
     - Stock quantity
     - Product image (JPEG, PNG, GIF)
   - Click "Add" to create the product

2. **Update Existing Products**
   - Locate the product in the products table
   - Click the "Update" button
   - Modify any field (name, description, price, stock, image)
   - Save changes

3. **Delete Products**
   - Click the "Delete" button next to a product
   - Confirm deletion in the modal dialog
   - The product will be permanently removed

4. **Logout**
   - Click the "Logout" button in the navigation bar
   - You'll be redirected to the public landing page

## 📂 Project Structure

### Controllers

#### `AccountController.cs`
Handles all authentication-related operations:
- **Login()** [GET] - Displays the public product showcase with login/register modals
- **Login()** [POST] - Validates user credentials and creates session
- **Register()** [POST] - Creates new user accounts with validation
- **Logout()** [POST] - Clears user session and redirects to landing page

#### `HomeController.cs`
Manages product CRUD operations:
- **Index()** [GET] - Displays admin dashboard with product list
- **AddProduct()** [POST] - Creates new products with image upload
- **UpdateProduct()** [POST] - Updates existing product details
- **DeleteProduct()** [POST] - Removes products from database

### Models

#### `User.cs`
User entity representing registered accounts:
```csharp
public class User {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}
```

#### `Product.cs`
Product entity with all necessary fields:
```csharp
public class Product {
    public int Id { get; set; }
    public string ProductName { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public DateTime CreatedDate { get; set; }
    public bool IsActive { get; set; }
    public string ImageUrl { get; set; }
}
```

#### `DBHelper.cs`
Database access layer using ADO.NET:
- **ValidateUser()** - Authenticates users against the database
- **RegisterUser()** - Inserts new user records with duplicate email check
- **GetAllProducts()** - Retrieves all products ordered by creation date
- **AddProduct()** - Inserts new product records
- **UpdateProduct()** - Updates existing product records
- **DeleteProduct()** - Removes product records by ID

### Views

#### `Login.cshtml`
Public-facing landing page featuring:
- Product grid with responsive cards
- Search and filter functionality
- Login modal (Bootstrap modal)
- Registration modal (Bootstrap modal)
- FAQ accordion section
- "New Arrival" badges for recent products

#### `Index.cshtml`
Admin dashboard with:
- Product creation form
- Data table with all products
- Inline update modals for each product
- Delete confirmation dialogs
- Image preview for products

#### `_Layout.cshtml`
Master layout template including:
- Navigation bar with session-aware login/logout
- User greeting with username display
- Responsive Bootstrap navbar
- Footer with copyright information

## 🔑 Key Components

### Session Management
The application uses ASP.NET Core Session to maintain user state:
```csharp
HttpContext.Session.SetInt32("UserId", user.Id);
HttpContext.Session.SetString("UserName", user.Name);
```

Sessions are configured with a 30-minute timeout and are used to protect admin routes.

### Image Upload System
Product images are handled with:
- **Unique Filename Generation** - GUID-based naming to prevent conflicts
- **File Storage** - Images saved to `wwwroot/images/`
- **Old Image Cleanup** - Automatic deletion of replaced images
- **Path Storage** - Relative URLs stored in database

### Client-Side Filtering
Real-time product filtering implemented with vanilla JavaScript:
```javascript
function filterProducts() {
    const name = document.getElementById("searchName").value.toLowerCase();
    const minPrice = parseFloat(document.getElementById("minPrice").value) || 0;
    const maxPrice = parseFloat(document.getElementById("maxPrice").value) || Number.MAX_VALUE;
    // ... filtering logic
}
```

### Direct ADO.NET Database Access
The project uses classic ADO.NET components:
- **SqlConnection** - Database connection management
- **SqlCommand** - SQL query execution
- **SqlDataReader** - Forward-only data reading
- **Parameterized Queries** - SQL injection prevention

Example:
```csharp
using (SqlConnection connection = GetConnection()) {
    string query = "SELECT * FROM dbo.Table_2 WHERE ProductID = @Id";
    SqlCommand command = new SqlCommand(query, connection);
    command.Parameters.AddWithValue("@Id", productId);
    connection.Open();
    // Execute command
}
```

## 🎨 Screenshots

### Landing Page (Public View)
The public product showcase with filtering options and authentication modals.

### Admin Dashboard
Product management interface with CRUD operations and data table.

### Product Cards
Responsive product cards with images, pricing, and status indicators.

### Modal Dialogs
Bootstrap modals for login, registration, updates, and confirmations.

## 🚧 Future Enhancements

### Planned Features
- [ ] **Password Hashing** - Implement bcrypt or PBKDF2 for secure password storage
- [ ] **Admin Role System** - Add role-based access control (Admin vs. Regular User)
- [ ] **Shopping Cart** - Allow users to add products to cart
- [ ] **Order Management** - Complete e-commerce functionality with orders
- [ ] **Email Verification** - Send confirmation emails during registration
- [ ] **Forgot Password** - Password reset functionality
- [ ] **Product Categories** - Organize products into categories
- [ ] **Product Reviews** - Allow users to rate and review products
- [ ] **Search Optimization** - Full-text search with SQL Server
- [ ] **Image Gallery** - Multiple images per product
- [ ] **Export Functionality** - Export product data to Excel/PDF
- [ ] **API Endpoints** - RESTful API for mobile apps
- [ ] **Logging System** - Implement Serilog for application logging
- [ ] **Unit Tests** - Add xUnit tests for business logic
- [ ] **Docker Support** - Containerize the application

### Performance Improvements
- [ ] **Caching** - Implement Redis or in-memory caching
- [ ] **Pagination** - Add server-side pagination for large datasets
- [ ] **Lazy Loading** - Load product images on demand
- [ ] **Connection Pooling** - Optimize database connection management

### Security Enhancements
- [ ] **HTTPS Enforcement** - Force HTTPS in production
- [ ] **CSRF Protection** - Add anti-forgery tokens
- [ ] **Rate Limiting** - Prevent brute-force attacks
- [ ] **Input Validation** - Enhanced server-side validation
- [ ] **SQL Injection Prevention** - Review and audit all queries

## 📝 License

This project is an educational demonstration and is available for learning purposes. Feel free to use, modify, and distribute as needed.

## 👤 Author

**Arda Ergin**
- GitHub: [@ardaergin43](https://github.com/ardaergin43)

## 🙏 Acknowledgments

- Built during Computer Programming studies (2nd year)
- Demonstrates fundamental ASP.NET Core MVC concepts
- Showcases ADO.NET for direct database access
- Bootstrap framework for responsive design
- jQuery for enhanced client-side interactivity

---

**Note**: This project was created as a learning exercise during university studies. It demonstrates core concepts of ASP.NET Core MVC and ADO.NET but may require security enhancements (such as password hashing) before production use.
