namespace ProbabilitiesWebCalculator.Calculators
{
    public class Calculator
    {
        public decimal CombineAwithB(decimal probabilityA, decimal probabilityB)
        {
            return probabilityA * probabilityB;
        }

        public decimal EitherAorB(decimal probabilityA, decimal probabilityB)
        {
            return probabilityA + probabilityB - (probabilityA * +probabilityB);
        }
    }
}