using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace MCT.Functions
{
    public class CalculatorTrigger
    {
        private readonly ILogger<CalculatorTrigger> _logger;

        public CalculatorTrigger(ILogger<CalculatorTrigger> logger)
        {
            _logger = logger;
        }

        [Function("CalculatorTrigger")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "calculator")] HttpRequest req)
        {
            try
            {
                // Leest de JSON-body van de aanvraag
                var request = await req.ReadFromJsonAsync<CalculationRequest>();

                if (request == null)
                {
                    return new BadRequestObjectResult("Invalid request payload");
                }

                // Verwerkt de operatie
                int result = request.Operation switch
                {
                    "+" => request.A + request.B,
                    "-" => request.A - request.B,
                    "*" => request.A * request.B,
                    "/" => request.B != 0 ? request.A / request.B : throw new DivideByZeroException("Division by zero is not allowed"),
                    _ => throw new InvalidOperationException($"Invalid operation: {request.Operation}")
                };

                // Genereert het resultaat
                var response = new CalculationResult
                {
                    A = request.A,
                    B = request.B,
                    Operation = request.Operation,
                    Result = result
                };

                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while processing the request");
                return new BadRequestObjectResult(new { error = ex.Message });
            }
        }
    }}
