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
    public class ProfileController : Controller
    {
        Helper help = new Helper();
        // GET: Profile
        public ActionResult Index()
        {
            return View(Helpers.Info);
        }

        [HttpPost]
        public ActionResult Index(Editors editor)
        {
            string EditorID = Request.QueryString["ID"];
            Editors oldEditor = help.GetEditor(editor.ID);
            HttpPostedFileBase file = Request.Files["ImageURL"];


            if (string.IsNullOrWhiteSpace(editor.Firstname) || string.IsNullOrWhiteSpace(editor.Lastname))
            {
                ViewBag.MessageFail = "Editör adı-soyadı boş bırakılamaz!";
                return View(oldEditor);
            }
            if (string.IsNullOrWhiteSpace(editor.Email))
            {
                ViewBag.MessageFail = "Email boş bırakılamaz!";
                return View(oldEditor);
            }

            
            bool result = false;
            string path = "";
            if (file != null && file.FileName != "") result = Helper.SaveImg(file, out path);
            if (result) oldEditor.ImageURL = path;

            if (!string.IsNullOrEmpty(editor.Password))
                oldEditor.Password = Helper.MD5Hex(editor.Password).ToUpper();
            oldEditor.Firstname = editor.Firstname;
            oldEditor.Lastname = editor.Lastname;
            oldEditor.Email = editor.Email;

            Helper.Update(oldEditor);

            ViewBag.Message = "Profil başarıyla güncellendi.";
            return View(oldEditor);
        }
    }
}