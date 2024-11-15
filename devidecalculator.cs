using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace MCT.Functions
{
    public class Devidecalculator
    {
        private readonly ILogger<Devidecalculator> _logger;

        public Devidecalculator(ILogger<Devidecalculator> logger)
        {
            _logger = logger;
        }

        [Function("Devidecalculator")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "Devision/{a:int}/{b:int}")] HttpRequest req, int a, int b)
        {
            int results = a / b;
            return new OkObjectResult($"the devision {a}/{b} = {results}");
        }
    }
}
