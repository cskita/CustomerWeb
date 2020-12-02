# CustomerWeb

**_CustomerWeb_** is an ASP.NET MVC Core case solution create to implement a login/ authorization pages using Json Web Token (JWT) and to demonstrate the behavior of a page list with filters in MVC/ Razor.

## Design Patterns

Model-View-Controller

## Solution Projects

| Project | Application Layer |
| :--- | :---
| CustomerWeb | (all) |

## Technologies

| Dependency | Version
| :--- | ---:
| .NET Core | 3.1
| ASP.NET MVC Core | 2.2.5
| AutoMapper | 10.1.1
| Bootstrap | 4.3.1
| C# | 8
| jQuery | 3.3.1
| jQuery Validation | 1.17.0
| Microsoft jQuery Unobtrusive Validation | 3.2.11
| Microsoft VisualStudio Web CodeGeneration Design | 3.1.4

## Getting Started

1. Download or clone this repository.
1. Open the solution in Visual Studio 2017 or higher.
1. Select the **CustomerWeb** project.
1. Open the _appsettings.Development.json_ file in the project root and update the value of `EndPointUrl` for the `CustomerAPI` URL to point to a API that exists on your local machine.
1. Run the application.

For run the Rest API uses in this project you need to download or clone the project [CustomerAPI](https://github.com/cskita/CustomerAPI).

## Configuration

* The project contains a configuration which may require modification for the developer's specific environment:

    | Project | File
    | :--- | :---
    | CustomerWeb | appsettings.json
    | CustomerWeb | appsettings.Development.json

* The configuration string specifies the target of API Rest: `https://localhost:44341/api`. Developers using a different target will have to change the EndPointUrl of CustomerAPI.
