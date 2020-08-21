using ProbabilitiesWebCalculator.Calculators;
using Xunit;
using FluentAssertions;

namespace ProbabilitiesCalculator.UnitTest
{
    public class CalculatorTests
    {
        private Calculator calculator;

        public CalculatorTests()
        {
            calculator = new Calculator();

        }

        [Theory]
        [InlineData(0.5, 0.5)]
        [InlineData(1, 1)]
        [InlineData(0, 0)]
        public void CalculateCombineAwithB_ReturnCorrectValue(decimal a, decimal b)
        {
            var result = calculator.CombineAwithB(a, b);
            result.Should().Be(a * b);
        }

        [Theory]
        [InlineData(0.5, 0.5)]
        [InlineData(1, 1)]
        [InlineData(0, 0)]
        public void EitherAorB_ReturnCorrectValue(decimal a, decimal b)
        {
            var result = calculator.EitherAorB(a, b);
            result.Should().Be(a + b - a * b);
        }
    }
}
