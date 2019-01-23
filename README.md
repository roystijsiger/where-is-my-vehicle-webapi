# Where is my vehicle
## Web API
**This project provides a RESTfull webapi used in [Where is my vehicle Android](https://github.com/roystijsiger/where-is-my-vehicle-android).**
### [Swagger docs](http://whereismyvehicle.azurewebsites.net/swagger/index.html)

### Development instructions
- Install [Visual Studio 2017](https://visualstudio.microsoft.com/vs/)
- Install [.net core 2.2.*](https://dotnet.microsoft.com/download/dotnet-core/2.2)
- Open .sln file in Visual Studio and start project
- Run `update-database` to migrate local database (connectionstring is set in `appsettings.Development.json`)

### Deployment instructions
- Add connectionstring `WhereIsMyVehicleConnectionString` to `appsettings.json`
- Download publish profile from Azure webservice
- Right click web project > publish
- Load publish profile
- Configure
  - Enable migrations (insert production connection string)
- Publish
- Go to domain.com/swagger to check if the api is running and test endpoints
