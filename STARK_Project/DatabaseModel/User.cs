using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STARK_Project.DatabaseModel
{
    public class User : IdentityUser
    {
        public ICollection<Cryptocurreny> Watchlist { get; set; }
    }
}
