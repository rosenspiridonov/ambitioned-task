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
        public IActionResult Index(ExpressionModel model)
        {
            const string ExpressionErrorKey = "expression";

            try
            {
                model.Result = this.expressionService.Evaluate(model.Expression);
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
