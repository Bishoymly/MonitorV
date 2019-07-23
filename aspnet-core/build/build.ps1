# COMMON PATHS

$buildFolder = (Get-Item -Path "./" -Verbose).FullName
$slnFolder = Join-Path $buildFolder "../"
$outputFolder = Join-Path $buildFolder "outputs"
$webHostFolder = Join-Path $slnFolder "src/MonitorV.Web.Host"
$simulatorFolder = Join-Path $slnFolder "src/MonitorV.VehicleSimulator"
$ngFolder = Join-Path $buildFolder "../../angular"

## CLEAR ######################################################################

Remove-Item $outputFolder -Force -Recurse -ErrorAction Ignore
New-Item -Path $outputFolder -ItemType Directory

## RESTORE NUGET PACKAGES #####################################################

Set-Location $slnFolder
dotnet restore

## PUBLISH WEB HOST PROJECT ###################################################

Set-Location $webHostFolder
dotnet publish --output (Join-Path $outputFolder "Host")

## PUBLISH SIMULATOR PROJECT ###################################################

Set-Location $simulatorFolder
dotnet publish --output (Join-Path $outputFolder "Simulator")

# Change SIMULATOR configuration
$simulatorConfigPath = Join-Path $outputFolder "Simulator/appsettings.json"
(Get-Content $simulatorConfigPath) -replace "localhost:21021", "abp_host:80" | Set-Content $simulatorConfigPath

## PUBLISH ANGULAR UI PROJECT #################################################

Set-Location $ngFolder
& yarn
& ng build --prod="true"
Copy-Item (Join-Path $ngFolder "dist") (Join-Path $outputFolder "ng") -Recurse
Copy-Item (Join-Path $ngFolder "Dockerfile") (Join-Path $outputFolder "ng")
Copy-Item (Join-Path $ngFolder "default.conf") (Join-Path $outputFolder "ng")

# Change UI configuration
$ngConfigPath = Join-Path $outputFolder "ng/assets/appconfig.production.json"
(Get-Content $ngConfigPath) -replace "21021", "9901" | Set-Content $ngConfigPath
(Get-Content $ngConfigPath) -replace "4200", "9902" | Set-Content $ngConfigPath

## CREATE DOCKER IMAGES #######################################################

# Host
Set-Location (Join-Path $outputFolder "Host")

docker rmi bishoymly/host -f
docker build -t bishoymly/host .

# Simulator
Set-Location (Join-Path $outputFolder "Simulator")

docker rmi bishoymly/simulator -f
docker build -t bishoymly/simulator .

# Angular UI
Set-Location (Join-Path $outputFolder "ng")

docker rmi bishoymly/ng -f
docker build -t bishoymly/ng .

## DOCKER COMPOSE FILES #######################################################

Copy-Item (Join-Path $slnFolder "docker/ng/*.*") $outputFolder

## FINALIZE ###################################################################

Set-Location $outputFolder

docker push bishoymly/host
docker push bishoymly/simulator
docker push bishoymly/ng
