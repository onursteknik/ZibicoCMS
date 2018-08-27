using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZibicoCMS.CommonHelpers;
using ZibicoCMS.Entity;

namespace ZibicoCMS.WebAdminUI.Controllers
{
    [ZbcAuthorize]
    public class BlogsController : Controller
    {
        Helper help = new Helper();
        public ActionResult Index()
        {
            return View(help.GetAllBlogs());
        }
        public ActionResult Create(long? ID)
        {
            if (ID != null)
            {
                Blogs blog = help.GetBlog((long)ID);
                if (blog == null) return HttpNotFound();
                return View(blog);
            }
            return View();
        }
        [HttpPost]
        public ActionResult Create(Blogs blog)
        {
            HttpPostedFileBase file = Request.Files["BlogImageURL"];

            if (string.IsNullOrWhiteSpace(blog.BlogTitle))
            {
                ViewBag.MessageFail = "Blog başlığı boş bırakılamaz!";
                return View();
            }
            if (string.IsNullOrWhiteSpace(blog.BlogContent))
            {
                ViewBag.MessageFail = "Blog yazısı boş bırakılamaz!";
                return View();
            }

            string blogID = Request.QueryString["ID"];
            if (blogID == null)
            {
                if (file == null || file.FileName == "")
                {
                    ViewBag.MessageFail = "Fotoğraf boş bırakılamaz!";
                    return View();
                }

                bool result = Helper.SaveImg(file, out string path);
                blog.Status = true;
                blog.BlogImageURL = path;

                bool result2 = Helper.Insert(blog);
                if (result2)
                {
                    ViewBag.Message = "Blog yazısı başarıyla eklendi.";
                    return View();
                }
                else
                {
                    ViewBag.MessageFail = "Blog yazısı kaydedilirken bir hata meydana geldi.";
                    return View();
                }
            }
            else
            {
                Blogs oldBlog = help.GetBlog(long.Parse(blogID));
                bool result = false;
                string path = "";
                if (file != null && file.FileName != "") result = Helper.SaveImg(file, out path);
                if (result) oldBlog.BlogImageURL = path;

                oldBlog.BlogContent = blog.BlogContent;
                oldBlog.BlogTitle = blog.BlogTitle;

                Helper.Update(oldBlog);

                ViewBag.Message = "Blog yazısı başarıyla güncellendi.";
                return View(oldBlog);

            }
        }
        
        public ActionResult Delete()
        {
            string ID = Request.QueryString["ID"];
            if (ID == null) return HttpNotFound();
            Blogs blog = help.GetBlog(long.Parse(ID));
            if (blog == null) return HttpNotFound();
            Helper.Delete(blog);

            return Redirect("/Blogs/Index?Status=DeleteSuccess");
        }
    }
}