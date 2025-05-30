# open_shift_scheduler
Shift roaster managing software

## Prerequisites
* dotnet 9
* npm
* Visual Studio with required dotnet worksloads installed in Visual studio installer
* PostgreSQL database

## Steps to run the application in visual studio
* Clone the repository branch using command like `git clone --single-branch --branch <branchname> <remote-repo>`
* Open a terminal in the folder wwwroot->js->packages and run `npm install`
* Open a terminal in the folder wwwroot->js->shifts_edit_ui_app and run `npm install` , `npm run build`. This buils the react application used in shifts editing page
* open the sln file in visual studio
* Right click on OSS.Web project and select "Set as Startup Project"
* Right click on OSS.Web project and select "Manage user secrets". Paste the content of appsettings.json and edit it as per requirement
* Set ASPNETCORE_ENVIRONMENT as "Testing" in OSS.Web->Properties->launchSettings.json file to run the application without database (optional. This uses a transient in-memory database)
* Run database migrations, open view->other windows->package manager console. Select the default project as OSS.Infra. Run the command `Update-Database`
* Run the application using the play button in the top menu bar