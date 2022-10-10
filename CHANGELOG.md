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
