namespace SimpleCalculator.Services.Operators
{
    public class SubtractOperator : IOperator
    {
        public double Evaluate(double num1, double num2) => num1 - num2;
    }
}
