**1.  Clone or Download the Project**

Clone the project from GitHub: `git clone https://github.com/iamantoon/inforce-url-shortener.git` or download the ZIP and extract it.

**2. Set Up the Database**

Ensure PostgreSQL is installed and running.
Create a new database: `CREATE DATABASE Urls;`

**3. Configure Database Connection**

For example, In the `appsettings.json` file, set the connection string: 

`
"ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=Urls;Username=postgres;Password=password"
}
`

**4. Run the Project**

Open a terminal, navigate to the project folder: `cd path/to/UrlShortener`

Run the project: `dotnet run`

The app should be available at http://localhost:5000
