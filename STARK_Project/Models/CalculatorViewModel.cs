using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STARK_Project.Models
{
    public class CalculatorViewModel
    {
        public double LeftValue { get; set; }
        public double RightValue { get; set; }
        public string LeftCurrency { get; set; }
        public string RightCurrency { get; set; }
    }
}
