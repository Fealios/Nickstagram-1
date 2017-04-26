using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Nickstagram.Models;

namespace Nickstagram.ViewModels
{
    public class PostViewModel
    {
        [Required]
        [Display(Name = "Image")]
        public ICollection<IFormFile> files { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }

        public string UserId { get; set; }
    }
}
