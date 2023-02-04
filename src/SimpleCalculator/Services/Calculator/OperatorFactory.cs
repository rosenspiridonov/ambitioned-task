using SimpleCalculator.Common.Exceptions;
using SimpleCalculator.Services.Operators;

using static SimpleCalculator.Common.Constants;

namespace SimpleCalculator.Services.Calculator
{
    public class OperatorFactory
    {
        public static IOperator GetOperator(char op)
        {
            return op switch
            {
                Symbols.Add => new AddOperator(),
                Symbols.Subtract => new SubtractOperator(),
                Symbols.Multiply => new MultiplyOperator(),
                Symbols.Divide => new DivideOperator(),
                _ => throw new InvalidExpressionException(string.Format(ExpressionErrorMessages.InvalidOperator, op))
            };
        }
    }
}
