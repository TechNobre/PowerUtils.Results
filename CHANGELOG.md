# [2.10.0](https://github.com/TechNobre/PowerUtils.Results/compare/v2.9.1...v2.10.0) (2024-11-12)


### Features

* Added support to .NET9 ([11894c6](https://github.com/TechNobre/PowerUtils.Results/commit/11894c683cdd00c0d57bb6be194783d001d87705))

## [2.9.1](https://github.com/TechNobre/PowerUtils.Results/compare/v2.9.0...v2.9.1) (2023-11-18)


### Bug Fixes

* Added support to .NET8 ([b58c136](https://github.com/TechNobre/PowerUtils.Results/commit/b58c136c50b38dde584756c42e6e693a4c0285e4))

# [2.9.0](https://github.com/TechNobre/PowerUtils.Results/compare/v2.8.1...v2.9.0) (2023-11-18)


### Features

* Added support to .NET8 ([93668de](https://github.com/TechNobre/PowerUtils.Results/commit/93668de2116fd01fab8f604ee52bd2edef9546b7))

## [2.8.1](https://github.com/TechNobre/PowerUtils.Results/compare/v2.8.0...v2.8.1) (2023-05-05)


### Bug Fixes

* Null reference when the value is null ([37992eb](https://github.com/TechNobre/PowerUtils.Results/commit/37992eb977a05be80741facbc6ba64fc4f698a61))

# [2.8.0](https://github.com/TechNobre/PowerUtils.Results/compare/v2.7.0...v2.8.0) (2023-03-14)


### Bug Fixes

* Issues when the error type does not exist ([93fa697](https://github.com/TechNobre/PowerUtils.Results/commit/93fa69723e0cdd586e78c66b48567fef3fcef52f))


### Features

* Added support to serialize and deserialize custom erros ([b69d0bd](https://github.com/TechNobre/PowerUtils.Results/commit/b69d0bd1aad5ee21f5b0e79057b92524a4d7fdc4))
* Added support to serialize and deserialize erros using `System.Text.Json` ([652efe2](https://github.com/TechNobre/PowerUtils.Results/commit/652efe2ac6ae0334dd527ad375b5504874025fde))
* Added support to serialize/deserialize `VoidResult` using `System.Text.Json` ([d209da3](https://github.com/TechNobre/PowerUtils.Results/commit/d209da343e7042d31a52d690b8f5427f9b6c375a))
* Added support to serialize/deserialize ValueResult using `System.Text.Json` ([cd7c082](https://github.com/TechNobre/PowerUtils.Results/commit/cd7c08235ce50fb33a4a4fcac0a8378ed2b9c937))

# [2.7.0](https://github.com/TechNobre/PowerUtils.Results/compare/v2.6.0...v2.7.0) (2023-03-09)


### Features

* Added new factory `Result.From(List<IError> errors)` ([10140de](https://github.com/TechNobre/PowerUtils.Results/commit/10140de94b9ae900422f57b49b060e195de2311c))
* Created factory `Success.Create();` ([44e6145](https://github.com/TechNobre/PowerUtils.Results/commit/44e61454dab522e930e37e6f5844f425103dc876))

# [2.6.0](https://github.com/TechNobre/PowerUtils.Results/compare/v2.5.0...v2.6.0) (2022-12-11)


### Features

* Added extension `Result.DistinctErrors()` ([d6b1fac](https://github.com/TechNobre/PowerUtils.Results/commit/d6b1fac933840dcc244bc90b0b1e5ca6a78dbdb4))

# [2.5.0](https://github.com/TechNobre/PowerUtils.Results/compare/v2.4.0...v2.5.0) (2022-11-27)


### Bug Fixes

* Added ConfigureAwait(false) ([9433286](https://github.com/TechNobre/PowerUtils.Results/commit/94332861bea839957b8bf755963fa8e466fdab9f))


### Features

* Added equality operators in Errors ([a37dff1](https://github.com/TechNobre/PowerUtils.Results/commit/a37dff15b4f427caa284e457d730688a5c39dc74))
* Added property IsSuccess in Results ([b08a9b6](https://github.com/TechNobre/PowerUtils.Results/commit/b08a9b6caaa425cae0680ed37b9b778ff38d9fbc))
* Extended IEquatable in errors ([f1f1cd9](https://github.com/TechNobre/PowerUtils.Results/commit/f1f1cd9d4c5b1e0150b8e1ba99d104d0b519ad02))

# [2.4.0](https://github.com/TechNobre/PowerUtils.Results/compare/v2.3.0...v2.4.0) (2022-11-11)


### Features

* Added extensions `IsError()` ([09f792e](https://github.com/TechNobre/PowerUtils.Results/commit/09f792ee1972321a83aa88a08823bcb37ae3c6e7))

# [2.3.0](https://github.com/TechNobre/PowerUtils.Results/compare/v2.2.0...v2.3.0) (2022-11-10)


### Features

* Added `Deconstruct` to `ValueResult` ([d3fec73](https://github.com/TechNobre/PowerUtils.Results/commit/d3fec73b90a1fdbb93321dc6960c9975de6b5cbe))
* Added `IsSuccess` with `Deconstruct` ([1df1040](https://github.com/TechNobre/PowerUtils.Results/commit/1df1040a174474d07a208e665b96b0d09c4b1212))
* Added extension `AsList()` ([046ecf3](https://github.com/TechNobre/PowerUtils.Results/commit/046ecf37cd50ae6b3381dcec9151987dc4f23fe9))

# [2.2.0](https://github.com/TechNobre/PowerUtils.Results/compare/v2.1.0...v2.2.0) (2022-11-09)


### Features

* Added support to .NET 7.0 ([1e825e4](https://github.com/TechNobre/PowerUtils.Results/commit/1e825e4020777b216e8c26f1cd2c5f1076644d86))

# [2.1.0](https://github.com/TechNobre/PowerUtils.Results/compare/v2.0.1...v2.1.0) (2022-11-01)


### Features

* Added implicit conversion from result to error list ([e4b4e2c](https://github.com/TechNobre/PowerUtils.Results/commit/e4b4e2c4a75b72c5d33bab58edfb67d668b91bfb))
* Added implicit conversion to boolean ([73dfc8f](https://github.com/TechNobre/PowerUtils.Results/commit/73dfc8fd92d9cf49b83523dfe3fa70ddff889639))

## [2.0.1](https://github.com/TechNobre/PowerUtils.Results/compare/v2.0.0...v2.0.1) (2022-10-10)


### Bug Fixes

* Rename ErrorCodes to ResultErrorCodes to prevent conflits ([64dc111](https://github.com/TechNobre/PowerUtils.Results/commit/64dc11185c8933c9b85cd04b613e16bab78446b6))

# [2.0.0](https://github.com/TechNobre/PowerUtils.Results/compare/v1.5.0...v2.0.0) (2022-10-09)


### Code Refactoring

* Rename `ErrorCodes`  to `Errors.Codes` to prevent conflits with other nugets ([f773e12](https://github.com/TechNobre/PowerUtils.Results/commit/f773e12e3b849f5d5db3c719bd5e774a9647601f))


### BREAKING CHANGES

* Moved ErrorCode factories to `PowerUtils.Results.Validations` nuget

# [1.5.0](https://github.com/TechNobre/PowerUtils.Results/compare/v1.4.0...v1.5.0) (2022-10-09)


### Features

* Added new extensions for async methos ([303cdbe](https://github.com/TechNobre/PowerUtils.Results/commit/303cdbe3f216ae27825e3d361ea53a4cc810e53f))
* Added new factories to Create ErrorCodes based in `DateOnly` and `TimeOnly`; ([f1ed19b](https://github.com/TechNobre/PowerUtils.Results/commit/f1ed19b75d7e1b269fa319d3ab82f7448dff3dab))

# [1.4.0](https://github.com/TechNobre/PowerUtils.Results/compare/v1.3.0...v1.4.0) (2022-10-01)


### Features

* Added new async extensions to `Switch` and `Match` ([8e56117](https://github.com/TechNobre/PowerUtils.Results/commit/8e561173ed48ebc946f27f4bfe0a555f51fc6f5c))

# [1.3.0](https://github.com/TechNobre/PowerUtils.Results/compare/v1.2.0...v1.3.0) (2022-09-26)


### Bug Fixes

* When added error list with nulls ([53c5770](https://github.com/TechNobre/PowerUtils.Results/commit/53c5770397b507ae19b54fb4ade09b293070b369))


### Features

* Added method Success to create a result ([10a88b2](https://github.com/TechNobre/PowerUtils.Results/commit/10a88b22005f427914b475b6b79ea4ab6382cca9))
* Added method to add multi errors ([475e4e8](https://github.com/TechNobre/PowerUtils.Results/commit/475e4e889c5e2e295c70b55906d56d4292ab8802))
* Inplicit operator to create a result from Success type ([ee78ef1](https://github.com/TechNobre/PowerUtils.Results/commit/ee78ef14c3f01cfff1c4ad97dd724452c7a521ee))

# [1.2.0](https://github.com/TechNobre/PowerUtils.Results/compare/v1.1.0...v1.2.0) (2022-09-11)


### Bug Fixes

* Sealed `Result` in .NET5.0 to be equivalent to `record struct Result` .NET6.0 ([8625866](https://github.com/TechNobre/PowerUtils.Results/commit/8625866ce8637aa12cb4f6a281edf9b09f649040))


### Features

* Added factory for error codes ([63b4a20](https://github.com/TechNobre/PowerUtils.Results/commit/63b4a2059ae0a8e8cb7563e136a1411baf1e4553))

# [1.1.0](https://github.com/TechNobre/PowerUtils.Results/compare/v1.0.0...v1.1.0) (2022-09-08)


### Features

* Added extension `Result.Match()` and `Result.MatchFirst()` ([7d55f5b](https://github.com/TechNobre/PowerUtils.Results/commit/7d55f5b2fabbbdf6fe47624f7406813f00a0df17))
* Added factory `Result.Create()`; ([b9f419b](https://github.com/TechNobre/PowerUtils.Results/commit/b9f419b1b1d0c2a447cff419b2abfc7b4cf3b7b8))
* Added implicit conversion from `VoidResult` to `ValueResult` and from `ValueResult` to `VoidResult` ([9ac8628](https://github.com/TechNobre/PowerUtils.Results/commit/9ac8628422256f98738a18c0c8518c2b7c51b1d3))

# 1.0.0 (2022-09-06)


### Features

* added `FirstOrDefaultError()` with predicate ([968380f](https://github.com/TechNobre/PowerUtils.Results/commit/968380fdc7d197e59927e03ed5d492a5c689247a))
* Added extension `Result.ContainsError()` ([b6bc3dc](https://github.com/TechNobre/PowerUtils.Results/commit/b6bc3dc9bcecc89c646b9df9d3e49c97b68339b3))
* Added extension `Result.IsSuccess()` ([5565011](https://github.com/TechNobre/PowerUtils.Results/commit/55650110f745f3a09542973abd02fb47f5742f01))
* Added extensions `FirstOrDefaultError`, `LastOrDefaultError`, `SingleOrDefaultError` ([5c43285](https://github.com/TechNobre/PowerUtils.Results/commit/5c432855e5d1db97e7c24e0d481bf6fd9769861f))
* Added extensions `Result.Switch()` and `Result.SwitchFirst()` ([82d5a4e](https://github.com/TechNobre/PowerUtils.Results/commit/82d5a4ebf7916920ff41cae3defd7b95a4208fd5))
* Added implicit conversion from `Result<TModel>` to `TModel` ([7459537](https://github.com/TechNobre/PowerUtils.Results/commit/7459537b2d85dff744e11c1cbc6d72e3cea4ecdf))
* Added method `Result.From()` for errors ([a9f8a30](https://github.com/TechNobre/PowerUtils.Results/commit/a9f8a30fa40c609ebfc344c5bb2ef3dd8e945867))
* Added method `Result.Ok<TValue>();` ([6acbca0](https://github.com/TechNobre/PowerUtils.Results/commit/6acbca03bbdffc38fc0e99b55594f06bf67a3990))
* Added new error type `Unexpected` ([a343a02](https://github.com/TechNobre/PowerUtils.Results/commit/a343a02045cffbdc7a4703f9425b28922aabf04e))
* added ValidationError ([6e709db](https://github.com/TechNobre/PowerUtils.Results/commit/6e709db160ea63a70164c450f20ced501a38ec0b))
* Kickoff ([61acf06](https://github.com/TechNobre/PowerUtils.Results/commit/61acf06ef87a86666b8b1a19375d128198406555))
* Override method `Result.GetType()` ([48a24be](https://github.com/TechNobre/PowerUtils.Results/commit/48a24bef96beb8d9ca2851ab3a6ebf2515c11547))
