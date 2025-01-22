**README.md Template**


# URL Shortener Service

## Overview
This project is a simple URL Shortening service built with **.NET/C#**. It allows users to:
1. Shorten a long URL into a short, fixed-length URL.
2. Retrieve the original URL using the short URL.
3. View access statistics for a given short URL.

---

## Features
- **Shorten a URL**: Converts long URLs into unique, short URLs.
- **Retrieve a URL**: Redirects a short URL to its original long URL.
- **Access Statistics**: View the number of times a short URL has been accessed.
- **Rate Limiting**: Limits requests to prevent abuse.
- **Caching**: In-memory caching for faster URL retrieval.
- **High Performance**: Designed to handle up to 1,000,000 operations per day.

---

## API Endpoints
1. **POST /api/urlshortener/shorten**  
   - **Description**: Accepts a long URL and returns a shortened URL.  
   - **Request Body**:  
     ```json
     {
       "longUrl": "https://example.com"
     }
     ```  
   - **Response**:  
     ```json
     {
       "shortUrl": "abc123"
     }
     ```

2. **GET /api/urlshortener/{shortUrl}**  
   - **Description**: Redirects to the original long URL corresponding to the short URL.

3. **GET /api/urlshortener/stats/{shortUrl}**  
   - **Description**: Retrieves statistics for a given short URL.  
   - **Response**:  
     ```json
     {
       "shortUrl": "abc123",
       "accessCount": 10
     }
     ```

---

## Technology Stack
- **Backend**: ASP.NET Core
- **Database**: Microsoft SQL Server
- **Caching**: In-Memory Cache
- **Rate Limiting**: AspNetCoreRateLimit

---

## Database Schema
The `UrlMappings` table structure:
| Column Name    | Type         | Description                  |
|----------------|--------------|------------------------------|
| `Id`           | Integer      | Primary key                 |
| `LongUrl`      | String       | Original long URL           |
| `ShortUrl`     | String       | Generated short URL (unique)|
| `CreatedAt`    | DateTime     | Timestamp of creation       |
| `AccessCount`  | Integer      | Number of times accessed    |

---

## How to Run Locally
### Prerequisites
- Install [.NET SDK 8.0](https://dotnet.microsoft.com/download/dotnet/8.0)
- Install [Docker](https://www.docker.com/)

### Steps
1. Clone the repository:
   ```bash
   git clone <your-repository-link>
   cd UrlShortenerService
   ```

2. Set up the database:
   - Update the connection string in `appsettings.json`.

3. Run the application locally:
   ```bash
   dotnet run
   ``

4. Access the API via:
   - Swagger UI: `http://localhost:8080/swagger`

---

## Scalability and Performance
- **Scalability**:
  - Use Redis for distributed caching.
  - Add horizontal scaling with a load balancer.
  - Shard database storage for large-scale use.
- **High Availability**:
  - Deploy on cloud platforms like Azure or AWS.
  - Use replicated databases and failover mechanisms.
- **Security**:
  - Input validation to prevent XSS and SQL injection.
  - API authentication using API keys or tokens.
  - Enforce HTTPS.

---

## Testing
1. Access the Swagger UI at `http://localhost:8080/swagger`.
2. Test each endpoint:
   - **POST /shorten**: Provide a valid long URL and check the response.
   - **GET /{shortUrl}**: Use the returned short URL and check redirection.
   - **GET /stats/{shortUrl}**: Verify the access count updates correctly.

---

## License
This project is licensed under the MIT License.
```

---

### **2. Testing on Swagger**

Follow these steps to test on Swagger:

1. **Run the Application**:
   - If running locally:
     ```bash
     dotnet run
     ```

2. **Open Swagger UI**:
   - Navigate to `http://localhost:8080/swagger` in your browser.

3. **Test the Endpoints**:
   - **POST /shorten**:
     - Click on the endpoint in Swagger.
     - Provide a sample long URL in the request body:
       ```json
       {
         "longUrl": "https://example.com"
       }
       ```
     - Execute the request and note the short URL in the response.

   - **GET /{shortUrl}**:
     - Use the short URL returned by the previous step.
     - Verify the response redirects to the original URL.

   - **GET /stats/{shortUrl}**:
     - Provide the short URL in the path parameter.
     - Check the `accessCount` field in the response.

---

Let me know if you face any issues while testing or need further adjustments!
