using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace MCT.Functions
{
    public class Multiplicatetrigger
    {
        private readonly ILogger<Multiplicatetrigger> _logger;

        public Multiplicatetrigger(ILogger<Multiplicatetrigger> logger)
        {
            _logger = logger;
        }

        [Function("Multiplicatetrigger")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "Multiplication/{a:int}/{b:int}")] HttpRequest req, int a, int b)
        {
            int result = a *b;
            return new OkObjectResult($"the multiplication {a}*{b} = {result}");
        }
    }
}
