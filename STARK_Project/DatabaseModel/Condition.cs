using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STARK_Project.DatabaseModel
{
    public class Condition
    {
        public int Id { get; set; }
        public float TresholdMax { get; set; }
        public float TresholdMin { get; set; }
        public virtual Cryptocurrency Cryptocurrency { get; set; }
        public virtual User User { get; set; }
    }
}
