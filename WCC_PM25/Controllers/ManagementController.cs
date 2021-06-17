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

            // Query String是通过网址传的参数 
            if (!string.IsNullOrEmpty(Request.QueryString.Value))

            {
                //把问号去掉
                ViewBag.Page = Request.QueryString.Value.Trim('?');
            }

            using var db = new MyDbContext();

            int pagenumber = 10;//默认每一页多少行 只在算skip和一共多少页才用
            int pagecount = 10; // 默认显示页码的数量 eg 1-10，11-21.。。


            int pages = 0;// 总页码数量


            int recordcount = db.EDU365_EduSystemsUsers.Count();//一共多少个用户数据

            //找出总行数
            if (recordcount % pagenumber == 0) //如果用户数据能除净每页的行数
            {
                pages = recordcount / pagenumber; //每页的行数都一样
            }
            else
            {
                pages = recordcount / pagenumber + 1; //余出来的数据放到多出来的一页里 
            }



            ViewBag.PageCount = pagecount;//默认显示页码的数量

            ViewBag.PageNumbers = pagecount;// 实际显示页码数，eg. 余3页的情况




            //显示每一页的用户数据
            if (ViewBag.Page.IndexOf("P") == 0)
            {
                //确认选中页码
                int pageindex = int.Parse(ViewBag.Page.Replace("P", "")) - 1;   //int.Parse convert a string representation value into it's integer equivalent 

                //确认当前页码的组号
                ViewBag.PageGroup = pageindex / pagecount;
                
               

                //如果最后一组*默认每组页码数量大于总页码数量
                if ((ViewBag.PageGroup + 1) * pagecount > pages)
                {
                    ViewBag.PageNumbers = pages % pagecount; //最有一组的显示页数为余数
                }

                //显示用户数据
                ViewBag.Users = db.EDU365_EduSystemsUsers.OrderByDescending(x => x.CreateTime).Skip(pageindex * pagenumber).Take(pagenumber).ToList();




            }

            //翻到上/下一个页面
            else
            {



                //下一页
                if (ViewBag.Page.IndexOf("N") == 0)
                {
                    //先确认当前是哪个组
                    int pagegroup = int.Parse(ViewBag.Page.Substring(1));
                    //检查range
                    if ((pagegroup + 1) * pagecount < pages)
                    {
                        pagegroup++;
                    }

                    if ((pagegroup + 1) * pagecount > pages)
                    {
                        ViewBag.PageNumbers = pages % pagecount;
                    }
                    ViewBag.PageGroup = pagegroup;
                    ViewBag.Users = db.EDU365_EduSystemsUsers.OrderByDescending(x => x.CreateTime).Skip(pagegroup * pagecount * pagenumber).Take(pagenumber).ToList();


                }

                //上一页
                if (ViewBag.Page.IndexOf("V") == 0)
                {
                    //先确认当前是哪个组
                    int pagegroup = int.Parse(ViewBag.Page.Substring(1));
                    if (pagegroup > 0)
                    {
                        pagegroup--;
                    }

                    //如果总页数小于10
                    if (pagecount > pages)
                    {
                        ViewBag.PageNumber = pages % pagecount;
                    }

                    ViewBag.PageGroup = pagegroup;
                    ViewBag.Users = db.EDU365_EduSystemsUsers.OrderByDescending(x => x.CreateTime).Skip(pagegroup * pagecount * pagenumber).Take(pagenumber).ToList();


                }
                if (ViewBag.Page.IndexOf("n") == 0)
                {

                    ViewBag.PageCount = 1; 
                    int pagegroup = int.Parse(ViewBag.Page.Substring(1));
                    //检查range
                    if (pagegroup+2 < pages-pages % pagecount)
                    {
                        pagegroup++;
                    }

                    if (pagegroup+2 > pages - pages % pagecount-1)
                    {
                        ViewBag.PageNumbers = (pages+2) % pagecount;
                    }
                    ViewBag.PageGroup = pagegroup;
                    ViewBag.Users = db.EDU365_EduSystemsUsers.OrderByDescending(x => x.CreateTime).Skip(pagegroup * pagenumber).Take(pagenumber).ToList();
                }

            }
        }

        public IActionResult Users()
        {
            ViewBag.Title = "Users";

            GetTableHeader();

            GetDataByPage();

            return View();
            // GetDataByPage();
            // ViewBag.Users = db.EDU365_EduSystemsUsers.Take(10).ToList();
            //ViewBag.Users = db.EDU365_EduSystemsUsers.Take(10).OrderByDescending(x=>x.CreateTime).Select(x=>x.CreateTime).ToList();
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
    