using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STARK_Project.Models
{
    public class ConditionViewModel
    {
        public string Type { get; set; }
        public string LimitType { get; set; }
        public float ThresholdValue { get; set; }
    }
}
