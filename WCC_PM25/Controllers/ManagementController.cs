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



        public void GetDataByPage()
        {
            ViewBag.Page = "P1";

            if (!string.IsNullOrEmpty(Request.QueryString.Value))
            {
                ViewBag.Page = Request.QueryString.Value.Trim('?');
            }

            using var db = new MyDbContext();

            int pagenumber = 10;
            int pagecount = db.EDU365_EduSystemsUsers.Count();

            int pages = 0;
            if (pagecount % pagenumber == 0)
            {
                pages = pagecount / pagenumber;
            }
            else
            {
                pages = pagecount / pagenumber + 1;
            }

            ViewBag.Pages = pages;

            //go to another page :
            for (int i = 1; i <= pages; i++)
            {
                if (ViewBag.Page == "P" + i.ToString())
                {
                    ViewBag.Users = db.EDU365_EduSystemsUsers.OrderByDescending(x => x.CreateTime).Skip(pagenumber * (i - 1)).Take(pagenumber).ToList();
                }
            }

        }
        



        public IActionResult Users()
        {
            ViewBag.Title = "Users";

            GetTableHeader();
            GetDataByPage();
             // ViewBag.Users = db.EDU365_EduSystemsUsers.Take(10).ToList();
            //ViewBag.Users = db.EDU365_EduSystemsUsers.Take(10).OrderByDescending(x=>x.CreateTime).Select(x=>x.CreateTime).ToList();

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