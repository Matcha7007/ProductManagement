# 🛒 Product Management

A mini product management web application built with ASP.NET Core 8 (Web API) using CQRS pattern and PostgreSQL for data persistence. Includes user authentication using JWT and a simple frontend built with React and Bootstrap for managing products.

---

## ✨ Features

### 🔐 Authentication & Authorization
- JWT-based authentication.
- User registration and login endpoints.
- Role-based protected endpoints.

### 📦 Product Management
- CRUD (Create, Read, Update, Delete) operations for products.
- Product fields: `Name`, `Description`, `Price`.

### 🔍 Search & Filtering
- Search by product name.
- Filter by price range.
- Pagination and sorting support.

### ✅ Validation
- DataAnnotations-based validation for all request models.

### 🧾 Logging & Exception Handling
- Structured logging using **Serilog**.
- Centralized error handling with meaningful HTTP responses.

### 🖥️ Frontend
- React app with Bootstrap UI.
- Product listing and form for add/edit.
- Integrated with backend API.

---

## 🚀 Tech Stack

| Layer         | Tech Stack                    |
|---------------|-------------------------------|
| Backend       | ASP.NET Core 8, CQRS, Serilog |
| Database      | PostgreSQL                    |
| Auth          | JWT                           |
| Frontend      | React + Bootstrap             |
| Architecture  | REST API, Clean/CQRS Pattern  |

---

## 📌 Assumptions

- Users can only register and log in to obtain an authentication token.
- Product data can only be accessed after logging in (authentication token is required).
- Basic form validations are implemented, such as:
  - Product name must not be empty.
  - Product price must be a positive number.
- There is no user role grouping (e.g., admin vs regular user).
- The frontend UI is kept simple and minimal, following the scope requirements.

- The application can be deployed on a Linux VM on Azure.
The IP address will be shared via email to prevent any unwanted access.

---

## 🧪 Postman Collection

Use this Postman collection to test all available endpoints:

### 🔑 Auth Endpoints

| Method | Endpoint        | Description         |
|--------|------------------|---------------------|
| POST   | `/register`      | Register new user   |
| POST   | `/login`         | Login and get token |
| GET    | `/is-online`     | Check auth status   |

### 📦 Product Endpoints

| Method | Endpoint        | Description            |
|--------|------------------|------------------------|
| GET    | `/is-online`     | Check product service  |
| POST   | `/create`        | Create product         |
| POST   | `/update`        | Update product         |
| POST   | `/delete`        | Delete product         |
| POST   | `/get-by-id`     | Get product by ID      |
| POST   | `/search`        | Search with filters    |

> Variables used in Postman collection:
> - `{{BaseUrlAuth}}`
> - `{{BaseUrlProduct}}`
> - `{{BearerToken}}`
> - `{{ProductId}}`

---

## ⚙️ Setup Instructions

### 1. Clone the repository

```bash
git clone https://github.com/Matcha7007/ProductManagement.git
cd ProductManagement
```

### 2. Ensure docker installed
If you haven't installed Docker yet, please install it first via the following link:
👉 https://docs.docker.com/get-started/get-docker/

Once Docker is ready, run the following command:

### 3. Run the App with Docker Compose
```bash
docker compose up --build
```

## Authors
Ilham [@Matcha7007](https://www.github.com/matcha7007)
