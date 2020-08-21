using Newtonsoft.Json;
using ProbabilitiesWebCalculator.Models;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web.Http;
using ProbabilitiesWebCalculator.Processor;
using System.Configuration;

namespace ProbabilitiesWebCalculator.Controllers
{
    public class ProbabilitiesCalculatorController : ApiController
    {
        private static readonly string filePath = ConfigurationManager.AppSettings["FilePath"].ToString();

        public IHttpActionResult Get()
        {
            var jsonText = File.ReadAllText(filePath);
            var histories = JsonConvert.DeserializeObject<List<ProbabilityCalculator>>(jsonText);

            if (histories == null)
            {
                return NotFound();
            }
            return Ok(histories);
        }

        public IHttpActionResult Post([FromBody] ProbabilityCalculator newCalculation)
        {
            CalculationProcessor processor =  new CalculationProcessor();
            var result  = processor.Process(newCalculation, filePath);
            if (result == null)
            {
                return StatusCode(HttpStatusCode.BadRequest);
            }
            return StatusCode(HttpStatusCode.Created);
        }

      
    }
}
