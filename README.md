This is a sample scenario that simulates vehicles updating their status in a realtime dashboard.

Please check the documentations for [Architecture Overview](docs/architecture.md)

# Getting started
## To Run using prebuilt docker images
1. First make sure docker is installed 
2. Download the file: https://raw.githubusercontent.com/Bishoymly/MonitorV/master/aspnet-core/docker/ng/docker-compose.yml
3. In the same folder run this command: ``` docker-compose up -d ```
4. After completion wait for a minute so that everything is truely up and then browse: http://localhost:9902/ 
5. Login with username: **admin** and password: **123qwe**

## To Build
### Install Prerequisites
- nodejs: https://nodejs.org/en/download/
- yarn: https://yarnpkg.com/lang/en/docs/install/
- dotnet core sdk 2.2.106: https://dotnet.microsoft.com/download/dotnet-core/2.2
- angular cli: ``` npm install -g @angular/cli```
- docker (optional - to build docker images or run prebuilt images without sql server)
- SQL Server (optional - only needed to run without docker)

### Build
- Clone or download this repo obviously

**One build script:**
- Go to folder: aspnet-core\build and run the command: ``` ./build ```
- The final step will try to push docker images to docker hub which needs permission so if it fails there is no problem, as the images are cached on your local machine

**Using Visual Studio:**
- Open the solution: aspnet-core/MonitorV.sln
- Update the database connection string in this file to reflect your sql server credentials: aspnet-core\src\MonitorV.Web.Host\appsettings.json
- Build and run the solution for backend
- Open command prompt at folder: angular
- Run ```yarn``` or ```npm install```
- Run ```npm start```
- Browse: http://localhost:4200/ and login with username: **admin** and password: **123qwe**
