## Synopsis

Assignmnet has two parts 
1. ASP.Net Web API
2. MVC Web App

The Web API hosts and exposes  record processor/parser.

The Web App ( MVC) consumes Web API faciltaing :

* Adding of new passenger records
* Seraching through existing passenger records ( Wild card search supported *?-)
* Listing of passenger records (Paging support)

## Installation/Setup Guide 

The following section breifs about setting and running code from user local machine.
### Web API

* Download the code form the repository.
* Browse to the WebApi/Src/VedaleonWebAPI.sln
* Open the solution file in VS 2015 & build 
* Press 'F5' to run
* "http://localhost:56668/help" will open up in the browser.

### Web App

* Download the code from the repository.
* Browse to the WebApp/Src/PassengersPortal.sln
* Open the solution file in VS 2015 & build
* Press 'F5' to run
* "http://localhost:62690/" will open up in the browser.
* Application landing page shows the passenger name list in the desired format.
* Using the menu bar end-user can add/search across existing records. 

## Configuration

* Web App runs on a file named "SampleInput.txt" (Content file) residing inside the "InputSource"  folder ( part of .csProj file )
	```
	 1ARNOLD/NIGELMR-B2 .L/LVGVUP
     .R/TKNE HK1 9244501028078/1
     1ATKINSON/KARENMRS-M2 .L/LVKBTB
     .R/TKNE HK1 9244501227666/1
	```
* Web App "web.config" file has the following key defined and should aptly point at the Web API
  ```
   <add key="WebAPIBaseUrl" value="http://localhost:56668" />
  ```
## API Reference

* Ninject is used as the Dependency resolver tool.
* Log4net is used for logging inside the Web API.
* Wild card search implementation is based on following resource http://www.henrikbrinch.dk/Blog/2013/03/07/Wildcard-Matching-In-C-Using-Regular-Expressions
* Web Api Help pages are generated using Microsoft.AspNet.WebApi.HelpPage package
* 

### Web API

### Web API
Depending on the size of the project, if it is small and simple enough the reference docs can be added to the README. For medium size to larger projects it is important to at least provide a link to where the API reference docs live.

## Tests

Both the Web API & Web App solutions has Test projects for testing the basic operations.

### Web API Test

* Allows for the Testing of the Passenger API controller 

### Web App Test

* Allows for the Testing of method on the Home Controller ( add, search, list)
* The app.config file needs to aptly set "WebAPIBaseUrl" to point the Web API
* The Add/Search/List operation are performed on a local "SampleTestInput.txt" ( content file) residing inside the "InputSource" folder of the project. 


And that's all folks! :+1: