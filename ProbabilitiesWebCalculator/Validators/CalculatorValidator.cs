using ProbabilitiesWebCalculator.Models;

namespace ProbabilitiesWebCalculator.Validators
{
    public class CalculatorValidator
    {
        public bool IsInputValid(ProbabilityCalculator calculator)
        {
            if (calculator.ProbabilityA < 0 || calculator.ProbabilityA > 1 || calculator.ProbabilityB < 0 || calculator.ProbabilityB > 1)
            {
                return false;
            }
            if (calculator.ProbabilityA == 0 && calculator.ProbabilityB == 0)
            {
                return false;
            }
            return true;
        }
    }
}