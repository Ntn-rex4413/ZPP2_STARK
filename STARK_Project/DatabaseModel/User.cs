using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STARK_Project.DatabaseModel
{
    public class User : IdentityUser
    {
        public virtual ICollection<Cryptocurrency> Watchlist { get; set; }
        public virtual ICollection<Condition> Conditions { get; set; }
    }
}
