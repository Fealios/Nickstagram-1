using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nickstagram.Models
{
    public class User : IdentityUser
    {
        public virtual ICollection<User> Followers { get; set; }
    }
}
