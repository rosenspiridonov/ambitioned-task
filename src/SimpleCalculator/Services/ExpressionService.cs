using System.Text;

using SimpleCalculator.Common.Exceptions;
using SimpleCalculator.Services.Calculator;

using static SimpleCalculator.Common.Constants;

namespace SimpleCalculator.Services
{
    public class ExpressionService : IExpressionService
    {
        public Task<double> EvaluateAsync(string expression) => Task.FromResult(this.Evaluate(expression));

        private double Evaluate(string expression)
        {
            expression = expression.Replace(" ", string.Empty);

            if (!this.IsValid(expression))
            {
                throw new InvalidExpressionException(ExpressionErrorMessages.InvalidTokens);
            }

            var tokens = expression.ToCharArray();

            var numbers = new Stack<double>();
            var operators = new Stack<char>();

            var prevToken = '\0';
            var numberString = new StringBuilder();

            foreach (var token in tokens)
            {
                if (char.IsDigit(token) || token == '.')
                {
                    if (prevToken == Symbols.ClosingBracket)
                    {
                        operators.Push(Symbols.Multiply);
                    }

                    numberString.Append(token);
                }
                else
                {
                    if (numberString.Length > 0)
                    {
                        numbers.Push(double.Parse(numberString.ToString()));
                        numberString.Clear();
                    }

                    if (token == Symbols.OpeningBracket)
                    {
                        if (char.IsDigit(prevToken))
                        {
                            operators.Push(Symbols.Multiply);
                        }

                        operators.Push(token);
                    }
                    else if (token == Symbols.ClosingBracket)
                    {
                        while (operators.Peek() != Symbols.OpeningBracket)
                        {
                            numbers.Push(this.ApplyOperator(numbers, operators));
                        }

                        operators.Pop();
                    }
                    else if (token == Symbols.Add || 
                             token == Symbols.Subtract || 
                             token == Symbols.Multiply || 
                             token == Symbols.Divide)
                    {
                        while (operators.Any() && this.CanEvaluate(token, operators.Peek()))
                        {
                            numbers.Push(this.ApplyOperator(numbers, operators));
                        }

                        operators.Push(token);
                    }
                }

                prevToken = token;
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
            var op = OperatorFactory.GetOperator(operators.Pop());

            return op.Evaluate(num1, num2);
        }

        private bool CanEvaluate(char currOperator, char prevOperator)
        {
            if (prevOperator == Symbols.OpeningBracket)
            {
                return false;
            }

            if ((currOperator == Symbols.Multiply || currOperator == Symbols.Divide) && 
                (prevOperator == Symbols.Add || prevOperator == Symbols.Subtract))
            {
                return false;
            }

            return true;
        }

        private bool IsValid(string expression)
        {
            var allowedCharacters = "0123456789+-*/().";

            return !expression.Any(ch => !allowedCharacters.Contains(ch));
        }
    }
}
