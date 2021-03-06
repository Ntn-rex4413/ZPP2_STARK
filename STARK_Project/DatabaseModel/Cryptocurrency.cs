using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STARK_Project.DatabaseModel
{
    public class Cryptocurrency
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Condition> Conditions { get; set; }
    }
}
