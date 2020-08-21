using ProbabilitiesWebCalculator.Models;
using ProbabilitiesWebCalculator.Processor;
using System.Web.Mvc;
using System;
using System.Configuration;

namespace ProbabilitiesWebCalculator.Controllers
{
    public class HomeController : Controller
    {
        private static readonly string filePath = ConfigurationManager.AppSettings["FilePath"].ToString();

        public ActionResult Index(int? page, string sortOrder)
        {
            ViewBag.Title = "Probabilities Calculator";
            ViewBag.DateSortParm = String.IsNullOrEmpty(sortOrder) ? "CreatedAt" : "";
            ViewBag.ModeSortParm = sortOrder == "Mode" ? "mode_desc" : "Mode"; ;

            CalculationProcessor processor = new CalculationProcessor();

            ProbabilitiesWebCalculatorViewModel viewModel = processor.CreateViewModel(new ProbabilityCalculator(), page, sortOrder, filePath);
            return View(viewModel);
        }


        [HttpPost]
        public ActionResult CalculateResult(ProbabilityCalculator newCalculation)
        {
            CalculationProcessor processor = new CalculationProcessor();

            var result = processor.Process(newCalculation, filePath);

            ProbabilitiesWebCalculatorViewModel viewModel = processor.CreateViewModel(result, 1, "", filePath);
            return View("Index", viewModel);
        }



    }
}
