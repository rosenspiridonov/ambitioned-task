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
            const string ExpressionErrorKey = "expression";

            var model = new ExpressionModel
            {
                Expression = expression,
            };

            if (expression == null)
            {
                ModelState.AddModelError(ExpressionErrorKey, ExpressionErrorMessages.ExpressionEmpty);
                return this.View(model);
            }

            try
            {
                model.Result = this.expressionService.Evaluate(expression);
            }
            catch (InvalidExpressionException ex)
            {
                ModelState.AddModelError(ExpressionErrorKey, ex.Message);
            }
            catch (InvalidOperationException)
            {
                ModelState.AddModelError(ExpressionErrorKey, ExpressionErrorMessages.InvalidExpression);
            }

            return this.View(model);
        }
    }
}
