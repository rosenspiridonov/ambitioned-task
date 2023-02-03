using System.Text;

using SimpleCalculator.Common.Exceptions;

using static SimpleCalculator.Common.Constants;

namespace SimpleCalculator.Services
{
    public class ExpressionService : IExpressionService
    {
        public double Evaluate(string expression)
        {
            expression = expression.Replace(" ", "");

            if (!this.ValidateTokens(expression))
            {
                throw new InvalidExpressionException(ExpressionErrorMessages.InvalidTokens);
            }

            var tokens = expression.ToCharArray();

            var numbers = new Stack<double>();
            var operators = new Stack<char>();

            var numberString = new StringBuilder();

            foreach (var token in tokens)
            {
                if (char.IsDigit(token))
                {
                    numberString.Append(token);
                }
                else
                {
                    if (numberString.Length > 0)
                    {
                        numbers.Push(double.Parse(numberString.ToString()));
                        numberString.Clear();
                    }

                    if (token == Operators.OpeningBracket)
                    {
                        operators.Push(token);
                    }
                    else if (token == Operators.ClosingBracket)
                    {
                        while (operators.Peek() != Operators.OpeningBracket)
                        {
                            numbers.Push(this.ApplyOperator(numbers, operators));
                        }

                        operators.Pop();
                    }
                    else if (token == Operators.Add || token == Operators.Subtract || token == Operators.Multiply || token == Operators.Divide)
                    {
                        while (operators.Any() && this.CanEvaluate(token, operators.Peek()))
                        {
                            numbers.Push(this.ApplyOperator(numbers, operators));
                        }

                        operators.Push(token);
                    }
                }
            }

            if (numberString.Length > 0)
            {
                numbers.Push(double.Parse(numberString.ToString()));
                numberString.Clear();
            }

            while (operators.Any())
            {
                numbers.Push(this.ApplyOperator(numbers, operators));
            }

            return numbers.Pop();
        }

        private double ApplyOperator(Stack<double> numbers, Stack<char> operators)
        {
            var num2 = numbers.Pop();
            var num1 = numbers.Pop();
            var op = operators.Pop();

            return op switch
            {
                Operators.Add => num1 + num2,
                Operators.Subtract => num1 - num2,
                Operators.Multiply => num1 * num2,
                Operators.Divide => num2 == 0 ? throw new InvalidExpressionException(ExpressionErrorMessages.DivisionByZero) : num1 / num2,
                _ => throw new InvalidExpressionException(string.Format(ExpressionErrorMessages.InvalidOperator, op))
            };
        }

        private bool CanEvaluate(char currOperator, char prevOperator)
        {
            if (prevOperator == Operators.OpeningBracket || prevOperator == Operators.ClosingBracket)
            {
                return false;
            }

            if ((currOperator == Operators.Multiply || currOperator == Operators.Divide) && (prevOperator == Operators.Add || prevOperator == Operators.Subtract))
            {
                return false;
            }

            return true;
        }

        private bool ValidateTokens(string expression)
        {
            var allowedCharacters = "0123456789+-*/()";

            return !expression.Any(ch => !allowedCharacters.Contains(ch));
        }
    }
}
