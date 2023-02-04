using SimpleCalculator.Common.Exceptions;

using static SimpleCalculator.Common.Constants;

namespace SimpleCalculator.Services.Operators
{
    public class DivideOperator : IOperator
    {
        public double Evaluate(double num1, double num2)
        {
            if (num2 == 0)
            {
                throw new InvalidExpressionException(ExpressionErrorMessages.DivisionByZero);
            }

            return num1 / num2;
        }
    }
}
