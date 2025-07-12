# KhumaloCraftWebsite

KhumaloCraft is an ASP.NET Core MVC web application designed to showcase and manage handcrafted products from local artisans.
The application allows users to browse, search, and view product details, while administrators can manage products through an intuitive dashboard.
Built to support local crafters and help bring their work to a global audience.

# Getting Started
## Prerequisites
Development Environment: Visual Studio 2019 or later

Framework: .NET Core 3.1 or later

Database: SQL Server (local or cloud instance)

Operating System: Windows, macOS, or Linux

## How to Apply
Download Visual Studio: Get Visual Studio 2019 or later from the official Microsoft website.

Install .NET Core: Download and install the .NET Core SDK from the official .NET website.

Clone Repository: Clone this repository from GitHub or download the source code archive.

Open Solution: Open the solution file (.sln) in Visual Studio.

Configure Database: Update the connection string in appsettings.json to point to your SQL Server instance.

Apply Migrations: Use the Package Manager Console or CLI to apply database migrations.

Build Solution: Build the solution to restore dependencies and compile the application.

Run Application: Start the application using Visual Studio (F5) or dotnet run.

# Functionality
## Features
Product Listings: View all available handcrafted products with details such as name, description, price, and images.

Product Details: Click on a product to see detailed information, including images and descriptions.

Admin Dashboard: Manage products by adding, editing, or removing them.

User Authentication: Supports login and registration, with role-based access for admins.

Search and Filter: Find products quickly by searching by name or filtering by category.

Responsive Design: Interface adapts seamlessly across desktop and mobile devices.

User Interface (UI) Design
Technology: ASP.NET Core MVC with Razor views

Styling: HTML5, CSS3, Bootstrap

Navigation: Intuitive menus for browsing, searching, and administration

Data Storage and Management
Database: SQL Server

Data Access: Entity Framework Core for object-relational mapping

Data Models: Includes models for Product, User, Category, and related entities

Performance
Optimized queries and pagination for large product lists

Responsive user interface for fast load times

## Non-Functional Requirements
Follows the MVC design pattern with clear separation of concerns

Uses internationally accepted coding standards and detailed comments for maintainability

Supports generic collections and strongly typed models for data handling

Implements role-based access control for admin features

Multiple views and controllers handle browsing, managing, and searching products

# FAQs
Q: Can anyone add or remove products?
A: No, only authenticated users with admin roles can manage products.

Q: Is there a shopping cart or checkout feature?
A: This version focuses on browsing and managing products; shopping features can be added in future releases.

Q: Can users search for products?
A: Yes, users can search by name and filter by category.

Q: Is the website mobile-friendly?
A: Yes, the design is responsive and adapts to various screen sizes.

Q: What happens if a required field is missing when adding a product?
A: The application will display validation errors prompting the user to complete all required fields.

# License
This project is licensed under the MIT License. See the LICENSE file for details.

# Author
Full Name: Dianca Jade Naidu

Email: diancanaidu@gmail.com

# Acknowledgements
OpenAI. (2024). ChatGPT. Retrieved from https://chatgpt.com

Microsoft. (2024). ASP.NET Core MVC Documentation. Retrieved from https://docs.microsoft.com/aspnet/core/mvc

FreeCodeCamp. (2023). ASP.NET MVC Tutorial [Video]. YouTube. https://youtu.be/1K5JIrXRzGs

