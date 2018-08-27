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
    public class EditorsController : Controller
    {
        Helper help = new Helper();
        public ActionResult Index()
        {
            return View(help.GetAllEditors());
        }
        public ActionResult Create()
        {
            string ID = Request.QueryString["ID"];
            if (ID != null)
            {
                Editors editor = help.GetEditor(long.Parse(ID));
                if (editor == null) return HttpNotFound();
                return View(editor);
            }
            return View();
        }
        [HttpPost]
        public ActionResult Create(Editors editor)
        {
            HttpPostedFileBase file = Request.Files["ImageURL"];

            if (string.IsNullOrWhiteSpace(editor.Nickname))
            {
                ViewBag.MessageFail = "Kullanıcı adı boş bırakılamaz!";
                return View();
            }
            if (string.IsNullOrWhiteSpace(editor.Firstname) || string.IsNullOrWhiteSpace(editor.Lastname))
            {
                ViewBag.MessageFail = "Editör adı-soyadı boş bırakılamaz!";
                return View();
            }
            if (string.IsNullOrWhiteSpace(editor.Email))
            {
                ViewBag.MessageFail = "Email boş bırakılamaz!";
                return View();
            }
            if (string.IsNullOrWhiteSpace(editor.HomeAddress))
            {
                ViewBag.MessageFail = "Adres boş bırakılamaz!";
                return View();
            }
            if (string.IsNullOrWhiteSpace(editor.StaffDescription))
            {
                ViewBag.MessageFail = "Tanım boş bırakılamaz!";
                return View();
            }

            string EditorID = Request.QueryString["ID"];
            if (EditorID == null)
            {
                if (string.IsNullOrWhiteSpace(editor.Password))
                {
                    ViewBag.MessageFail = "Parola boş bırakılamaz!";
                    return View();
                }
                if (file == null || file.FileName == "")
                {
                    ViewBag.MessageFail = "Editör fotoğrafı boş bırakılamaz!";
                    return View();
                }

                bool result = Helper.SaveImg(file, out string path);
                editor.Status = true;
                editor.ImageURL = path;
                editor.Password = Helper.MD5Hex(editor.Password).ToUpper();

                Editors checkEditor = help.GetEditor(editor.Nickname, editor.Email);
                if (checkEditor != null)
                {
                    ViewBag.Message = "Bu kullanıcı adı veya email ile kayıtlı bir editör zaten var!";
                    return View();
                }
                bool result2 = Helper.Insert(editor);
                if (result2)
                {
                    ViewBag.Message = "Editör başarıyla eklendi.";
                    return View();
                }
                else
                {
                    ViewBag.MessageFail = "Editör kaydedilirken bir hata meydana geldi.";
                    return View();
                }
            }
            else
            {
                Editors oldEditor = help.GetEditor(long.Parse(EditorID));
                bool result = false;
                string path = "";
                if (file != null && file.FileName != "") result = Helper.SaveImg(file, out path);
                if (result) oldEditor.ImageURL = path;

                if (!string.IsNullOrEmpty(editor.Password))
                    oldEditor.Password = Helper.MD5Hex(editor.Password).ToUpper();
                oldEditor.Nickname = editor.Nickname;
                oldEditor.Firstname = editor.Firstname;
                oldEditor.Lastname = editor.Lastname;
                oldEditor.StaffDescription = editor.StaffDescription;
                oldEditor.HomeAddress = editor.HomeAddress;
                oldEditor.Email = editor.Email;

                Helper.Update(oldEditor);

                ViewBag.Message = "Editör başarıyla güncellendi.";
                return View(oldEditor);
            }
        }
        
        public ActionResult Delete()
        {
            string ID = Request.QueryString["ID"];
            if (ID == null) return HttpNotFound();
            Editors editor = help.GetEditor(long.Parse(ID));
            if (editor == null) return HttpNotFound();

            Helpers.DeletedEditors.Add(editor);
            Helper.Delete(editor);



            return Redirect("/Editors/Index?Status=DeleteSuccess");
        }
    }
}