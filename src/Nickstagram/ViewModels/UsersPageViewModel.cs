using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Nickstagram.Models;

namespace Nickstagram.ViewModels
{
    public class UsersPageViewModel
    {
        public IEnumerable<User> Users { get; set; }
        public string CurrentUserId { get; set; }
    }
}
