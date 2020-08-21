using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProbabilitiesWebCalculator.Models
{
    public class ProbabilitiesWebCalculatorViewModel
    {
        public ProbabilityCalculator CalculationModel { get; set; }

        public IPagedList<ProbabilityCalculator> CalculationHistory { get; set; }
        
    }
}