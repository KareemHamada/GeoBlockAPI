📖 Overview

GeoBlockAPI is a .NET 8 Web API that uses IPGeolocation.io
 to detect a user’s country by IP address and allows blocking or unblocking specific countries.
The application uses in-memory storage (no database) and provides APIs to:

Block or unblock countries

List blocked countries

Lookup country info by IP

Check if an IP is from a blocked country

View logs of all block check attempts

This project was created as part of the .NET Developer Test Assignment.

⚙️ Technologies Used

.NET 8.0 Web API

ASP.NET Core Controllers

IPGeolocation.io API

In-Memory Collections (ConcurrentDictionary)

Swagger UI for testing

Newtonsoft.Json for JSON parsing

🧱 Architecture
GeoBlockAPI/
 ┣ Controllers/
 ┃ ┣ CountriesController.cs
 ┃ ┣ IpController.cs
 ┃ ┗ LogsController.cs
 ┣ Models/
 ┃ ┣ IpInfo.cs
 ┃ ┣ CountryBlock.cs
 ┃ ┗ BlockedAttemptLog.cs
 ┣ Repositories/
 ┃ ┗ InMemoryRepository.cs
 ┣ Services/
 ┃ ┗ GeoService.cs
 ┣ appsettings.json
 ┗ Program.cs

🚀 Getting Started
1️⃣ Clone the Project
git clone https://github.com/yourusername/GeoBlockAPI.git
cd GeoBlockAPI

2️⃣ Configure API Key

Edit appsettings.json and replace with your real key:

"GeolocationSettings": {
  "BaseUrl": "https://api.ipgeolocation.io/ipgeo",
  "ApiKey": "YOUR_API_KEY"
}


Get a free key here → https://ipgeolocation.io/signup.html

3️⃣ Install Dependencies

In Visual Studio → Tools > NuGet Package Manager > Package Manager Console

Install-Package Microsoft.Extensions.Http
Install-Package Newtonsoft.Json

4️⃣ Run the Project

Press F5 → Swagger opens at
👉 https://localhost:xxxx/swagger/index.html

🧠 Features & Endpoints
🔹 1. Block a Country

    POST /api/countries/block
    Body: "EG"
    Response: 200 OK — "EG blocked successfully."

🔹 2. Unblock a Country

    DELETE /api/countries/block/{countryCode}
    Example: /api/countries/block/EG
    Response: 200 OK — "EG unblocked successfully."

🔹 3. Get All Blocked Countries

    GET /api/countries/blocked
    Response Example:
    
    [
      { "countryCode": "EG", "expiryTime": null },
      { "countryCode": "US", "expiryTime": null }
    ]

🔹 4. Lookup IP Information

    GET /api/ip/lookup?ipAddress={ip}
    Example: /api/ip/lookup?ipAddress=8.8.8.8
    Response Example:
    
    {
      "ip": "8.8.8.8",
      "countryCode": "US",
      "countryName": "United States",
      "isp": "Google LLC"
    }

🔹 5. Check if IP is Blocked

    GET /api/ip/check-block
    Automatically detects caller IP.
    (Localhost fallback = 8.8.8.8 for testing)
    
    Response Example (for Egypt 🇪🇬):
    
    {
      "ip": "41.34.55.1",
      "countryCode": "EG",
      "countryName": "Egypt",
      "isp": "Telecom Egypt",
      "isBlocked": true
    }

🔹 6. View Block Logs

GET /api/logs/blocked-attempts
Shows all IP check attempts:

    [
      {
        "ipAddress": "41.34.55.1",
        "countryCode": "EG",
        "isBlocked": true,
        "userAgent": "Mozilla/5.0",
        "timestamp": "2025-10-19T03:15:42Z"
      }
    ]
