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
            var expression = "2 + 3 + (4 + 5) * (6 - 2)";
            var expectedResult = 41d;

            var result = this.expressionService.Evaluate(expression);
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void Evaluate_WhenGivenInvalidCharacters_ShouldThrowInvalidExpressionException()
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