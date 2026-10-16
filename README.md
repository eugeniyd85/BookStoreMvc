# BookStoreMvc: Online Bookstore on ASP.NET Core MVC

A learning e-commerce web application built with ASP.NET Core MVC (.NET 9). Customers browse books, fill a shopping cart and pay online with Stripe, while admins and employees manage products, orders and users. It uses ASP.NET Core Identity with role-based access, Entity Framework Core with SQL Server, and a repository / unit-of-work data layer.

Built while following the [.NET Core MVC - The Complete Guide 2026 [E-commerce][MVC]](https://www.udemy.com/course/complete-aspnet-core-21-course/) course.

## Features

**Customers**
- Browse the book catalog and open a product page with an image gallery
- Tiered pricing: the unit price depends on quantity (1–49, 50–99, 100+)
- Shopping cart with a live item counter (session + view component)
- Checkout with **Stripe Checkout** and an order confirmation page
- Register and log in with email and password or with Facebook

**Company customers**
- Delayed payment option: the order is approved first and paid later (payment due date)

**Admin and Employee**
- Orders: filter by status, edit shipping details, start processing, ship with carrier and tracking number, cancel with an automatic **Stripe refund**, pay for a delayed-payment order
- Categories and products: full CRUD, multiple image upload per product, rich-text description (TinyMCE)
- Companies: full CRUD
- Users: change roles and company assignment, lock / unlock accounts

**Roles:** `Customer`, `Company`, `Employee`, `Admin`

## Tech Stack

| Area | Technology |
|---|---|
| Framework | .NET 9, ASP.NET Core MVC with Areas (Admin, Customer, Identity) |
| Database | SQL Server, Entity Framework Core 9 (code-first migrations) |
| Auth | ASP.NET Core Identity, roles, Facebook external login |
| Payments | Stripe.net (Checkout sessions, refunds) |
| Patterns | Repository, Unit of Work, ViewModels, View Components, DbInitializer (seed) |
| Frontend | Razor views, Bootstrap, jQuery, DataTables, SweetAlert2, Toastr, TinyMCE |

## Solution Structure

```
BookStoreMvc/                Web app: controllers, views, areas, wwwroot
  Areas/Admin/               Products, categories, companies, users, orders
  Areas/Customer/            Home (catalog, details) and Cart
  Areas/Identity/            Scaffolded Identity pages (login, register, manage)
BookStoreMvc.DataAccess/     DbContext, migrations, repositories, UnitOfWork, DbInitializer
BookStoreMvc.Models/         Entities and ViewModels
BookStoreMvc.Utility/        Constants (roles, statuses), Stripe settings, email sender
BookStoreRazor/              Small practice project: Razor Pages CRUD for categories
```

## Getting Started

**Prerequisites:** [.NET 9 SDK](https://dotnet.microsoft.com/download), SQL Server (LocalDB, Express or Docker), a [Stripe](https://stripe.com) account in test mode.

1. Clone the repository.
2. Set the SQL Server connection string in `BookStoreMvc/appsettings.json` (see below).
3. Add your Stripe **test** keys (see below).
4. Run the app:

   ```bash
   dotnet run --project BookStoreMvc
   ```

5. Open the URL printed in the console (for example `https://localhost:7xxx`).

On startup the app applies pending EF Core migrations and, on the first run, creates the four roles and a demo admin account.

### Configuration

| Key | Purpose |
|---|---|
| `ConnectionStrings:DefaultConnection` | SQL Server connection string, e.g. `Server=localhost;Database=BookStoreDb;Trusted_Connection=True;TrustServerCertificate=True` |
| `Stripe:SecretKey` | Stripe secret key (use a **test** key, `sk_test_...`) |
| `Stripe:PublishableKey` | Stripe publishable key (`pk_test_...`) |

Keep keys out of source control, for example with [user secrets](https://learn.microsoft.com/aspnet/core/security/app-secrets):

```bash
cd BookStoreMvc
dotnet user-secrets init
dotnet user-secrets set "Stripe:SecretKey" "sk_test_..."
dotnet user-secrets set "Stripe:PublishableKey" "pk_test_..."
```

### Demo Account (local development only)

| Email | Password | Role |
|---|---|---|
| `admin@yevhensapp.com` | `Admin123!` | Admin |

Change or remove this account before deploying anywhere public.

### Trying a Payment

In Stripe test mode use the card number `4242 4242 4242 4242` with any future expiry date, any CVC and any postal code.

### Managing Migrations

```bash
# from the solution folder
dotnet ef migrations add <Name> --project BookStoreMvc.DataAccess --startup-project BookStoreMvc
dotnet ef database update --project BookStoreMvc.DataAccess --startup-project BookStoreMvc
```

## What I Practiced

- Structuring a multi-project ASP.NET Core MVC solution with Areas
- Generic repository and Unit of Work patterns on top of EF Core
- Code-first migrations, seed data and an initializer that creates roles and an admin user
- Authentication and authorization with ASP.NET Core Identity, roles and external login
- Integrating Stripe Checkout and refunds, including a delayed-payment flow for company users
- File upload and image management, session-based state, view components
- Server-side filtering with DataTables and AJAX endpoints

## Known Limitations / Ideas for Improvement

- Known bugs: an empty cart can still go to payment; quantity can be set to a negative number; some optional order fields are not shown after a page reload when cleared; the cart counter can be stale after login until the Home page is opened.
- `EmailSender` is a stub: no emails are sent (SendGrid integration is not implemented).
- Facebook login credentials should be added to user secrets or environment variables.
- Migration errors in `DbInitializer` are swallowed silently; they should be logged.
- Add unit and integration tests.
- Add a `docker-compose` file (app + SQL Server) for a one-command setup.
