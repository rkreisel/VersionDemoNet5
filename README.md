# Versioning Demo in C# and Swashbuckle #

## [Introduction](#Introduction)

Host multiple API versions in the same code base

## [Multiple API Versions in the same code](#multiple-api-versions)

A demonstartion of multiple versions of an API in the same deployable unit. 

### Nuget Packages

`Swashbuckle.AspNetCore`

## Change Detail

### Startup.cs

* MajorVersion* - Constants for versions
* AddSwaggerGen() - add one line per version
* UseSwaggerUI() - add one line per display version including the "Common" API operations

Team specific information can be inlcuded using `new OpenApiInfo()` (like contacts and email adresses) that will be displayed on the swagger page. In this example a little helper called `MakeOpenApiInfo()` can be used to ensure consistancy. 

### Controllers


For each controller for a specifc version add in the group name that matches the versions in the `MajorVersion*` constants above using the `GroupName` attribute.
In general the `[Route()]` element at top of the controller should define the version root, and the path to the operations (methods) domain. As above.

```C#
[ApiController]
[ApiExplorerSettings(GroupName = "v1")]
[Route("/v1/dummy")]
public class DummyControllerV1 : ControllerBase { ... }
```
The "sub-path" is expressed on each operation in the `[Http*()]` attribute (see examples in controllers)

```C#
[HttpGet("list")]
[ProducesResponseType(StatusCodes.Status200OK)]
public IActionResult DummyList() ...;
```

## [Other API Best Practices](#best-practices)

### Common Endpoint Conventions

Plus, how to document the `common` Version endpoints:
* `GET /` to return the semantic version string
* `GET /version` to return a JSON payload with additional version information (this can be customized)
* `GET /health` used by monitoring systems to determine the health of your API (see comments in method `CheckHealth()`)


### Centralized Error Handling for Normalizing Error Results

It is useful to have one centralized error handler that will format and standarize exceptions that happen in code, this is also useful for ensuring exceptions do not result in the disclosure of information we might not to share with the consumers of the API.

```C#
_ = services.AddMvc(
    config =>
    {
        config.Filters.Add(typeof(Libs.GlobalExceptionFilter));
    });
```

Teams should modify the implementation of the handler for their specific exceptions, so they can remove any exception handling code from their methods for any exceptions that can not be handled from with-in the method e.g. recoverable exceptions, not terminal ones.

See:
* `libs/GlobalExceptionFilter.cs` for a sample implementation
* `libs/TypeSwitch.cs` is a handy set of classes for doing a switch statement on types

### Code Quality and Style 

1. Code Quality is via in the inclusion of the *NuGet* package `Microsoft.CodeQuality.Analyzers` to give in VS or VS-Code feedback. This can be supplimented with SonarQube, etc.
2. Code Style is set via an `.editorconfig` that has useful style rules in it that impact maintainablitily. Some teams have used `StyleCop` but it is very opinionated, and may be much more than is useful.
3. Security code scan via https://security-code-scan.github.io/ adding the package `SecurityCodeScan`

## [References](#references)

* https://github.com/domaindrivendev/Swashbuckle.AspNetCore#generate-multiple-swagger-documents
* https://docs.microsoft.com/en-us/azure/azure-monitor/app/asp-net-core