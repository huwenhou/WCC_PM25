using Microsoft.AspNetCore.Mvc;
using System.Linq;

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

        private void GetDataByPage()
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
            if (pagecount%pagenumber == 0){
                 pages = pagecount / pagenumber;
            }
            else{
                 pages = pagecount / pagenumber + 1;
            }

            ViewBag.Pages = pages;

            for (int i = 1; i <= pages; i++)
            {
                if (ViewBag.Page == "P"+i.ToString())
                {
                    ViewBag.Users = db.EDU365_EduSystemsUsers.OrderByDescending(x => x.CreateTime).Skip(pagenumber*(i-1)).Take(pagenumber).ToList();
                }
            }
        }

        public IActionResult Users()
        {
            ViewBag.Title = "Users";

            GetTableHeader();
            GetDataByPage();

            return View();
        }

        public IActionResult Regions()
        {
            ViewBag.Title = "Regions";
            return View();
        }
    }
}
