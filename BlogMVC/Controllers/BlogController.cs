using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogMVC.Models;
using PagedList;
using PagedList.Mvc;
namespace BlogMVC.Controllers
{
    public class BlogController : Controller
    {
        BlogDBEntities1 db = new BlogDBEntities1();
        // GET: Blog
        public ActionResult Index(int page=1)
        {
            var blog = db.Blogs.OrderByDescending(b => b.BlogId).ToPagedList(page, 3);


            return View(blog);
        }

        public ActionResult Hakkinda()
        {
            var hakkinda = db.Hakkindas.ToList();


            return View(hakkinda);
        }

        public ActionResult BlogDetay(int id)
        {
            var blogDetay = db.Blogs.Where  (b => b.BlogId == id).SingleOrDefault();

            if (blogDetay == null)
            {
                return HttpNotFound();
            }
            return View(blogDetay);
        }

        public ActionResult OkunmaArttir(int blogid)
        {
            var blog = db.Blogs.Where(b=>b.BlogId == blogid).SingleOrDefault();
            blog.BlogOkunmaSayisi = blog.BlogOkunmaSayisi + 1;
            db.SaveChanges();
            return View();
        }
    }
}