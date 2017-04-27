using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Nickstagram.Controllers;
using Nickstagram.Models;
using Xunit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Nickstagram.Tests.ControllerTests
{
            
    public class HomeControllerTest
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        [Fact]
        public void Get_ModelList_Index_Test()
        {
            var contextOptions = new DbContextOptionsBuilder()
            .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Nickstagram;integrated security=True")
            .Options;
            var _db = new NickstagramDbContext(contextOptions);
            HomeController controller = new HomeController(_userManager, _signInManager, _db);

            IActionResult actionResult = controller.Index();
            ViewResult indexView = controller.Index() as ViewResult;

            var result = indexView.ViewData.Model;

            Assert.IsType<List<Post>>(result);
        }
    }
}
