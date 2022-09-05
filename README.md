# PowerUtils.Results

![Logo](https://raw.githubusercontent.com/TechNobre/PowerUtils.Results/main/assets/logo/logo_128x128.png)

***Wrapper to transfer results or errors between layers or resources***

![Tests](https://github.com/TechNobre/PowerUtils.Results/actions/workflows/tests.yml/badge.svg)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=TechNobre_PowerUtils.Results&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=TechNobre_PowerUtils.Results)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=TechNobre_PowerUtils.Results&metric=coverage)](https://sonarcloud.io/summary/new_code?id=TechNobre_PowerUtils.Results)

[![NuGet](https://img.shields.io/nuget/v/PowerUtils.Results.svg)](https://www.nuget.org/packages/PowerUtils.Results)
[![Nuget](https://img.shields.io/nuget/dt/PowerUtils.Results.svg)](https://www.nuget.org/packages/PowerUtils.Results)
[![License: MIT](https://img.shields.io/github/license/TechNobre/PowerUtils.Results.svg)](https://github.com/TechNobre/PowerUtils.Results/blob/main/LICENSE)


- [Support](#support-to)
- [How to use](#how-to-use)
  - [Install NuGet package](#Installation)
  - [Return results](#return-results)
    - [Success](#return-results-success)
    - [Errors](#return-results-errors)
      - [Add more errors](#return-results-errors-add)
  - [Extensions](#return-extensions)
    - [OfTypeFirstError](#return-extensions-OfTypeFirstError)
- [How is this different from error-of?](#how-is-different)
- [Contribution](#contribution)
- [License](./LICENSE)
- [Changelog](./CHANGELOG.md)
- [Credits](#Credits)



## Support to <a name="support-to"></a>
- .NET 6.0
- .NET 5.0


## How to use <a name="how-to-use"></a>

### Install NuGet package <a name="Installation"></a>
This package is available through Nuget Packages: https://www.nuget.org/packages/PowerUtils.Results

**Nuget**
```bash
Install-Package PowerUtils.Results
```

**.NET CLI**
```
dotnet add package PowerUtils.Results
```



### Return results <a name="return-results"></a>

#### Success <a name="return-results-success"></a>
```csharp
// Without payload
var result = Result.Ok();

// With payload
var model = new Model();
var result = Result<Model>.Ok(model);

// Implicit assignment
Result<Model> = model;
```

#### Errors <a name="return-results-errors"></a>
- `Error.Error()`;
- `Error.UnauthorizedError()`
- `Error.ForbiddenError()`
- `Error.NotFoundError()`
- `Error.ConflictError()`
- `Error.ValidationError()`

```csharp
Result result = Error.Forbidden("property", "code", "description2");

// Implicit assignment
Result result = new Error("property", "code", "description2");

// Error list
var errors = new List<Error>
{
    new Error("property", "code", "description2"),
    new Error("property", "code", "description2")
};

Result result = errors;
```

##### Add more errors <a name="return-results-errors-add"></a>

```csharp
Result result = new Error("property", "code", "description2");
result.AddError(new Error("property", "code", "description2"));
result.AddError("property", "code", "description2");
```

### Extensions <a name="return-extensions"></a>

```csharp
Result result = new Error[]
{
    new("Property", "Code", "Description"),
    new("Property", "Code", "Description")
};

var error = result.FirstError();
var error = result.FirstOrDefaultError();

var error = result.LastError();
var error = result.LastOrDefaultError();

var error = result.SingleError();
var error = result.SingleOrDefaultError();
```

#### OfTypeFirstError <a name="return-extensions-OfTypeFirstError"></a>

```csharp
Result result = Error.Conflict("property", "code", "description");
var type = result.OfTypeFirstError(); // Conflict

Result result = Result.Ok();
var type = result.GetType(); // Success

Result result = Error.Unauthorized("property", "code", "description");
var type = result.GetType(); // UnauthorizedError

Result<Model> result = Error.Forbidden("property", "code", "description");
var type = result.GetType(); // ForbiddenError

var model = new Model();
Result<Model> result = model;
var type = result.GetType(); // Model
```



## How is this different from error-of? <a name="how-is-different"></a>

- Support to `.NET 5.0` using only `record` instead of `record struct` available only `.NET 6.0` or greater;
  - But in projects `.NET 6.0` or greater also uses `record struct`because it is faster;
- The base wrapper named `Result` instead of _`ErrorOf`_ like a _`FluentResults`_;
- Support for using the `Result` wrapper without an encapsulated type to be able to pass a success state like _NoContent 204_ or error;
- The error list in `Result` is based in a interface `IError` to be able to create custom error types;
- The erro structure contains:
  - __`Property`__ - For error source;
  - __`Code`__ - For an error identification code;
  - __`Description`__ - For a description of the error;



## Contribution <a name="contribution"></a>

If you have any questions, comments, or suggestions, please open an [issue](https://github.com/TechNobre/PowerUtils.Results/issues/new/choose) or create a [pull request](https://github.com/TechNobre/PowerUtils.Results/compare)



## Credits <a name="Credits"></a>

This project is totally inspired by [error-or](https://github.com/amantinband/error-or) that are excellent libraries.

Other excellent libraries used as inspiration
- [FluentResults](https://github.com/altmann/FluentResults)
- [OneOf](https://github.com/mcintyre321/OneOf)