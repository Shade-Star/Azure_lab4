using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Azure_lab4
{
    public static class Function1
    {
        [FunctionName("CheckTemperature")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            int data = -100500;

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic dataBody = JsonConvert.DeserializeObject(requestBody);
            data = dataBody?.data;

            if (data != -100500)
            {
                return new OkObjectResult(CheckTemperature(data));
            }
            else
            {
                return new OkObjectResult("Fail");
            }
        }

        private static string CheckTemperature(int data)
        {
            if (data <= -15)
            {
                return "95";
            }
            else if (data <= -10)
            {
                return "83";
            }
            else if (data <= -5)
            {
                return "70";
            }
            else if (data <= 0)
            {
                return "57";
            }
            else if (data <= 5)
            {
                return "44";
            }
            else if (data <= 10)
            {
                return "30";
            }
            else
            {
                return "Off";
            }
        }
    }
}
