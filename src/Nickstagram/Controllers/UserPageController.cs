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
            ViewBag.UserId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return View();
        }

        //handling adding new content
        public IActionResult AddPost(string id)
        {
            return View(new PostViewModel { UserId = id });
        }

        [HttpPost]
        public async Task<IActionResult> Upload(PostViewModel newPost)
        {
            var filePath = "";
            var uploads = Path.Combine(_environment.WebRootPath, "uploads");
            foreach (var file in newPost.files)
            {
                if (file.Length > 0)
                {
                    filePath = Path.Combine(uploads, file.FileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                }
            }
            var user = _db.Users.FirstOrDefault(users => users.Id == newPost.UserId);
            var post = new Post { ImagePath = filePath, Description = newPost.Description, PostUser = user };
            _db.Posts.Add(post);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        //end handling new content
    }
}
