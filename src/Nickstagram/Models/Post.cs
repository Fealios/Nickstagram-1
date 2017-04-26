using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nickstagram.Models
{
    [Table("Posts")]
    public class Post
    {
        [Key]
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
        public int Likes { get; set; }
        public virtual User PostUser { get; set; }
        public Post()
        {
            Likes = 0;
        }
        
    }
}
