using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Nickstagram.ViewModels;
using Nickstagram.Models;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace Nickstagram.Controllers
{
    public class UserPageController : Controller
    {

        private readonly NickstagramDbContext _db;
        private IHostingEnvironment _environment;

        public UserPageController(IHostingEnvironment environment, NickstagramDbContext db)
        {
            _db = db;
            _environment = environment;
        }
        public IActionResult Index()
        {
            var currentUserId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            ViewBag.UserId = currentUserId;
            var posts = _db.Posts.Include(post => post.PostUser).Where(post => post.PostUser.Id == currentUserId).ToList();
            ViewBag.PostCount = posts.Count;
            ViewBag.User = _db.Users.FirstOrDefault(user => user.Id == currentUserId);
            return View(posts);
        }

        //handling adding new content
        public IActionResult AddPost(string id)
        {
            return View(new PostViewModel { UserId = id });
        }

        [HttpPost]
        public async Task<IActionResult> Upload(PostViewModel newPost)
        {
            var fileName = "";
            var uploads = Path.Combine(_environment.WebRootPath, "uploads");
            foreach (var file in newPost.files)
            {
                if (file.Length > 0)
                {
                    fileName = file.FileName;
                    using (var fileStream = new FileStream(Path.Combine(uploads, file.FileName), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                }
            }
            var user = _db.Users.FirstOrDefault(users => users.Id == newPost.UserId);
            //var post = new Post { ImagePath = ".\\..\\" + filePath.Substring(filePath.IndexOf("wwwroot")), Description = newPost.Description, PostUser = user };
            var post = new Post { ImagePath = "/uploads/" + fileName, Description = newPost.Description, PostUser = user };
            _db.Posts.Add(post);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        //end handling new content
    }
}
