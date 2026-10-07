# CHUSHKA

CHUSHKA (Central Hierarchically-Universal Sales Host Kickstarter Application) is a simple ASP.NET Core web app for selling products. Users can browse and order products, while admins can manage products and view all orders.

## Features

**Guest can:**
- Register and login
- View the home page

**User can:**
- View products and product details
- Order products
- Logout

**Admin can:**
- Create, edit and delete products (name, price, description, type)
- View all orders
- Logout

The first registered user automatically becomes **Admin**, everyone after that gets the **User** role.

## Tech

- ASP.NET Core MVC (.NET 8)
- ASP.NET Core Identity (login, register, roles)
- Entity Framework Core
- SQL Server LocalDB
- Visual Studio 2022

## Project Structure

| Folder / File | Purpose |
|---|---|
| `CHUSHKA.sln` | Visual Studio solution |
| `CHUSHKA/Controllers` | `HomeController`, `ProductsController`, `AdminController` |
| `CHUSHKA/Models` | `Product`, `Order`, `ApplicationUser` |
| `CHUSHKA/Views` | Razor views for products, admin panel and layout |
| `CHUSHKA/Areas/Identity` | Login, register and logout pages |
| `CHUSHKA/Data` | `ApplicationDbContext` |
| `CHUSHKA/Migrations` | Database migrations |
| `CHUSHKA/appsettings.json` | Database connection string |

## How to Run

1. Clone the repository and open `CHUSHKA.sln` in Visual Studio 2022.
2. Restore NuGet packages (Visual Studio does this automatically on build).
3. Create the database. The migrations are already included, so in **Package Manager Console** run:
   ```
   Update-Database
   ```
4. Press **F5** to run the project.
5. Register an account — the first one becomes Admin.

Without Visual Studio (needs the .NET 8 SDK and SQL Server LocalDB):

```
dotnet tool install --global dotnet-ef
dotnet ef database update --project CHUSHKA
dotnet run --project CHUSHKA
```

## License

This project is licensed under the [MIT License](LICENSE).

## Links

Repository: https://github.com/Danmanbg/CHUSHKA
