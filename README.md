# Patient Tracking System - C# WinForms Clinic Management 🏥

[![.NET Framework](https://img.shields.io/badge/.NET_Framework-4.8-512BD4?style=for-the-badge&logo=.net)](https://dotnet.microsoft.com/en-us/download/dotnet-framework/net48)
[![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![MySQL](https://img.shields.io/badge/MySQL-4479A1?style=for-the-badge&logo=mysql&logoColor=white)](https://www.mysql.com/)

A robust desktop application developed with C# and Windows Forms for efficient management of patient records, appointments, and administrative tasks in a clinical environment.

---

## 📝 Table of Contents

- [📌 Project Overview](#-project-overview)
- [✨ Core Functionality](#-core-functionality)
- [📸 Application Screenshots](#-application-screenshots)
- [🛠️ Technical Architecture](#️-technical-architecture)
- [📂 File Structure](#-file-structure)
- [🚀 Getting Started](#-getting-started)
  - [Prerequisites](#prerequisites)
  - [Database Setup](#database-setup)
  - [Installation Guide](#installation-guide)
- [🤝 Contributing](#-contributing)

---

## 📌 Project Overview

The **Patient Tracking System** is a comprehensive Windows desktop application designed to streamline the daily operations of small to medium-sized clinics or medical practices. It replaces manual, paper-based processes with a centralized digital system. The application features distinct user roles and interfaces for general staff and administrators, ensuring secure and organized access to sensitive patient data. The primary goal is to improve data accuracy, reduce administrative overhead, and enhance patient care coordination.

---

## ✨ Core Functionality

This application is divided into several key modules, each providing specific functionalities:

-   **Secure Login (`giris_ekrani`):** A dedicated login screen to authenticate users and grant access based on their roles.
-   **Patient Management (`hasta_ekrani`):** A complete CRUD (Create, Read, Update, Delete) interface for managing patient records. Staff can register new patients, search for existing ones, and update their personal and medical information.
-   **Procedure & Appointment Management (`islem_ekrani`):** Allows staff to log treatments, schedule appointments, and manage the services provided to each patient.
-   **Administrator Dashboard (`yonetici_ekrani`):** A restricted-access panel for administrators to oversee operations, manage user accounts, and view system-level data.
-   **Database Interface (`db_ekrani`):** A utility screen for direct interaction with the database, likely used for administrative tasks and data verification.

---

## 📸 Application Screenshots

---

## 🛠️ Technical Architecture

The application is built on the reliable and mature Microsoft .NET ecosystem, ensuring stability and performance.

-   **Platform:** Windows Desktop
-   **Framework:** .NET Framework 4.8
-   **Language:** C#
-   **User Interface:** Windows Forms (WinForms)
-   **Database:** MySQL
-   **Connectivity:** `MySql.Data` ADO.NET driver for seamless database communication.
-   **Data Handling:** Utilizes `DataTable` and `MySqlDataAdapter` for efficient data retrieval and manipulation.

---

## 📂 File Structure

The project's source code is organized logically to separate different parts of the application.

```
Hasta_takip/
├── Properties/             # Project settings and resources
├── Resources/              # Image assets used in the application
├── bin/
│   └── Debug/              # Compiled application and dependencies
├── obj/
├── App.config              # Application configuration (including connection string)
├── giris_ekrani.cs         # Login Screen (View + Logic)
├── hasta_ekrani.cs         # Patient Screen (View + Logic)
├── islem_ekrani.cs         # Procedure Screen (View + Logic)
├── yonetici_ekrani.cs      # Administrator Screen (View + Logic)
├── db_ekrani.cs            # Database Screen (View + Logic)
├── Program.cs              # Main entry point of the application
└── Hasta_takip.csproj      # C# project file
```

---

## 🚀 Getting Started

Follow these instructions to set up and run the project on your local machine.

### Prerequisites

-   **Visual Studio:** 2019 or 2022 with the ".NET desktop development" workload installed.
-   **.NET Framework:** Version 4.8 Developer Pack.
-   **MySQL Server:** A running instance of MySQL (e.g., via MySQL Community Server, XAMPP, or WAMP).

### Database Setup

1.  Create a new database (schema) in your MySQL server (e.g., `hasta_takip_db`).
2.  Create the necessary tables for patients, users, procedures, etc. *(You will need to create these tables based on the application's C# code, as a schema file is not provided.)*
3.  Ensure you have a user with privileges to access this database.

### Installation Guide

1.  **Clone the Repository:**
    ```bash
    git clone [https://github.com/your-username/Hasta_takip.git](https://github.com/your-username/Hasta_takip.git)
    cd Hasta_takip
    ```

2.  **Configure the Connection String:**
    -   Open the `App.config` file in the project root.
    -   Find the `<connectionStrings>` section.
    -   Update the `server`, `database`, `user id`, and `password` attributes to match your local MySQL setup.
    ```xml
    <connectionStrings>
        <add name="Hasta_takip.Properties.Settings.hastaneConnectionString"
             connectionString="server=localhost;database=your_database_name;user id=your_username;password=your_password"
             providerName="MySql.Data.MySqlClient" />
    </connectionStrings>
    ```

3.  **Open and Build in Visual Studio:**
    -   Open the `Hasta_takip.csproj` or the solution file in Visual Studio.
    -   Visual Studio should automatically restore the NuGet packages listed in `packages.config`. If not, right-click the solution and select "Restore NuGet Packages".
    -   Build the solution by pressing `Ctrl+Shift+B`.

4.  **Run the Application:**
    -   Press `F5` or click the "Start" button in Visual Studio to run the project. The login screen (`giris_ekrani`) should appear.

---

## 🤝 Contributing

Contributions are what make the open-source community such an amazing place to learn, inspire, and create. Any contributions you make are **greatly appreciated**.

1.  **Fork** the Project.
2.  Create your Feature Branch (`git checkout -b feature/AmazingFeature`).
3.  Commit your Changes (`git commit -m 'Add some AmazingFeature'`).
4.  Push to the Branch (`git push origin feature/AmazingFeature`).
5.  Open a **Pull Request**.
