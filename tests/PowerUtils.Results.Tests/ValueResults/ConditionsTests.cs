using System.Collections.Generic;
using FluentAssertions;
using FluentAssertions.Execution;
using PowerUtils.Results.Tests.Fakes;
using Xunit;

namespace PowerUtils.Results.Tests.ValueResults
{
    public class ConditionsTests
    {
        [Fact]
        public void ResultWithErrors_If_ValueNullAndErrors()
        {
            // Arrange
            var property = "Fake";
            Result<FakeModel> result = Error.Forbidden(property);


            // Act
            FakeModel value = null;
            List<IError> errors = null;
            var condition = 0;

            if(result is (var value1, var errors1) && errors1.Count == 0)
            {
                value = value1;
                errors = errors1;
                condition = 1;
            }

            if(result is (var value2, var errors2) && errors2.Count == 1)
            {
                value = value2;
                errors = errors2;
                condition = 2;
            }


            // Assert
            using(new AssertionScope())
            {
                value.Should().BeNull();
                errors.Should().ContainSingle(s => s.Property == property);

                condition.Should().Be(2);
            }
        }

        [Fact]
        public void ResultWithoutErrors_If_ValueNullAndErrors()
        {
            // Arrange
            Result<FakeModel> result = new FakeModel();


            // Act
            FakeModel value = null;
            List<IError> errors = null;
            var condition = 0;

            if(result is (var value1, var errors1) && errors1.Count == 0)
            {
                value = value1;
                errors = errors1;
                condition = 1;
            }

            if(result is (var value2, var errors2) && errors2.Count == 1)
            {
                value = value2;
                errors = errors2;
                condition = 2;
            }


            // Assert
            using(new AssertionScope())
            {
                value.Should().NotBeNull();
                errors.Should().BeEmpty();

                condition.Should().Be(1);
            }
        }
    }
}
