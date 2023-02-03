using Microsoft.AspNetCore.Mvc;

using SimpleCalculator.Common.Exceptions;
using SimpleCalculator.Models;
using SimpleCalculator.Services;

using static SimpleCalculator.Common.Constants;

namespace SimpleCalculator.Controllers
{
    public class HomeController : Controller
    {
        private readonly IExpressionService expressionService;

        public HomeController(IExpressionService expressionService)
        {
            this.expressionService = expressionService;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Index(string expression)
        {
            var result = 0d;

            try
            {
                result = this.expressionService.Evaluate(expression);
            }
            catch (InvalidExpressionException ex)
            {
                ModelState.AddModelError("expression", ex.Message);
                return this.View();
            }
            catch (InvalidOperationException)
            {
                ModelState.AddModelError("expression", ExpressionErrorMessages.InvalidExpression);
                return this.View();
            }

            var model = new ExpressionModel
            {
                Expression = expression,
                Result = result
            };

            return this.View(model);
        }
    }
}
