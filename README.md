# JobTracker API

JobTracker is a simple yet powerful RESTful API designed to help you effortlessly manage your job application process. Built with .NET Core and SQLite, this project is a great learning tool for those diving into full-stack development.

## Features

* **CRUD Operations:** Seamlessly create, read, update, and delete job applications.
* **Application Status Tracking:**  Keep tabs on the progress of each application, whether it's pending, scheduled for an interview, or you've received an offer (or rejection).
* **Comprehensive Job Details:**  Store essential information like job titles, company names, detailed descriptions, application dates, and your desired salary range.
* **Personalized Notes:** Add notes and reminders to each application to stay organized and on top of your job search.
* **SQLite Database:** Leverages a lightweight, file-based database for easy setup and portability, ideal for personal projects.
* **Swagger UI:**  Includes interactive API documentation to make testing and interacting with the API a breeze.

## Getting Started

### Prerequisites

To get started with JobTracker, you'll need the following:

* **.NET SDK:** Download and install the latest .NET SDK from the official Microsoft website: [https://dotnet.microsoft.com/download](https://dotnet.microsoft.com/download)

### Installation & Running

1. **Clone the Repository:**
   ```bash
   git clone https://github.com/<your_username>/JobTrackerApi.git
   ```

2. **Navigate to Project Directory:**
   ```bash
   cd JobTrackerApi
   ```

3. **Start the API:**
   ```bash
   dotnet run
   ```

   By default, the API will be accessible at `https://localhost:7001` or `http://localhost:5001` (or the port you've configured).

4. **Explore with Swagger UI:**
   Open your web browser and navigate to `https://localhost:7001/swagger` or `http://localhost:5001/swagger` to access the interactive Swagger UI documentation. Here you can experiment with API endpoints, view request/response examples, and test your application's functionality.

## API Endpoints

The JobTracker API provides the following RESTful endpoints:

| Method | Endpoint                   | Description                         |
|-------|-----------------------------|-------------------------------------|
| GET    | `/jobapplications`          | Retrieve all job applications              |
| GET    | `/jobapplication/{id}`       | Get details for a specific job application by ID |
| POST   | `/jobapplication`          | Create a new job application         |
| PUT    | `/jobapplication/{id}`       | Update an existing job application by ID      |
| DELETE | `/jobapplication/{id}`       | Delete a job application by ID      |


**Request and Response Examples:**

Refer to the Swagger UI documentation for detailed examples of requests and responses, including JSON schemas and sample data.