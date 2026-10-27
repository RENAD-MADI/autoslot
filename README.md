# AutoSlot — Car Service Booking

<div dir="rtl">

نظام حجز مواعيد صيانة وغسيل السيارات: تسجيل حساب، حجز موعد لخدمة معيّنة، ومتابعة الحجوزات وتعديلها وإلغاؤها. مشروع جامعي لمادة CPIS358.

</div>

A car-service booking system built with ASP.NET Core MVC for the CPIS358 course. Customers create an account, book a time slot for a service (car wash, oil change, brake inspection, …) and manage their own bookings.

![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core_MVC-8.0-512BD4?logo=dotnet)
![EF Core](https://img.shields.io/badge/EF_Core-8-512BD4)
![SQL Server](https://img.shields.io/badge/SQL_Server-CC2927?logo=microsoftsqlserver&logoColor=white)

![Home](docs/home.jpg)

## Features

- Sign up and log in (session based); passwords are hashed.
- Book a service with car details, date and time.
- **No double booking:** a date + time slot can only be taken once, and bookings must be in the future.
- My bookings: list, edit and cancel. Users only ever see and change their own bookings.
- Service price list and an About page.

## Run it

Requirements: .NET 8 SDK and SQL Server LocalDB (installed with Visual Studio).

```bash
cd AutoSlotMVC
dotnet run
```

The database is created automatically on first run (EF Core migrations).

## Project structure

```
AutoSlotMVC/
├── Controllers/HomeController.cs   Accounts and bookings
├── Models/                         User, Booking, ApplicationDbContext
├── Migrations/                     EF Core schema
└── Views/Home/                     Pages
docs/phase-1-static/                First phase of the course project (HTML/CSS/JS only)
```

## What changed from the course submission

- Added the EF Core migration; the database was never created before.
- Passwords are hashed instead of stored as plain text.
- Bookings belong to the user who made them. Before this, anyone could list, edit or delete every booking without logging in.
- Delete is a POST with anti-forgery protection instead of a link; all forms validate input.
- Added duplicate-email and double-booking checks and the missing error page.
- Dates now bind correctly on machines set to the Hijri calendar.

## Author

**Renad Madi** — [GitHub](https://github.com/RENAD-MADI)
