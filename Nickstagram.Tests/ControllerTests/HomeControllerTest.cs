using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Nickstagram.Controllers;
using Nickstagram.Models;
using Nickstagram.ViewModels;
using Xunit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

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

        [Fact]
        public async void Post_RegisterPost_Test()
        {
            var contextOptions = new DbContextOptionsBuilder()
            .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Nickstagram;integrated security=True")
            .Options;
            var _db = new NickstagramDbContext(contextOptions);
            HomeController controller = new HomeController(_userManager, _signInManager, _db);

            RegisterViewModel viewModel = new RegisterViewModel();
            viewModel.UserName = "Melvin";
            viewModel.Password = "Spartan1!7";

            await controller.RegisterPost(viewModel);

            var dbUser = await _db.Users.FirstOrDefaultAsync(users => users.UserName == viewModel.UserName);
            Console.WriteLine(dbUser.UserName);
            Console.WriteLine(viewModel.UserName);

            Assert.Equal(dbUser.UserName, viewModel.UserName);
        }
    }
}
