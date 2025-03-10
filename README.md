# PowerUtils.Results

![Logo](https://raw.githubusercontent.com/TechNobre/PowerUtils.Results/main/assets/logo/logo_128x128.png)

***Wrapper to transfer results or errors between layers or resources***

![Tests](https://github.com/TechNobre/PowerUtils.Results/actions/workflows/tests.yml/badge.svg)
[![Mutation tests](https://img.shields.io/endpoint?style=flat&url=https%3A%2F%2Fbadge-api.stryker-mutator.io%2Fgithub.com%2FTechNobre%2FPowerUtils.Results%2Fmain)](https://dashboard.stryker-mutator.io/reports/github.com/TechNobre/PowerUtils.Results/main)

[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=TechNobre_PowerUtils.Results&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=TechNobre_PowerUtils.Results)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=TechNobre_PowerUtils.Results&metric=coverage)](https://sonarcloud.io/summary/new_code?id=TechNobre_PowerUtils.Results)
[![Reliability Rating](https://sonarcloud.io/api/project_badges/measure?project=TechNobre_PowerUtils.Results&metric=reliability_rating)](https://sonarcloud.io/summary/new_code?id=TechNobre_PowerUtils.Results)
[![Bugs](https://sonarcloud.io/api/project_badges/measure?project=TechNobre_PowerUtils.Results&metric=bugs)](https://sonarcloud.io/summary/new_code?id=TechNobre_PowerUtils.Results)

[![NuGet](https://img.shields.io/nuget/v/PowerUtils.Results.svg)](https://www.nuget.org/packages/PowerUtils.Results)
[![Nuget](https://img.shields.io/nuget/dt/PowerUtils.Results.svg)](https://www.nuget.org/packages/PowerUtils.Results)
[![License: MIT](https://img.shields.io/github/license/TechNobre/PowerUtils.Results.svg)](https://github.com/TechNobre/PowerUtils.Results/blob/main/LICENSE)


- [Support to](#support-to)
- [How to use](#how-to-use)
  - [Install NuGet package](#install-nuget-package)
  - [Creating a Result](#creating-a-result)
    - [Success](#success)
    - [Errors](#errors)
      - [Add more errors](#add-more-errors)
      - [Built-in error types](#built-in-error-types)
      - [Custom error](#custom-error)
    - [Result factory](#result-factory)
  - [Extensions](#extensions)
    - [Handling errors](#handling-errors)
    - [OfTypeFirstError](#oftypefirsterror)
    - [Handling success](#handling-success)
    - [Switch](#switch)
    - [Match](#match)
    - [Conversions](#conversions)
    - [DistinctErrors](#distincterrors)
  - [Deconstruct operators](#deconstruct-operators)
  - [Implicit conversion](#implicit-conversion)
  - [Check validity](#check-validity)
  - [Serialization/Deserialization](#serializationdeserialization)
    - [Results](#results)
    - [Errors](#errors-1)
- [Contribution](#contribution)



## Support to
- .NET 9.0
- .NET 8.0
- .NET 7.0
- .NET 6.0
- .NET 5.0


## How to use

### Install NuGet package
This package is available through Nuget Packages: https://www.nuget.org/packages/PowerUtils.Results

**Nuget**
```bash
Install-Package PowerUtils.Results
```

**.NET CLI**
```
dotnet add package PowerUtils.Results
```



### Creating a Result

#### Success
```csharp
// Void result
var result = Result.Ok();
var result = Result.Success();

// Result with typed value
var model = new Model();
var result = Result<Model>.Ok(model);
var result = Result.Ok<Model>(model);
var result = Result.Ok(model);
var result = Result.Success(model);

// Implicit assignment
Result<Model> result = model;
Result result = new Success();
```

#### Errors


```csharp
Result result = Error.Failure("property", "code", "description");
Result result = Error.Unauthorized("property", "code", "description");
Result result = Error.Forbidden("property", "code", "description");
Result result = Error.NotFound("property", "code", "description");
Result result = Error.Conflict("property", "code", "description");
Result result = Error.Validation("property", "code", "description");
Result result = Error.Unexpected("property", "code", "description");

// Implicit assignment
Result result = new Error("property", "code", "description");
Result result = new UnauthorizedError("property", "code", "description");
Result result = new ForbiddenError("property", "code", "description");
Result result = new NotFoundError("property", "code", "description");
Result result = new ConflictError("property", "code", "description");
Result result = new ValidationError("property", "code", "description");
Result result = new UnexpectedError("property", "code", "description");

// Error list
var errors = new List<Error>
{
    new Error("property", "code", "description2"),
    new Error("property", "code", "description2")
};

Result result = errors;
```

##### Add more errors

```csharp
Result result = new Error("property", "code", "description");

result.AddError(new Error("property", "code", "description"));
result.AddError("property", "code", "description");

result.AddErrors(new List<Error>
{
    new Error("property", "code", "description2"),
    new Error("property", "code", "description2")
});
```

##### Built-in error types

- `Error.Error()`;
- `Error.Unauthorized()`
- `Error.Forbidden()`
- `Error.NotFound()`
- `Error.Conflict()`
- `Error.Validation()`
- `Error.Unexpected()`

##### Custom error

```csharp
public class CustomError : IError
{
    public string Property { get; init; }
    public string Code { get; init; }
    public string Description { get; init; }

    public CustomError(string property, string code, string description)
    {
        Property = property;
        Code = code;
        Description = description;
    }
}

var error = new CustomError(property, code, description);

var result = Result.From(error);
var result = Result<FakeModel>.From(error);
var result = Result.From<FakeModel>(error);
```


#### Result factory
Creates a `Result<TValue>` when the error list is null or empty otherwise creates a result with a list of errors.

Delegate is used to instantiate the value only when there are no errors
```csharp
// Returns `Result` with value
var result = Result.Create(
    Array.Empty<Error>(),
    () => new Model());

// Returns `Result` with errors
var result = Result.Create(
    new List<Error> { Error.Failure("property", "code", "description") },
    () => new Model());
```



### Extensions

#### Handling errors
```csharp
Result result = new Error[]
{
    new("Property", "Code", "Description"),
    new("Property", "Code", "Description")
};

IError error = result.FirstError();
IError error = result.FirstOrDefaultError();
IError error = result.FirstOrDefaultError(Func<IError, bool> predicate);

IError error = result.LastError();
IError error = result.LastOrDefaultError();
IError error = result.LastOrDefaultError(Func<IError, bool> predicate);

IError error = result.SingleError();
IError error = result.SingleOrDefaultError();
IError error = result.SingleOrDefaultError(Func<IError, bool> predicate);

bool IResult.ContainsError();
bool IResult.ContainsError(Func<IError, bool> predicate);
```

#### OfTypeFirstError

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

#### Handling success

```csharp
bool IResult.IsSuccess();
bool IResult.IsSuccess(Func<TValue, bool> predicate);
```

#### Switch

```csharp
result.Switch(
    value => onSuccess(value),
    errors => onErrors(errors));

// Only return the value or first erro
result.SwitchFirst(
    value => onSuccess(value),
    error => onError(error));

await result.SwitchAsync(
    value => onSuccess(value),
    errors => onErrors(errors));

// Only return the value or first erro
await result.SwitchFirstAsync(
    value => onSuccess(value),
    error => onError(error));
```

#### Match

```csharp
TOutput response = result.Match<TValue, TOutput>(
    value => onSuccess(value),
    errors => onErrors(errors));

// Only return the value or first erro
TOutput response = result.MatchFirst<TValue, TOutput>(
    value => onSuccess(value),
    error => onError(error));


Task<TOutput> response = result.MatchAsync<TValue, TOutput>(
    value => onSuccess(value),
    errors => onErrors(errors));

// Only return the value or first erro
Task<TOutput> response = result.MatchFirstAsync<TValue, TOutput>(
    value => onSuccess(value),
    error => onError(error));
```

#### Conversions

```csharp
var errorList = result.Errors.AsList();
```

#### DistinctErrors

```csharp
Result result = new IError[]
{
    Error.Conflict("FakeProperty", "FakeCode", "FakeDescription"),
    Error.Conflict("FakeProperty", "FakeCode", "FakeDescription")};

// Only returns one error
var errors = result.DistinctErrors();
```

### Deconstruct operators

```csharp
var (property, code, description) = Error.Unauthorized("property", "code", "description");

Result<Model> result = new Model { Id = id, Name = name };
// Deconstruct -> value is not null and errors is empty
(var value, var errors) = result;
```

### Implicit conversion

```csharp
Result<Model> result = new Model { Id = id, Name = name };
Model model = result;

// Result with errors
Result result = Error.Conflict("property", "code", "description");
List<IError> errors = result;
```

### Check validity

**Valid example**
```csharp
Result<Model> result = new Model { Id = id, Name = name };

// result = implicit conversion "true"
if(result)
{
    // Do something
}

// Equivalent to
if(result.IsError == false)
{
    // Do something
}

if(result.IsSuccess(out var value, out var errors))
{
    // Do something
}

if(result.IsError(out var value, out var errors))
{
    // Do something
}
```

**Invalid example**
```csharp
Result result = Error.Conflict("property", "code", "description");

// result = implicit conversion "false"
if(result)
{
    // Do something
}

// Equivalent to
if(result.IsError == true)
{
    // Do something
}
```

### Serialization/Deserialization

#### Results

**Serialization `Result` success example**
```csharp
var result = Result.Success();
var json = JsonSerializer.Serialize(result);
/*
json = {
    "IsSuccess": true
}
*/
```

```csharp
var result = Result.Success(45);
var json = JsonSerializer.Serialize(result);
/*
json = {
    "IsSuccess": true,
    "Value": 45
}
*/
```

**Serialization `Result` errors example**
```csharp
Result result = Error.NotFound("client", "NOT_FOUND", "Client not found");
Result<int> result = Error.NotFound("client", "NOT_FOUND", "Client not found");

var json = JsonSerializer.Serialize(result);
/*
json = {
    "IsSuccess": false,
    "Errors": [
        {
            "_type": "PowerUtils.Results.NotFoundError",
            "Property": "client",
            "Code": "NOT_FOUND",
            "Description":"Client not found"
        }
    ]
}
*/
```

**Deserialization `Result` success example**

```csharp
var json = """
    "IsSuccess": true
    """;

var result = JsonSerializer.Deserialize<Result>(json);
```

```csharp
var json = """
    "IsSuccess": true,
    "Value": 541.7
    """;

var result = JsonSerializer.Deserialize<Result<double>>(json);
```

**Deserialization `Result` errors example**

```csharp
var json = """
    "IsSuccess": false,
    "Errors": [
        {
            "_type": "PowerUtils.Results.NotFoundError",
            "Property": "client",
            "Code": "NOT_FOUND",
            "Description":"Client not found"
        }
    ]
    """;

var result = JsonSerializer.Deserialize<Result>(json);
var result = JsonSerializer.Deserialize<Result<Client>>(json);
```


#### Errors

**Serialization example**
```csharp
var error = Error.NotFound("client", "NOT_FOUND", "Client not found");

var json = JsonSerializer.Serialize(error);
/*
json = {
    "_type": "PowerUtils.Results.NotFoundError",
    "Property": "client",
    "Code": "NOT_FOUND",
    "Description": "Client not found"
}
*/
```

**Deserialization example**
```csharp
var json = """
    {
        "_type": "PowerUtils.Results.NotFoundError",
        "Property": "client",
        "Code": "NOT_FOUND",
        "Description": "Client not found"
    }
    """;

var error = JsonSerializer.Deserialize<NotFoundError>(json);
```

For .NET 6.0 or greater, you can use the `IError` interface as the type to be deserialized.
To versions .NET 5.0 or lower, the deserialization using interface is not supported by `System.Text.Json`. You will get an exception `System.NotSupportedException`.

```csharp
var error = JsonSerializer.Deserialize<IError>(json);
```

**Custom error Serialization/Deserialization example**

```csharp
[JsonConverter(typeof(ErrorJsonConverter<CustomError>))]
public class CustomError : IError { }
```


## Contribution

If you have any questions, comments, or suggestions, please open an [issue](https://github.com/TechNobre/PowerUtils.Results/issues/new/choose) or create a [pull request](https://github.com/TechNobre/PowerUtils.Results/compare)
