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
    public class CategoriesController : Controller
    {
        Helper help = new Helper();
        public ActionResult Index()
        {
            return View(help.GetAllCategories());
        }
        public ActionResult Create(long? ID)
        {
            if (ID != null)
            {
                Categories cat = help.GetCategory((long)ID);
                if (cat == null) return HttpNotFound();
                return View(cat);
            }
            return View();
        }
        [HttpPost]
        public ActionResult Create(Categories cat)
        {
            HttpPostedFileBase file = Request.Files["CategoryIcon"];

            if (string.IsNullOrWhiteSpace(cat.CategoryName))
            {
                ViewBag.MessageFail = "Kategori adı boş bırakılamaz!";
                return View();
            }
            if (string.IsNullOrWhiteSpace(cat.CategoryDescription))
            {
                ViewBag.MessageFail = "Kategori açıklaması boş bırakılamaz!";
                return View();
            }

            string CatID = Request.QueryString["ID"];
            if (CatID == null)
            {
                bool result = false;
                string path = "";
                if (file != null && file.FileName != "")  result = Helper.SaveImg(file, out path);
                cat.Status = true;
                if (result) cat.CategoryIcon = path;
                
                bool result2 = Helper.Insert(cat);
                if (result2)
                {
                    ViewBag.Message = "Kategori başarıyla eklendi.";
                    return View();
                }
                else
                {
                    ViewBag.MessageFail = "Kategori kaydedilirken bir hata meydana geldi.";
                    return View();
                }
            }
            else
            {
                Categories oldCat = help.GetCategory(long.Parse(CatID));
                bool result = false;
                string path = "";
                if (file != null && file.FileName != "") result = Helper.SaveImg(file, out path);
                if (result) oldCat.CategoryIcon = path;

                oldCat.CategoryName = cat.CategoryName;
                oldCat.CategoryDescription = cat.CategoryDescription;

                Helper.Update(oldCat);

                ViewBag.Message = "Kategori başarıyla güncellendi.";
                return View(oldCat);
            }
        }
        
        public ActionResult Delete()
        {
            string ID = Request.QueryString["ID"];
            if (ID == null) return HttpNotFound();
            Categories cat = help.GetCategory(long.Parse(ID));
           List<Products> products = Helper.Select<Products>("CategoryID=" + cat.ID);
            if (products!=null && products.Count>0) return Redirect("/Categories/Index?Status=DeleteFailed");
            if (cat == null) return HttpNotFound();
            Helper.Delete(cat);

            return Redirect("/Categories/Index?Status=DeleteSuccess");
        }
    }
}