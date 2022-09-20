using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogMVC.Models;
using System.Web.Helpers;
using System.IO;

namespace BlogMVC.Controllers
{
    public class AdminBlogController : Controller
    {

        BlogDBEntities1 db = new BlogDBEntities1();
        // GET: AdminBlog
        public ActionResult Index()
        {
            var blog = db.Blogs.ToList();
            return View(blog);
        }

        // GET: AdminBlog/Details/5
        public ActionResult Details(int id)
        {
            if(id==null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blogs.Find(id);
            if (blog==null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // GET: AdminBlog/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminBlog/Create
        [HttpPost]
        public ActionResult Create(Blog blog,HttpPostedFileBase BlogFoto)
        {
            try
            {
                if (BlogFoto!=null)
                {
                    WebImage webImage = new WebImage(BlogFoto.InputStream);
                    FileInfo fileInfo = new FileInfo(BlogFoto.FileName);
                    string newFoto = Guid.NewGuid().ToString()+fileInfo.Extension;
                    webImage.Resize(800, 350);
                    webImage.Save("~/Uploads/BlogFoto/" + newFoto);
                    blog.BlogFoto = "/Uploads/BlogFoto/" + newFoto;
                }
                blog.BlogTarih = DateTime.Now;
                blog.BlogOkunmaSayisi = 0;
                db.Blogs.Add(blog);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminBlog/Edit/5
        public ActionResult Edit(int id)
        {
            if (id==null)

            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var blog = db.Blogs.Where(b => b.BlogId == id).SingleOrDefault();

            if(blog==null)
            {
                return HttpNotFound();
            }

            return View(blog);
        }

        // POST: AdminBlog/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, HttpPostedFileBase BlogFoto, Blog blog)
        {
            try
            {
                var blogguncelle = db.Blogs.Where(b => b.BlogId == id).SingleOrDefault();

                if(BlogFoto!=null)

                {
                    if (System.IO.File.Exists(Server.MapPath(blogguncelle.BlogFoto)))

                    {
                        System.IO.File.Delete(Server.MapPath(blogguncelle.BlogFoto));
                    }


                    WebImage webImage = new WebImage(BlogFoto.InputStream);
                    FileInfo fileInfo = new FileInfo(BlogFoto.FileName);
                    string newFoto = Guid.NewGuid().ToString() + fileInfo.Extension;
                    webImage.Resize(800, 350);
                    webImage.Save("~/Uploads/BlogFoto/" + newFoto);
                    blogguncelle.BlogFoto = "/Uploads/BlogFoto/" + newFoto;
                    blogguncelle.BlogBaslik = blog.BlogBaslik;
                    blogguncelle.BlogIcerik = blog.BlogIcerik;
                    blogguncelle.BlogOkunmaSuresi = blog.BlogOkunmaSuresi;
                    blogguncelle.BlogOkunmaSayisi = blog.BlogOkunmaSayisi;
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
                else
                {
                    blogguncelle.BlogBaslik = blog.BlogBaslik;
                    blogguncelle.BlogIcerik = blog.BlogIcerik;
                    blogguncelle.BlogOkunmaSuresi = blog.BlogOkunmaSuresi;
                    blogguncelle.BlogOkunmaSayisi = blog.BlogOkunmaSayisi;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View();

                
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminBlog/Delete/5
        public ActionResult Delete(int id)
        {
            var blog = db.Blogs.Where(b => b.BlogId == id).SingleOrDefault();
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // POST: AdminBlog/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var blog = db.Blogs.Where(b => b.BlogId == id).SingleOrDefault();
                if (blog==null)
                {
                    return HttpNotFound();
                }

                if (System.IO.File.Exists(Server.MapPath(blog.BlogFoto)))
                {
                    System.IO.File.Delete(Server.MapPath(blog.BlogFoto));
                }
                db.Blogs.Remove(blog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
