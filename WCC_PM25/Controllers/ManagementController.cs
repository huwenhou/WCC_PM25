using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WCC_PM25.Models;

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
        private void GetDataByPage(UserViewModel data)
        {
            using var db = new MyDbContext();

            var vm = new UserViewModel();

            ViewBag.Page = "P1";

            if (!string.IsNullOrEmpty(Request.QueryString.Value))
            {
                if(Request.QueryString.Value.IndexOf("deleteuserguid") >=0)
                {
                    var deleteuserguid = Request.QueryString.Value.Replace("?deleteuserguid=", "");
                    var user = db.EDU365_EduSystemsUsers.SingleOrDefault(x => x.SystemUserGuid.Equals(deleteuserguid));
                    if(user!=null)
                    {
                        db.SingleDelete(user);
                    }
                    
                }
            }


            //默认每页显示多少行
            int pagenumber = 10;

            //总页码
            //int pages = 0;

            ////总记录行数
            //int recordcount = db.EDU365_EduSystemsUsers.Count();
            //if (recordcount % pagenumber == 0)
            //{
            //    pages = recordcount / pagenumber;
            //}
            //else
            //{
            //    pages = recordcount / pagenumber + 1;
            //}


            //默认显示页码的数量
            int pagecount = 10;
            ViewBag.PageCount = pagecount;

            //真正显示页码数量
            ViewBag.PageNumbers = pagecount;

            if (ViewBag.Page.IndexOf("P") == 0)
            {
                //选中的页码
                int pageindex = 0;
                if (!string.IsNullOrEmpty(data.Page))
                {
                    pageindex = int.Parse(data.Page)-1;
                }
                else
                {
                    pageindex = int.Parse(ViewBag.Page.Replace("P", "")) - 1;
                }

                //当前页码组的编号
                ViewBag.PageGroup = pageindex / pagecount;

   

                //显示用户数据
                if(!string.IsNullOrEmpty(data.Keyword))
                {
                    ViewBag.AllUsers = db.EDU365_EduSystemsUsers.Where(x => x.UserNameEn.Contains(data.Keyword) || x.UserEmail.Contains(data.Keyword)).OrderByDescending(x => x.CreateTime).ToList();
                    ViewBag.Users = db.EDU365_EduSystemsUsers.Where(x => x.UserNameEn.Contains(data.Keyword) || x.UserEmail.Contains(data.Keyword)).OrderByDescending(x => x.CreateTime).Skip(pageindex * pagenumber).Take(pagenumber).ToList();
                }
                else if (!string.IsNullOrEmpty(data.UserName))
                {
                    ViewBag.AllUsers = db.EDU365_EduSystemsUsers.Where(x => x.UserNameEn.Contains(data.UserName)).OrderByDescending(x => x.CreateTime).ToList();
                    ViewBag.Users = db.EDU365_EduSystemsUsers.Where(x => x.UserNameEn.Contains(data.UserName)).OrderByDescending(x => x.CreateTime).Skip(pageindex * pagenumber).Take(pagenumber).ToList();
                }
                else if (!string.IsNullOrEmpty(data.Email))
                {
                    ViewBag.AllUsers = db.EDU365_EduSystemsUsers.Where(x => x.UserNameEn.Contains(data.Email)).OrderByDescending(x => x.CreateTime).ToList();
                    ViewBag.Users = db.EDU365_EduSystemsUsers.Where(x => x.UserEmail.Contains(data.Email)).OrderByDescending(x => x.CreateTime).Skip(pageindex * pagenumber).Take(pagenumber).ToList();
                }
                else
                {
                    ViewBag.AllUsers = db.EDU365_EduSystemsUsers.OrderByDescending(x => x.CreateTime).ToList();
                    ViewBag.Users = db.EDU365_EduSystemsUsers.OrderByDescending(x => x.CreateTime).Skip(pageindex * pagenumber).Take(pagenumber).ToList();
                }

                int pages = 0;
                int recordcount = ViewBag.AllUsers.Count;
                if (recordcount % pagenumber == 0)
                {
                    pages = recordcount / pagenumber;
                }
                else
                {
                    pages = recordcount / pagenumber + 1;
                }

                if ((ViewBag.PageGroup + 1) * pagecount > pages)
                {
                    ViewBag.PageNumbers = pages % pagecount;
                }

            }
            //else
            //{
            //    //当前是哪个组
            //    int pagegroup = int.Parse(ViewBag.Page.Substring(1));

            //    if (ViewBag.Page.IndexOf("N") == 0)
            //    {
            //        if ((pagegroup + 1) * pagecount < pages)
            //        {
            //            pagegroup ++;
            //        }

            //        if ((pagegroup+1) * pagecount > pages)
            //        {
            //            ViewBag.PageNumbers = pages % pagecount;
            //        }
            //    }
            //    else
            //    {
            //        if (pagegroup > 0)
            //        {
            //            pagegroup--;
            //        }

            //        if (pagecount > pages)
            //        {
            //            ViewBag.PageNumbers = pages % pagecount;
            //        }
            //    }

            //    //当前页码组的编号
            //    ViewBag.PageGroup = pagegroup;

            //    if (!string.IsNullOrEmpty(data.Keyword))
            //    {
            //        ViewBag.AllUsers = db.EDU365_EduSystemsUsers.Where(x => x.UserNameEn.Contains(data.Keyword) || x.UserEmail.Contains(data.Keyword)).OrderByDescending(x => x.CreateTime).ToList();
            //        ViewBag.Users = db.EDU365_EduSystemsUsers.Where(x => x.UserNameEn.Contains(data.Keyword) || x.UserEmail.Contains(data.Keyword)).OrderByDescending(x => x.CreateTime).Skip(pagegroup * pagecount * pagenumber).Take(pagenumber).ToList();
            //    }
            //    else if (!string.IsNullOrEmpty(data.UserName))
            //    {
            //        ViewBag.AllUsers = db.EDU365_EduSystemsUsers.Where(x => x.UserNameEn.Contains(data.UserName)).OrderByDescending(x => x.CreateTime).ToList();
            //        ViewBag.Users = db.EDU365_EduSystemsUsers.Where(x => x.UserNameEn.Contains(data.UserName)).OrderByDescending(x => x.CreateTime).Skip(pagegroup * pagecount * pagenumber).Take(pagenumber).ToList();
            //    }
            //    else if (!string.IsNullOrEmpty(data.Email))
            //    {
            //        ViewBag.AllUsers = db.EDU365_EduSystemsUsers.Where(x => x.UserNameEn.Contains(data.Email)).OrderByDescending(x => x.CreateTime).ToList();
            //        ViewBag.Users = db.EDU365_EduSystemsUsers.Where(x => x.UserEmail.Contains(data.Email) ).OrderByDescending(x => x.CreateTime).Skip(pagegroup * pagecount * pagenumber).Take(pagenumber).ToList();
            //    }
            //    else
            //    {
            //        ViewBag.AllUsers = db.EDU365_EduSystemsUsers.OrderByDescending(x => x.CreateTime).ToList();
            //        ViewBag.Users = db.EDU365_EduSystemsUsers.OrderByDescending(x => x.CreateTime).Skip(pagegroup * pagecount * pagenumber).Take(pagenumber).ToList();
            //    }

            //    //
            //    recordcount = ViewBag.AllUsers.Count;
            //    if (recordcount % pagenumber == 0)
            //    {
            //        pages = recordcount / pagenumber;
            //    }
            //    else
            //    {
            //        pages = recordcount / pagenumber + 1;
            //    }

            //    if (ViewBag.Page.IndexOf("N") == 0)
            //    {
            //        if ((pagegroup + 1) * pagecount < pages)
            //        {
            //            pagegroup++;
            //        }

            //        if ((pagegroup + 1) * pagecount > pages)
            //        {
            //            ViewBag.PageNumbers = pages % pagecount;
            //        }
            //    }
            //    else
            //    {
            //        if (pagegroup > 0)
            //        {
            //            pagegroup--;
            //        }

            //        if (pagecount > pages)
            //        {
            //            ViewBag.PageNumbers = pages % pagecount;
            //        }
            //    }
            //}

            //ViewBag.PageNumbers = ViewBag.Users.Count;
        }

        public IActionResult Users(UserViewModel data)
        {
            ViewBag.Title = "Users";

            GetTableHeader();
            GetDataByPage(data);
            
            return View();
        }

        public IActionResult Regions()
        {
            ViewBag.Title = "Regions";
            return View();
        }
    }
}
