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

        //Method 1
        private void GetDataByPage()
        {
            ViewBag.Page = "P1";

            if (!string.IsNullOrEmpty(Request.QueryString.Value))
            {
                ViewBag.Page = Request.QueryString.Value.Trim('?');
            }

            using var db = new MyDbContext();

            //默认每页显示多少行
            int pagenumber = 10;

            //总页码
            int pages = 0;

            //总记录行数
            int recordcount = db.EDU365_EduSystemsUsers.Count();
            if (recordcount % pagenumber == 0)
            {
                pages = recordcount / pagenumber;
            }
            else
            {
                pages = recordcount / pagenumber + 1;
            }


            //默认显示页码的数量
            int pagecount = 10;
            ViewBag.PageCount = pagecount;

            //真正显示页码数量
            ViewBag.PageNumbers = pagecount;

            if (ViewBag.Page.IndexOf("P") == 0)
            {
                //选中的页码
                int pageindex = int.Parse(ViewBag.Page.Replace("P", "")) - 1;

                //当前页码组的编号
                ViewBag.PageGroup = pageindex / pagecount;

                if ((ViewBag.PageGroup + 1) * pagecount > pages)
                {
                    ViewBag.PageNumbers = pages % pagecount;
                }

                //显示用户数据
                ViewBag.Users = db.EDU365_EduSystemsUsers.OrderByDescending(x => x.CreateTime).Skip(pageindex * pagenumber).Take(pagenumber).ToList();
            }
            else
            {
                //当前是哪个组
                int pagegroup = int.Parse(ViewBag.Page.Substring(1));

                if (ViewBag.Page.IndexOf("N") == 0)
                {
                    if ((pagegroup + 1) * pagecount < pages)
                    {
                        pagegroup ++;
                    }

                    if ((pagegroup+1) * pagecount > pages)
                    {
                        ViewBag.PageNumbers = pages % pagecount;
                    }
                }
                else
                {
                    if (pagegroup > 0)
                    {
                        pagegroup--;
                    }

                    if (pagecount > pages)
                    {
                        ViewBag.PageNumbers = pages % pagecount;
                    }
                }

                //当前页码组的编号
                ViewBag.PageGroup = pagegroup;
                ViewBag.Users = db.EDU365_EduSystemsUsers.OrderByDescending(x => x.CreateTime).Skip(pagegroup * pagecount * pagenumber).Take(pagenumber).ToList();

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
