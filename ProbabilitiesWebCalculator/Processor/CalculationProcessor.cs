using Newtonsoft.Json;
using PagedList;
using ProbabilitiesWebCalculator.Models;
using ProbabilitiesWebCalculator.Validators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ProbabilitiesWebCalculator.Processor
{
    public class CalculationProcessor
    {

        public ProbabilityCalculator Process(ProbabilityCalculator calculatorModel, string filePath)
        {
            var validator = new CalculatorValidator();
            if (!validator.IsInputValid(calculatorModel))
            {
                return null;
            }

            ProbabilityCalculator newCalculation = CalculateResult(calculatorModel);

            WriteToFile(newCalculation, filePath);
            return newCalculation;
        }

        public ProbabilitiesWebCalculatorViewModel CreateViewModel(
            ProbabilityCalculator calculationModel, int? page, 
            string sortOrder, string filePath)
        {
            ProbabilitiesWebCalculatorViewModel viewModel = 
                new ProbabilitiesWebCalculatorViewModel();
            viewModel.CalculationModel = calculationModel;
            viewModel.CalculationHistory = new PagedList<ProbabilityCalculator>(null, 1, 1);
            var result = LoadHistoryRecords(filePath);

            SortResult(page, sortOrder, viewModel, result);
            return viewModel;
        }

        private static IList<ProbabilityCalculator> LoadHistoryRecords(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return null;
            }
            string jsonText = File.ReadAllText(filePath);

            var histories = 
                JsonConvert.DeserializeObject<IList<ProbabilityCalculator>>(jsonText);

            return histories;
        }

        private static void SortResult(int? page, string sortOrder, 
            ProbabilitiesWebCalculatorViewModel viewModel, 
            IList<ProbabilityCalculator> result)
        {
            if (result != null && !result.Equals(string.Empty))
            {
                int pageSize = 10;
                int pageNumber = (page ?? 1);
                var sortedResult = result.OrderByDescending(x => x.CreatedAt);

                switch (sortOrder)
                {
                    case "CreatedAt":
                        sortedResult = result.OrderBy(x => x.CreatedAt);
                        break;
                    case "Mode":
                        sortedResult = result.OrderBy(x => x.Mode);
                        break;
                    case "mode_desc":
                        sortedResult = result.OrderByDescending(x => x.Mode);
                        break;
                    default:
                        sortedResult = result.OrderByDescending(x => x.CreatedAt);
                        break;
                }

                viewModel.CalculationHistory = sortedResult.ToPagedList(pageNumber, pageSize);
            }
        }

        private static void WriteToFile(ProbabilityCalculator newCalculation, string filePath)
        {
            var jsonText = JsonConvert.SerializeObject(newCalculation, Formatting.Indented);
            var text = new StringBuilder();

            if (File.Exists(filePath))
            {
                string texts = File.ReadAllText(filePath);
                if (texts != null)
                {
                    texts = texts.Replace("]", ",");
                    File.WriteAllText(filePath, texts);
                }
            }
            else
            {
                var dirPath = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }
                text.AppendLine("[");
            }

            text.Append(jsonText);
            text.AppendLine("]");

            using (StreamWriter file = File.AppendText(filePath))
            {

                file.WriteLine(text.ToString());
            }
        }

        private static ProbabilityCalculator CalculateResult(ProbabilityCalculator cauculator)
        {
            ProbabilityCalculator newCalculation = new ProbabilityCalculator();

            newCalculation.ProbabilityA = cauculator.ProbabilityA;
            newCalculation.ProbabilityB = cauculator.ProbabilityB;
            newCalculation.Mode = cauculator.Mode;

            Calculators.Calculator calculator = new Calculators.Calculator();
            if (newCalculation.Mode == ProbabilityCalculator.CalculationModel.CombinedWith)
            {
                newCalculation.Result = 
                    calculator.CombineAwithB(newCalculation.ProbabilityA, newCalculation.ProbabilityB);
            }

            if (newCalculation.Mode == ProbabilityCalculator.CalculationModel.Either)
            {
                newCalculation.Result = 
                    calculator.EitherAorB(newCalculation.ProbabilityA, newCalculation.ProbabilityB);
            }
            newCalculation.CreatedAt = DateTime.Now;
            return newCalculation;
        }

    }
}