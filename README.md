# TodoList Web App

This is a TodoList web application built using React.js for the frontend and ASP.NET Core Web API for the backend.

## Frontend

The frontend is developed with React.js and utilizes styled components for styling.

### Features:
- View, add, edit, and delete Todo items.
- Mark Todo items as completed.
- JWT authentication and role-based authorization.

### Demo Accounts:
1. **Username:** liangfang
   - **Password:** liangfang

2. **Username:** xiao
   - **Password:** xiao

## Backend

The backend is a RESTful API built with ASP.NET Core Web API.

### Features:
- Todo List API with CRUD operations.
- JWT token-based authentication.
- Role-based authorization.

### Demo Users:
- Two demo accounts are available for testing:
  1. **Username:** liangfang
     - **Password:** liangfang
  2. **Username:** xiao
     - **Password:** xiao

### Authentication:
- Obtain a JWT token by sending a POST request to `/api/users/authenticate` with the provided demo credentials.
- Include the token in the Authorization header for authenticated requests.

### Roles:
- Two roles are available: "User" and "Admin."
- Assign the "Admin" role to a user for elevated privileges.

### API Endpoints:

- **POST /todolist/createtask**
  - Create a new task.

- **GET /todolist/gettask**
    - Retrieves a list of all Todo items, user specific.

- **GET /todolist/gettaskbyfilterorsort**
  - Retrieves a list of all Todo items based on filter and sort date, user specific

- **PUT /todolist/updatetask**
  - Updates an existing Todo item and add new user (to share).

- **DELETE /todolist/deletetask**
  - Deletes a Todo item.

### Note:
This application is a demo and currently supports a limited set of functionalities. It is intended for educational and testing purposes.
