﻿using FluentAssertions;
using Xunit;

namespace PowerUtils.Results.Tests.Errors.ForbiddenErrors
{
    public class MethodGetHashCodeTests
    {
        [Fact]
        public void DifferentsProperties_GetHashCode_False()
        {
            // Arrange
            var property = "fakeProperty";
            var code = "fakeCode";
            var description = "fakeDescription";

            var left = Error.Forbidden(property, code, description);
            var right = Error.Forbidden("fake", code, description);


            // Act
            var act = left.GetHashCode() == right.GetHashCode();


            // Assert
            act.Should().BeFalse();
        }

        [Fact]
        public void SamePropertiesAndDifferentType_GetHashCode_False()
        {
            // Arrange
            var property = "fakeProperty";
            var code = "fakeCode";
            var description = "fakeDescription";

            var left = Error.Forbidden(property, code, description);
            var right = Error.Unauthorized(property, code, description);


            // Act
            var act = left.GetHashCode() == right.GetHashCode();


            // Assert
            act.Should().BeFalse();
        }

        [Fact]
        public void SamePropertiesAndSameType_GetHashCode_True()
        {
            // Arrange
            var property = "fakeProperty";
            var code = "fakeCode";
            var description = "fakeDescription";

            var left = Error.Forbidden(property, code, description);
            var right = Error.Forbidden(property, code, description);


            // Act
            var act = left.GetHashCode() == right.GetHashCode();


            // Assert
            act.Should().BeTrue();
        }
    }
}
