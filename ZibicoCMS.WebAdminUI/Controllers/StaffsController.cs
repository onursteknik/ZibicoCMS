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
    public class StaffsController : Controller
    {
        Helper help = new Helper();
        public ActionResult Index()
        {
            return View(help.GetAllStaffs());
        }
        public ActionResult Create()
        {
            string ID = Request.QueryString["ID"];
            if (ID != null)
            {
                Staffs staff = help.GetStaff(long.Parse(ID));
                if (staff == null) return HttpNotFound();
                return View(staff);
            }
            return View();
        }
        [HttpPost]
        public ActionResult Create(Staffs staff)
        {
            HttpPostedFileBase file = Request.Files["StaffImageURL"];
            List<Staffs> stf = help.GetLastStaffs(1);

            if (string.IsNullOrWhiteSpace(staff.StaffName))
            {
                ViewBag.MessageFail = "Çalışan ismi boş bırakılamaz!";
                return View();
            }

            string StaffID = Request.QueryString["ID"];
            if (StaffID == null)
            {
                if (file == null || file.FileName == "")
                {
                    ViewBag.MessageFail = "Çalışan fotoğrafı boş bırakılamaz!";
                    return View();
                }

                bool result = Helper.SaveImg(file, out string path);
                staff.Status = true;
                staff.StaffImageURL = path;
                if (stf.Count > 0) staff.StaffOrderNumber = stf[0].StaffOrderNumber + 1;
                else staff.StaffOrderNumber = 1;
                bool result2 = Helper.Insert(staff);
                if (result2)
                {
                    ViewBag.Message = "Çalışan başarıyla eklendi.";
                    return View();
                }
                else
                {
                    ViewBag.MessageFail = "Çalışan kaydedilirken bir hata meydana geldi.";
                    return View();
                }
            }
            else
            {
                Staffs oldStaff = help.GetStaff(long.Parse(StaffID));
                bool result = false;
                string path = "";
                if (file != null && file.FileName != "") result = Helper.SaveImg(file, out path);
                if (result) oldStaff.StaffImageURL = path;

                oldStaff.StaffName = staff.StaffName;
                oldStaff.StaffOrderNumber = staff.StaffOrderNumber;
                oldStaff.StaffRole = staff.StaffRole;

                Helper.Update(oldStaff);

                ViewBag.Message = "Çalışan başarıyla güncellendi.";
                return View(oldStaff);

            }
        }
        
        public ActionResult Delete()
        {
            string ID = Request.QueryString["ID"];
            if (ID == null) return HttpNotFound();
            Staffs staff = help.GetStaff(long.Parse(ID));
            if (staff == null) return HttpNotFound();
            Helper.Delete(staff);

            return Redirect("/Staffs/Index?Status=DeleteSuccess");
        }
    }
}