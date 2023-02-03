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
            try
            {
                model.Result = this.expressionService.Evaluate(model.Expression);
            }
            catch (InvalidExpressionException ex)
            {
                ModelState.AddModelError("expression", ex.Message);
            }
            catch (InvalidOperationException)
            {
                ModelState.AddModelError("expression", ExpressionErrorMessages.InvalidExpression);
            }

            return this.View(model);
        }
    }
}
