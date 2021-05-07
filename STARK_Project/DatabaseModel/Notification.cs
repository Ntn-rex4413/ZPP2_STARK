using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STARK_Project.DatabaseModel
{
    public class Notification
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public virtual User User { get; set; }
    }
}
