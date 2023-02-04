namespace SimpleCalculator.Services
{
    public interface IExpressionService
    {
        Task<double> EvaluateAsync(string expression);
    }
}
