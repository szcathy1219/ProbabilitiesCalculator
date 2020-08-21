using ProbabilitiesWebCalculator.Validators;
using ProbabilitiesWebCalculator.Models;
using Xunit;
using FluentAssertions;

namespace ProbabilitiesCalculator.UnitTest
{
    public class CalculatorValidatorTest
    {
        private CalculatorValidator validator;

        public CalculatorValidatorTest()
        {
            validator = new CalculatorValidator();
        }

        [Theory]
        [InlineData(0.1, 1)]
        [InlineData(1, 1)]
        [InlineData(0.01, 0.99)]
        public void IsInputValid_WithValidInput_ReturnTrue(decimal a, decimal b)
        {

            var model = new ProbabilityCalculator();
            model.ProbabilityA = a;
            model.ProbabilityB = b;
            var result = validator.IsInputValid(model);
            result.Should().BeTrue();
        }

        [Theory]
        [InlineData(-0.1, 0.5)]
        [InlineData(1, 1.1)]
        [InlineData(0, 0)]
        public void IsInputValid_WithInvalidInput_ReturnFalse(decimal a, decimal b)
        {

            var model = new ProbabilityCalculator();
            model.ProbabilityA = a;
            model.ProbabilityB = b;
            validator.IsInputValid(model);
            var result = validator.IsInputValid(model);
            result.Should().BeFalse();
        }
    }
}
