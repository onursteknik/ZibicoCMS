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
    public class ProductsController : Controller
    {
        Helper help = new Helper();
        public ActionResult Index()
        {
            return View(help.GetAllProducts());
        }
        public ActionResult Create(long? ID)
        {
            ViewBag.Categories = help.GetAllCategories();
            if (ID != null)
            {
                Products prd = help.GetProduct((long)ID);
                if (prd == null) return HttpNotFound();
                return View(prd);
            }
            return View();
        }
        [HttpPost]
        public ActionResult Create(Products product)
        {
            ViewBag.Categories = help.GetAllCategories();
            HttpPostedFileBase file = Request.Files["ProductImageURL"];

            if (string.IsNullOrWhiteSpace(product.ProductName))
            {
                ViewBag.MessageFail = "Ürün adı boş bırakılamaz!";
                return View();
            }
            if (string.IsNullOrWhiteSpace(product.ProductDescription))
            {
                ViewBag.MessageFail = "Ürün açıklaması boş bırakılamaz!";
                return View();
            }

            string ProductID = Request.QueryString["ID"];
            if (ProductID == null)
            {
                if (file == null || file.FileName == "")
                {
                    ViewBag.MessageFail = "Ürün fotoğrafı boş bırakılamaz!";
                    return View();
                }

                bool result = Helper.SaveImg(file, out string path);
                product.Status = true;
                product.ProductImageURL = path;

                bool result2 = Helper.Insert(product);
                if (result2)
                {
                    ViewBag.Message = "Ürün başarıyla eklendi.";
                    return View();
                }
                else
                {
                    ViewBag.MessageFail = "Ürün kaydedilirken bir hata meydana geldi.";
                    return View();
                }
            }
            else
            {
                Products oldPrd = help.GetProduct(long.Parse(ProductID));
                bool result = false;
                string path = "";
                if (file != null && file.FileName != "") result = Helper.SaveImg(file, out path);
                if (result) oldPrd.ProductImageURL = path;

                oldPrd.ProductName = product.ProductName;
                oldPrd.ProductDescription = product.ProductDescription;
                oldPrd.CategoryID = product.CategoryID;

                Helper.Update(oldPrd);

                ViewBag.Message = "Ürün başarıyla güncellendi.";
                return View(oldPrd);

            }
        }
        
        public ActionResult Delete()
        {
            string ID = Request.QueryString["ID"];
            if (ID == null) return HttpNotFound();
            Products prd = help.GetProduct(long.Parse(ID));
            if (prd == null) return HttpNotFound();
            Helper.Delete(prd);

            return Redirect("/Products/Index?Status=DeleteSuccess");
        }
    }
}