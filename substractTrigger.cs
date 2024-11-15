using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace MCT.Functions
{
    public class substractTrigger
    {
        private readonly ILogger<substractTrigger> _logger;

        public substractTrigger(ILogger<substractTrigger> logger)
        {
            _logger = logger;
        }

        [Function("substractTrigger")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "Substract/{a:int}/{b:int}")] HttpRequest req, int a, int b)
        {
            int results = a - b;
            return new OkObjectResult($"The substraction of {a} - {b} = {results}");
        }
    }
}
