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
                ViewBag.Language = Request.QueryString.Value.Contains("en") ? "en" : "cn";
            }

            if (ViewBag.Language == "en")
            {
                ViewBag.UserNameHead = "User Name";
                ViewBag.EmailNameHead = "User Email";
                ViewBag.TimeNameHead = "Creation Data ";
                ViewBag.AuthorNameHead = "Authority";
                ViewBag.DeleteNameHead = "Delete";
            }
            else
            {
                ViewBag.UserNameHead = "用户名称";
                ViewBag.EmailNameHead = "邮箱";
                ViewBag.TimeNameHead = "创建时间 ";
                ViewBag.AuthorNameHead = "权限";
                ViewBag.DeleteNameHead = "删除";
            }
        }

        public IActionResult Users()
        {
            ViewBag.Title = "Users";
            GetTableHeader();

            using var db = new MyDbContext();
            ViewBag.Users = db.EDU365_EduSystemsUsers.Take(10).ToList();
           

            return View();
        }

        public IActionResult Region()
        {
            ViewBag.Title = "Region";
            return View();
        }
        public IActionResult Sensor()
        {
            ViewBag.Title = "Sensor";
            return View();
        }
        public IActionResult Quality()
        {
            ViewBag.Title = "Quality";
            return View();
        }
        public IActionResult PM2_5()
        {
            ViewBag.Title = "PM2.5";
            return View();
        }
        public IActionResult PM_standard()
        {
            ViewBag.Title = "PM_standard";
            return View();
        }
    }
}