using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nickstagram.Controllers;

namespace Nickstagram.Filters
{
    public class MyActionFilter : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //var myController = context.Controller as HomeController;

            //if (myController != null)
            //{
            //    myController.Layout = new MainLayoutViewModel
            //    {

            //    };

            //    myController.ViewBag.MainLayoutViewModel = myController.Layout;
            //}
        }
    }
}
