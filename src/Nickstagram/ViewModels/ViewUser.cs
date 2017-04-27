using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Nickstagram.Models;

namespace Nickstagram.ViewModels
{
    public class ViewUser
    {
        public string CurrentUserId { get; set; }
        public string ViewedUserId { get; set; }
        public int PostCount { get; set; }
        public User ViewedUser { get; set; }
        public IEnumerable<Post> Posts { get; set; }
    }
}
