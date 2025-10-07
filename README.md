📚 Mini Library Management System API

Tech Stack: ASP.NET Core Web API · EF Core · SQL Server · Onion Architecture

🔹 Overview

A clean and scalable RESTful API for managing library operations including books, members, employees, shelves, payments, and borrowing records.

🔹 Key Features

✅ Onion Architecture with Repository & Unit of Work

✅ CRUD operations for all core entities

✅ DTO mapping with AutoMapper

✅ API documentation & testing with Swagger & Postman

🔹 Core Models

Book → Title, ISBN, PublishedDate, Quantity

Category → Groups books into genres

Author → Linked with multiple books

Publisher → Publishes one or many books

Shelf → Organizes books by physical location

User (Member) → Library member with borrowing history

Employee → Manages library operations

BorrowingRecord → Tracks borrowed & returned books

Payment → Membership fees & late return fines
