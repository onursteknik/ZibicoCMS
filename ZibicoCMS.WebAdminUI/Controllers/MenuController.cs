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
    public class MenuController : Controller
    {
        Helper help = new Helper();
        public ActionResult Index()
        {
            return View(help.GetAllMenuFoods());
        }
        public ActionResult Create(long? ID)
        {
            if (ID != null)
            {
                MenuFoods menu = help.GetMenuFood((long)ID);
                if (menu == null) return HttpNotFound();
                return View(menu);
            }
            return View();
        }
        [HttpPost]
        public ActionResult Create(MenuFoods menu)
        {
            HttpPostedFileBase file = Request.Files["FoodImageURL"];

            if (string.IsNullOrWhiteSpace(menu.FoodTitle))
            {
                ViewBag.MessageFail = "Ürün başlığı boş bırakılamaz!";
                return View();
            }
            if (string.IsNullOrWhiteSpace(menu.FoodDescription))
            {
                ViewBag.MessageFail = "Ürün açıklaması boş bırakılamaz!";
                return View();
            }

            string MenuFoodID = Request.QueryString["ID"];
            if (MenuFoodID == null)
            {
                if (file == null || file.FileName == "")
                {
                    ViewBag.MessageFail = "Ürün fotoğrafı boş bırakılamaz!";
                    return View();
                }

                bool result = Helper.SaveImg(file, out string path);
                menu.Status = true;
                menu.FoodImageURL = path;

                bool result2 = Helper.Insert(menu);
                if (result2)
                {
                    ViewBag.Message = "Menü ürünü başarıyla eklendi.";
                    return View();
                }
                else
                {
                    ViewBag.MessageFail = "Menü ürünü kaydedilirken bir hata meydana geldi.";
                    return View();
                }
            }
            else
            {
                MenuFoods oldMenu = help.GetMenuFood(long.Parse(MenuFoodID));
                bool result = false;
                string path = "";
                if (file != null && file.FileName != "") result = Helper.SaveImg(file, out path);
                if (result) oldMenu.FoodImageURL = path;
                oldMenu.FoodTitle = menu.FoodTitle;
                oldMenu.FoodDescription = menu.FoodDescription;
                oldMenu.FoodPrice = menu.FoodPrice;

                Helper.Update(oldMenu);

                ViewBag.Message = "Menü ürünü başarıyla güncellendi.";
                return View(oldMenu);

            }
        }
        
        public ActionResult Delete()
        {
            string ID = Request.QueryString["ID"];
            if (ID == null) return HttpNotFound();
            MenuFoods menu = help.GetMenuFood(long.Parse(ID));
            if (menu == null) return HttpNotFound();
            Helper.Delete(menu);
            return Redirect("/Menu/Index?Status=DeleteSuccess");
        }
    }
}