using System;
using System.Globalization;
using FluentAssertions;
using Xunit;

namespace PowerUtils.Results.Tests
{
    public class ErrorCodesTests
    {
        [Fact]
        public void CultureInfoPT_CreateMin_CodeWithComma()
        {
            // Arrange
            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-PT");
            var value = 1.24;


            // Act
            var act = ErrorCodes.CreateMin(value);


            // Assert
            act.Should().Be("MIN:1.24");
        }

        [Fact]
        public void CultureInfoPT_CreateMax_CodeWithComma()
        {
            // Arrange
            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-PT");
            var value = 21.87;


            // Act
            var act = ErrorCodes.CreateMax(value);


            // Assert
            act.Should().Be("MAX:21.87");
        }

        [Theory]
        [InlineData("0", "MIN:0")]
        [InlineData("10", "MIN:10")]
        [InlineData("-45", "MIN:-45")]
        public void String_CreateMin_Code(string input, string expected)
        {
            // Arrange && Act
            var act = ErrorCodes.CreateMin(input);


            // Assert
            act.Should()
                .Be(expected);
        }

        [Theory]
        [InlineData("0", "MAX:0")]
        [InlineData("10", "MAX:10")]
        [InlineData("-45", "MAX:-45")]
        public void String_CreateMax_Code(string input, string expected)
        {
            // Arrange && Act
            var act = ErrorCodes.CreateMax(input);


            // Assert
            act.Should()
                .Be(expected);
        }


        [Theory]
        [InlineData(0, "MIN:0")]
        [InlineData(10, "MIN:10")]
        [InlineData(-45, "MIN:-45")]
        public void Int_CreateMin_Code(int input, string expected)
        {
            // Arrange && Act
            var act = ErrorCodes.CreateMin(input);


            // Assert
            act.Should()
                .Be(expected);
        }

        [Theory]
        [InlineData(0, "MAX:0")]
        [InlineData(10, "MAX:10")]
        [InlineData(-45, "MAX:-45")]
        public void Int_CreateMax_Code(int input, string expected)
        {
            // Arrange && Act
            var act = ErrorCodes.CreateMax(input);


            // Assert
            act.Should()
                .Be(expected);
        }


        [Theory]
        [InlineData(0, "MIN:0")]
        [InlineData(10, "MIN:10")]
        [InlineData(45, "MIN:45")]
        public void UInt_CreateMin_Code(uint input, string expected)
        {
            // Arrange && Act
            var act = ErrorCodes.CreateMin(input);


            // Assert
            act.Should()
                .Be(expected);
        }

        [Theory]
        [InlineData(0, "MAX:0")]
        [InlineData(10, "MAX:10")]
        [InlineData(45, "MAX:45")]
        public void UInt_CreateMax_Code(uint input, string expected)
        {
            // Arrange && Act
            var act = ErrorCodes.CreateMax(input);


            // Assert
            act.Should()
                .Be(expected);
        }


        [Theory]
        [InlineData(0, "MIN:0")]
        [InlineData(10, "MIN:10")]
        [InlineData(-45, "MIN:-45")]
        public void Long_CreateMin_Code(long input, string expected)
        {
            // Arrange && Act
            var act = ErrorCodes.CreateMin(input);


            // Assert
            act.Should()
                .Be(expected);
        }

        [Theory]
        [InlineData(0, "MAX:0")]
        [InlineData(10, "MAX:10")]
        [InlineData(-45, "MAX:-45")]
        public void Long_CreateMax_Code(long input, string expected)
        {
            // Arrange && Act
            var act = ErrorCodes.CreateMax(input);


            // Assert
            act.Should()
                .Be(expected);
        }


        [Theory]
        [InlineData(0, "MIN:0")]
        [InlineData(10, "MIN:10")]
        [InlineData(45, "MIN:45")]
        public void ULong_CreateMin_Code(ulong input, string expected)
        {
            // Arrange && Act
            var act = ErrorCodes.CreateMin(input);


            // Assert
            act.Should()
                .Be(expected);
        }

        [Theory]
        [InlineData(0, "MAX:0")]
        [InlineData(10, "MAX:10")]
        [InlineData(45, "MAX:45")]
        public void ULong_CreateMax_Code(ulong input, string expected)
        {
            // Arrange && Act
            var act = ErrorCodes.CreateMax(input);


            // Assert
            act.Should()
                .Be(expected);
        }


        [Theory]
        [InlineData(0.124, "MIN:0.124")]
        [InlineData(10, "MIN:10")]
        [InlineData(-45, "MIN:-45")]
        [InlineData(-45.454, "MIN:-45.454")]
        public void Float_CreateMin_Code(float input, string expected)
        {
            // Arrange && Act
            var act = ErrorCodes.CreateMin(input);


            // Assert
            act.Should()
                .Be(expected);
        }

        [Theory]
        [InlineData(0.124, "MAX:0.124")]
        [InlineData(10, "MAX:10")]
        [InlineData(-45, "MAX:-45")]
        [InlineData(-45.454, "MAX:-45.454")]
        public void Float_CreateMax_Code(float input, string expected)
        {
            // Arrange && Act
            var act = ErrorCodes.CreateMax(input);


            // Assert
            act.Should()
                .Be(expected);
        }


        [Theory]
        [InlineData(0.124, "MIN:0.124")]
        [InlineData(10, "MIN:10")]
        [InlineData(-45, "MIN:-45")]
        [InlineData(-45.454, "MIN:-45.454")]
        public void Double_CreateMin_Code(double input, string expected)
        {
            // Arrange && Act
            var act = ErrorCodes.CreateMin(input);


            // Assert
            act.Should()
                .Be(expected);
        }

        [Theory]
        [InlineData(0.124, "MAX:0.124")]
        [InlineData(10, "MAX:10")]
        [InlineData(-45, "MAX:-45")]
        [InlineData(-45.454, "MAX:-45.454")]
        public void Double_CreateMax_Code(double input, string expected)
        {
            // Arrange && Act
            var act = ErrorCodes.CreateMax(input);


            // Assert
            act.Should()
                .Be(expected);
        }


        [Theory]
        [InlineData(0.124, "MIN:0.124")]
        [InlineData(10, "MIN:10")]
        [InlineData(-45, "MIN:-45")]
        [InlineData(-45.454, "MIN:-45.454")]
        public void Decimal_CreateMin_Code(decimal input, string expected)
        {
            // Arrange && Act
            var act = ErrorCodes.CreateMin(input);


            // Assert
            act.Should()
                .Be(expected);
        }

        [Theory]
        [InlineData(0.124, "MAX:0.124")]
        [InlineData(10, "MAX:10")]
        [InlineData(-45, "MAX:-45")]
        [InlineData(-45.454, "MAX:-45.454")]
        public void Decimal_CreateMax_Code(decimal input, string expected)
        {
            // Arrange && Act
            var act = ErrorCodes.CreateMax(input);


            // Assert
            act.Should()
                .Be(expected);
        }



        [Theory]
        [InlineData("2000-12-12", "MIN:2000-12-12", "yyyy-MM-dd")]
        [InlineData("1987-01-11", "MIN:1987-01-11", "yyyy-MM-dd")]
        [InlineData("2099-11-01 21:23:43", "MIN:2099-11-01 21:23:43", "yyyy-MM-dd HH:mm:ss")]
        [InlineData("1977-02-21 02:45", "MIN:1977-02-21 02:45", "yyyy-MM-dd HH:mm")]
        public void DateTime_CreateMin_Code(string input, string expected, string format)
        {
            // Arrange
            var dateTime = DateTime.ParseExact(input, format, CultureInfo.InvariantCulture);


            // Act
            var act = ErrorCodes.CreateMin(dateTime, format);


            // Assert
            act.Should()
                .Be(expected);
        }

        [Theory]
        [InlineData("2000-12-12", "MAX:2000-12-12", "yyyy-MM-dd")]
        [InlineData("1987-01-11", "MAX:1987-01-11", "yyyy-MM-dd")]
        [InlineData("2099-11-01 21:23:43", "MAX:2099-11-01 21:23:43", "yyyy-MM-dd HH:mm:ss")]
        [InlineData("1977-02-21 02:45", "MAX:1977-02-21 02:45", "yyyy-MM-dd HH:mm")]
        public void DateTime_CreateMax_Code(string input, string expected, string format)
        {
            // Arrange
            var dateTime = DateTime.ParseExact(input, format, CultureInfo.InvariantCulture);


            // Act
            var act = ErrorCodes.CreateMax(dateTime, format);


            // Assert
            act.Should()
                .Be(expected);
        }



        [Theory]
        [InlineData("2000-12-12", "MIN:2000-12-12")]
        [InlineData("1987-01-11", "MIN:1987-01-11")]
        [InlineData("2099-11-01", "MIN:2099-11-01")]
        [InlineData("1977-02-21", "MIN:1977-02-21")]
        public void DateTime_CreateDateMin_Code(string input, string expected)
        {
            // Arrange
            var dateTime = DateTime.ParseExact(input, "yyyy-MM-dd", CultureInfo.InvariantCulture);


            // Act
            var act = ErrorCodes.CreateDateMin(dateTime);


            // Assert
            act.Should()
                .Be(expected);
        }

        [Theory]
        [InlineData("2000-12-12", "MAX:2000-12-12")]
        [InlineData("1987-01-11", "MAX:1987-01-11")]
        [InlineData("2099-11-01", "MAX:2099-11-01")]
        [InlineData("1977-02-21", "MAX:1977-02-21")]
        public void DateTime_CreateDateMax_Code(string input, string expected)
        {
            // Arrange
            var dateTime = DateTime.ParseExact(input, "yyyy-MM-dd", CultureInfo.InvariantCulture);


            // Act
            var act = ErrorCodes.CreateDateMax(dateTime);


            // Assert
            act.Should()
                .Be(expected);
        }



        [Theory]
        [InlineData("2000-12-12 21:23:43", "MIN:2000-12-12 21:23:43")]
        [InlineData("1987-01-11 10:11:21", "MIN:1987-01-11 10:11:21")]
        [InlineData("2099-11-01 02:03:04", "MIN:2099-11-01 02:03:04")]
        [InlineData("1977-02-21 19:04:05", "MIN:1977-02-21 19:04:05")]
        public void DateTime_CreateDateTimeMin_Code(string input, string expected)
        {
            // Arrange
            var dateTime = DateTime.ParseExact(input, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);


            // Act
            var act = ErrorCodes.CreateDateTimeMin(dateTime);


            // Assert
            act.Should()
                .Be(expected);
        }

        [Theory]
        [InlineData("2000-12-12 21:23:43", "MAX:2000-12-12 21:23:43")]
        [InlineData("1987-01-11 10:11:21", "MAX:1987-01-11 10:11:21")]
        [InlineData("2099-11-01 02:03:04", "MAX:2099-11-01 02:03:04")]
        [InlineData("1977-02-21 19:04:05", "MAX:1977-02-21 19:04:05")]
        public void DateTime_CreateDateTimeMax_Code(string input, string expected)
        {
            // Arrange
            var dateTime = DateTime.ParseExact(input, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);


            // Act
            var act = ErrorCodes.CreateDateTimeMax(dateTime);


            // Assert
            act.Should()
                .Be(expected);
        }

#if NET6_0_OR_GREATER
        [Theory]
        [InlineData("01-12-2010", "MIN:01-12-2010", "dd-MM-yyyy")]
        [InlineData("11-1987-01", "MIN:11-1987-01", "MM-yyyy-dd")]
        [InlineData("1987/05/02", "MIN:1987/05/02", "yyyy/MM/dd")]
        public void DateOnly_CreateMin_Code(string input, string expected, string format)
        {
            // Arrange
            var date = DateOnly.ParseExact(input, format, CultureInfo.InvariantCulture);


            // Act
            var act = ErrorCodes.CreateMin(date, format);


            // Assert
            act.Should()
                .Be(expected);
        }

        [Theory]
        [InlineData("01-12-1810", "MAX:01-12-1810", "dd-MM-yyyy")]
        [InlineData("11-1999-01", "MAX:11-1999-01", "MM-yyyy-dd")]
        [InlineData("1900/05/02", "MAX:1900/05/02", "yyyy/MM/dd")]
        public void DateOnly_CreateMax_Code(string input, string expected, string format)
        {
            // Arrange
            var date = DateOnly.ParseExact(input, format, CultureInfo.InvariantCulture);


            // Act
            var act = ErrorCodes.CreateMax(date, format);


            // Assert
            act.Should()
                .Be(expected);
        }

        [Theory]
        [InlineData("23:12:38", "MIN:23:12:38", "HH:mm:ss")]
        [InlineData("13:22", "MIN:13:22", "HH:mm")]
        [InlineData("01:22", "MIN:01:22", "hh:mm")]
        public void TimeOnly_CreateMin_Code(string input, string expected, string format)
        {
            // Arrange
            var time = TimeOnly.ParseExact(input, format, CultureInfo.InvariantCulture);


            // Act
            var act = ErrorCodes.CreateMin(time, format);


            // Assert
            act.Should()
                .Be(expected);
        }

        [Theory]
        [InlineData("23:12:38", "MAX:23:12:38", "HH:mm:ss")]
        [InlineData("13:22", "MAX:13:22", "HH:mm")]
        [InlineData("01:22", "MAX:01:22", "hh:mm")]
        public void TimeOnly_CreateMax_Code(string input, string expected, string format)
        {
            // Arrange
            var time = TimeOnly.ParseExact(input, format, CultureInfo.InvariantCulture);


            // Act
            var act = ErrorCodes.CreateMax(time, format);


            // Assert
            act.Should()
                .Be(expected);
        }
#endif
    }
}
