using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace ProbabilitiesWebCalculator.Models
{
    public class ProbabilityCalculator
    {
        public enum CalculationModel
        {
            CombinedWith,
            Either
        }
        [Required]
        [Range(0, 1, ErrorMessage = "Input has to be between 0 - 1")]
        public decimal ProbabilityA { get; set; }
        [Required]
        [Range(0, 1, ErrorMessage = "Input has to be between 0 - 1")]
        public decimal ProbabilityB { get; set; }
        [Required]
        public CalculationModel Mode { get; set; }
        public decimal Result { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; }
    }
}