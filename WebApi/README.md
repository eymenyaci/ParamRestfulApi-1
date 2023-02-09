# Project: Best Practices Middleware, Extension Methods, Logging, and JWT Token Usage

This project implements best practices for using middleware, extension methods, logging, mediator, and JWT token authentication in an ASP.NET Core project.

## Features

- Code-first approach using Entity Framework Core (EFCore) for creating migrations for the log and book classes.
- A custom middleware to handle global exceptions and log simple "Action Invoked" messages. Data is added to the database through the context class inside the middleware.
- Token-based authentication using JWT tokens, with the IdentityService class handling the userName and password variables to generate and validate tokens. Requests without a valid token will result in a 401 Unauthorized error.
- BookService endpoint filtering based on the Author and BookName values, using a custom extension method for filtering. Although this could also be done using service-level methods, using extensions was chosen as a best practice.
