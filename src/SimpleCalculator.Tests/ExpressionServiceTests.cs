using SimpleCalculator.Common.Exceptions;
using SimpleCalculator.Services;

namespace SimpleCalculator.Tests
{
    public class ExpressionServiceTests
    {
        private readonly ExpressionService expressionService;

        public ExpressionServiceTests()
        {
            this.expressionService = new ExpressionService();
        }

        [Fact]
        public void Evaluate_WithValidExpression_ShouldReturnCorrectResult()
        {
            Assert.Equal(expected: 3.0, actual: this.expressionService.Evaluate("1 + 2"));
            Assert.Equal(expected: 41.0, actual: this.expressionService.Evaluate("2 + 3 + (4 + 5) * (6 - 2)"));
            Assert.Equal(expected: 14.0, actual: this.expressionService.Evaluate("2(4 + 3)"));
            Assert.Equal(expected: 14.0, actual: this.expressionService.Evaluate("(4 + 3)2"));
            Assert.Equal(expected: 5.0, actual: this.expressionService.Evaluate("2.5 * (4 / 2)"));
        }

        [Fact]
        public void Evaluate_WithInvalidCharacters_ShouldThrowInvalidExpressionException()
        {
            var expression = "5 + 4 a";

            Assert.Throws<InvalidExpressionException>(() => this.expressionService.Evaluate(expression));
        }

        [Fact]
        public void Evaluate_DivisionByZero_ShouldThrowInvalidExpressionException()
        {
            var expression = "1 / 0";

            Assert.Throws<InvalidExpressionException>(() => this.expressionService.Evaluate(expression));
        }
    }
}