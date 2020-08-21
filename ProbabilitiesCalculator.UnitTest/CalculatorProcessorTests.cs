using FluentAssertions;
using Newtonsoft.Json;
using ProbabilitiesWebCalculator.Models;
using ProbabilitiesWebCalculator.Processor;
using System;
using System.IO;
using System.Text;
using Xunit;
using static ProbabilitiesWebCalculator.Models.ProbabilityCalculator;

namespace ProbabilitiesCalculator.UnitTest
{
    public class CalculatorProcessorTests : IDisposable
    {
        private CalculationProcessor processor;
        string validFilePath = @"D:\VSSourceCode\ProbabilitiesCalculator\ProbabilitiesCalculator.UnitTest\TestData\test.txt";


        public CalculatorProcessorTests()
        {
            processor = new CalculationProcessor();
            CreateTestFile();
        }

        public void Dispose()
        {
            // dispose for cleanup code
            File.Delete(validFilePath);
        }

        [Fact]
        public void ProcessCombinedWithCalculation_WithValidModel_ReturnCorrectModel()
        {
            ProbabilityCalculator model = new ProbabilityCalculator();

            model.Mode = CalculationModel.CombinedWith;
            model.ProbabilityA = new decimal(0.5);
            model.ProbabilityB = new decimal(0.5);
            var result = processor.Process(model, validFilePath);
            result.Result.Should().Be(new decimal(0.25));
            result.CreatedAt.Should().BeBefore(DateTime.Now);
        }

        [Fact]
        public void ProcessEitherOrCalculation_WithValidModel_ReturnCorrectModel()
        {
            ProbabilityCalculator model = new ProbabilityCalculator();

            model.Mode = CalculationModel.Either;
            model.ProbabilityA = new decimal(0.5);
            model.ProbabilityB = new decimal(0.5);
            var result = processor.Process(model, validFilePath);
            result.Result.Should().Be(new decimal(0.75));
            result.CreatedAt.Should().BeBefore(DateTime.Now);
        }

        [Fact]
        public void ProcessEitherOrCalculation_WithInValidModel_ReturnNull()
        {
            ProbabilityCalculator model = new ProbabilityCalculator();

            model.Mode = CalculationModel.Either;
            model.ProbabilityA = new decimal(1.2);
            model.ProbabilityB = new decimal(0.5);
            var result = processor.Process(model, validFilePath);
            result.Should().Be(null);
        }

        [Fact]
        public void CreateViewModel_WithValidFile_ReturnModel()
        {
            ProbabilityCalculator model = new ProbabilityCalculator();

            model.Mode = CalculationModel.Either;
            model.ProbabilityA = new decimal(1.2);
            model.ProbabilityB = new decimal(0.5);
            ProbabilitiesWebCalculatorViewModel result =
                processor.CreateViewModel(model, 1, null, validFilePath);
            result.CalculationHistory.Should().NotBeEmpty();
        }

        [Fact]
        public void CreateViewModel_WithInValidFile_ReturnModel()
        {
            ProbabilityCalculator model = new ProbabilityCalculator();

            model.Mode = CalculationModel.Either;
            model.ProbabilityA = new decimal(1.2);
            model.ProbabilityB = new decimal(0.5);
            ProbabilitiesWebCalculatorViewModel result =
                processor.CreateViewModel(model, 1, null, @"invalidPath/test.txt");
            result.CalculationHistory.Should().BeEmpty();
        }

        private void CreateTestFile()
        {
            using (StreamWriter file = File.AppendText(validFilePath))
            {
                var text = new StringBuilder();
                var jsonText = JsonConvert.SerializeObject(new ProbabilityCalculator()
                {
                    ProbabilityA = 1,
                    ProbabilityB = 1,
                    Mode = CalculationModel.CombinedWith,
                    Result = 1,
                    CreatedAt = DateTime.Now
                },
                    Formatting.Indented);

                text.AppendLine("[");
                text.Append(jsonText);
                text.AppendLine("]");
                file.WriteLine(text.ToString());
            }
        }
    }
}
