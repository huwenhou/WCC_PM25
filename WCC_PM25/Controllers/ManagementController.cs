using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Z.EntityFramework.Extensions;

namespace WCC_PM25.Controllers
{
    public class ManagementController : Controller
    {
        private void GetTableHeader()
        {
            ViewBag.Language = "en";

            if (!string.IsNullOrEmpty(Request.QueryString.Value))
            {
                ViewBag.Language = Request.QueryString.Value.Contains("en")?"en":"cn";
            }

            if (ViewBag.Language == "en")
            {
                ViewBag.UserNameHead = "User Name";
            }
            else
            {
                ViewBag.UserNameHead = "用户名称";
            }
        }

        public IActionResult Users()
        {
            ViewBag.Title = "Users";

            using var db = new MyDbContext();
            ViewBag.Users = db.EDU365_EduSystemsUsers.Take(10).ToList();

            return View();
        }

        public IActionResult Regions()
        {
            ViewBag.Title = "Regions";
            return View();
        }
    }
}
