﻿using FluentAssertions;
using Xunit;

namespace PowerUtils.Results.Tests.Errors.NotFoundErrors
{
    public class ConstructTests
    {
        [Fact]
        public void ErrorWithDescription_Construct_CustomDescription()
        {
            // Arrange
            var property = "fakeNotFoundProperty";
            var code = "fakeNotFoundCode";
            var description = "fakeNotFoundDescription";


            // Act
            var act = new NotFoundError(property, code, description);


            // Assert
            act.Property.Should()
                .Be(property);
            act.Code.Should()
                .Be(code);
            act.Description.Should()
                .Be(description);
        }

        [Fact]
        public void ErrorWithoutDescription_Construct_DefaultDescription()
        {
            // Arrange
            var property = "fakeNotFoundProperty";
            var code = "fakeNotFoundCode";


            // Act
            var act = new NotFoundError(property, code);


            // Assert
            act.Property.Should()
                .Be(property);
            act.Code.Should()
                .Be(code);
            act.Description.Should()
                .Be($"An error has occurred with code '{code}'");
        }
    }
}
