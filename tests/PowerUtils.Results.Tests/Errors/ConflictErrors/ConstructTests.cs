﻿using FluentAssertions;
using Xunit;

namespace PowerUtils.Results.Tests.Errors.ConflictErrors
{
    public class ConstructTests
    {
        [Fact]
        public void ErrorWithDescription_Construct_CustomDescription()
        {
            // Arrange
            var property = "fakeConflictProperty";
            var code = "fakeConflictCode";
            var description = "fakeConflictDescription";


            // Act
            var act = new ConflictError(property, code, description);


            // Assert
            act.Should().IsError<ConflictError>(
                property,
                code,
                description);
        }

        [Fact]
        public void ErrorWithoutDescription_Construct_DefaultDescription()
        {
            // Arrange
            var property = "fakeConflictProperty";
            var code = "fakeConflictCode";


            // Act
            var act = new ConflictError(property, code);


            // Assert
            act.Should().IsError<ConflictError>(
                property,
                code,
                $"An error has occurred with code '{code}'");
        }
    }
}
