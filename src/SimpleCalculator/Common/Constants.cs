namespace SimpleCalculator.Common
{
    public static class Constants
    {
        public static class Symbols
        {
            public const char Add = '+';
            public const char Subtract = '-';
            public const char Multiply = '*';
            public const char Divide = '/';
            public const char OpeningBracket = '(';
            public const char ClosingBracket = ')';
        }

        public static class ExpressionErrorMessages
        {
            public const string InvalidExpression = "Invalid expression.";
            public const string DivisionByZero = "Division by zero is not allowed.";
            public const string InvalidOperator = "Invalid operator: {0}.";
            public const string InvalidTokens = "Allowed characters are digits, +, -, *, /, (, ) and .";
            public const string ExpressionEmpty = "Expression cannot be empty.";
        }
    }
}
