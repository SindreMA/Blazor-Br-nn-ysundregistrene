# Blazor Brønnøysundregistrene

This project is a test of my ASP.NET skills. It features a backend with a task system responsible for fetching data from brreg.no and saving it in the database. The frontend is a simple Blazor app using the MudBlazor UI Library.

## Overview

- Backend with a task system for periodic data retrieval and storage.
- Frontend powered by Blazor and MudBlazor for a straightforward user interface.

## Demo
- **Website:** [brreg.sindrema.com](https://brreg.sindrema.com/)
- **API Documentation:** [brreg.sindrema.com/swagger](https://brreg.sindrema.com/swagger)
- **Tasksystem:** [brreg.sindrema.com/hangfire](https://brreg.sindrema.com/hangfire)


## Task
A simple web interface for "maintenance of businesses" needs to be developed. A business should have at least properties such as organization number, name, address, and contact information such as phone and email.

## Requirements
- The interface should be usable in a web browser.
- The solution should be developed using ASP.NET and C#.
- Data should be stored in a relational database such as PostgreSQL or Microsoft SQL Server.
- "External data" should be presented, which can be retrieved from the Brønnøysund registers using the organization number of the business as a parameter ([Brønnøysund API](https://data.brreg.no/enhetsregisteret/api/enheter/951206091)).
- It is an advantage if the solution is protected with some form of authentication.

## Tools
The following free tools can be used to carry out the task. Feel free to use others if you prefer.

- **Visual Studio** - [Download Link](https://visualstudio.microsoft.com/vs/community/)
- **SQL Server** - [Download Link](https://www.microsoft.com/en-in/sql-server/sql-server-downloads)
- **PostgreSQL** - [Download Link](https://www.postgresql.org/)


## NB!
Note that the contact information ("contact information such as phone and email") were not provided in given API, so this is just left empty with the option to edit in values.
Nothing would stop us from making a lookup service that searched this data from other sources, but this was not the task at hand.

The people data were is fetched from https://data.brreg.no/enhetsregisteret/api/enheter/{orgId}/roller. This is the closest i found to a contact list.



