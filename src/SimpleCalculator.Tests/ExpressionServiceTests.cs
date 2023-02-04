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
        public async Task Evaluate_WithValidExpression_ShouldReturnCorrectResult()
        {
            Assert.Equal(expected: 3.0, actual: await this.expressionService.EvaluateAsync("1 + 2"));
            Assert.Equal(expected: 41.0, actual: await this.expressionService.EvaluateAsync("2 + 3 + (4 + 5) * (6 - 2)"));
            Assert.Equal(expected: 14.0, actual: await this.expressionService.EvaluateAsync("2(4 + 3)"));
            Assert.Equal(expected: 14.0, actual: await this.expressionService.EvaluateAsync("(4 + 3)2"));
            Assert.Equal(expected: 5.0, actual: await this.expressionService.EvaluateAsync("2.5 * (4 / 2)"));
        }

        [Fact]
        public async Task Evaluate_WithInvalidCharacters_ShouldThrowInvalidExpressionException()
        {
            var expression = "5 + 4 a";

            await Assert.ThrowsAsync<InvalidExpressionException>(async () => await this.expressionService.EvaluateAsync(expression));
        }

        [Fact]
        public async Task Evaluate_DivisionByZero_ShouldThrowInvalidExpressionException()
        {
            var expression = "1 / 0";

            await Assert.ThrowsAsync<InvalidExpressionException>(async () => await this.expressionService.EvaluateAsync(expression));
        }
    }
}