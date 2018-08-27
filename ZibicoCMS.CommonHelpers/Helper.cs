using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Configuration;
using Dapper;
using Dapper.Contrib.Extensions;
using ZibicoCMS.Entity;
using System.IO;
using System.Security.Cryptography;
using System.Linq.Expressions;
using System.Net;
using System.Threading;

namespace ZibicoCMS.CommonHelpers
{
    public class Helper
    {
        private SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["CMSConString"].ConnectionString);


        private static List<GeneralSettings> _settings;
        public static List<GeneralSettings> Settings
        {
            get
            {
                if (_settings == null)
                {
                    Helper h = new Helper();
                    _settings = h.GetAllSettings();
                }
                return _settings;
            }
            set { _settings = value; }
        }
        public static void RefreshSettingsCache(string referrer)
        {
            Helper h = new Helper();
            _settings = h.GetAllSettings();

            if (referrer != "WebUI")
            {
                try
                {
                    HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(Helper.Settings.GetValue("WebUIPath") + "/Home/ClearSettingsCachePage");
                    httpWebRequest.Method = "GET";
                    httpWebRequest.Timeout = 10000;
                    HttpWebResponse httpWebesponse = (HttpWebResponse)httpWebRequest.GetResponse();
                }
                catch (Exception ex)
                {
                    return;
                }
            }
        }
        public static string MD5Hex(string Expression)
        {
            MD5 m = MD5.Create();
            string result = string.Empty;
            byte[] _hash = m.ComputeHash(Encoding.Default.GetBytes(Expression));
            foreach (byte _byte in _hash)
                result += _byte.ToString("X2");
            return result;
        }


        #region GENERIC METHODS
        public static List<T> Select<T>(string whereCommand = "")
        {
            try
            {
                Helper h = new Helper();
                if (whereCommand != "") whereCommand = " where " + whereCommand;
                var sql = "Select * from " + typeof(T).Name + whereCommand;
                return h.con.Query<T>(sql).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static bool Update<T>(T entity)
        {
            bool result = false;
            try
            {
                Helper h = new Helper();
                PropertyInfo[] props = typeof(T).GetProperties();
                string command = "";

                for (int i = 0; i < props.Length; i++)
                {
                    if (i == 0) continue;
                    object obj = entity.GetType().GetProperty(props[i].Name).GetValue(entity, null);
                    if (obj == null) continue;
                    if (i == props.Length - 1)
                    {
                        string cmd = props[i].Name + "=@" + props[i].Name;
                        command += cmd;
                    }
                    else
                    {
                        string cmd = props[i].Name + "=@" + props[i].Name + ",";
                        command += cmd;
                    }
                }
                var sql = "Update " + typeof(T).Name + " set " + command + " where ID=" + entity.GetType().GetProperty(props[0].Name).GetValue(entity, null) + ";";
                int resultExecuteNonQuery = h.con.Execute(sql, entity);
                if (resultExecuteNonQuery == 1)
                    result = true;
            }
            catch (Exception)
            {
                return false;
            }
            return result;
        }
        public static bool Insert<T>(T entity)
        {
            bool result = false;
            try
            {
                Helper h = new Helper();
                PropertyInfo[] props = typeof(T).GetProperties();
                string commandProps = "";
                string commandValues = "";
                for (int i = 0; i < props.Length; i++)
                {
                    if (i == 0) continue;
                    if (i == props.Length - 1)
                    {
                        commandProps += props[i].Name;
                        commandValues += "@" + props[i].Name;
                    }
                    else
                    {
                        commandProps += props[i].Name + ",";
                        commandValues += "@" + props[i].Name + ",";
                    }
                }
                var sql = "Insert into " + typeof(T).Name + "(" + commandProps + ") VALUES (" + commandValues + ");";
                int resultExecuteNonQuery = h.con.Execute(sql, entity);
                if (resultExecuteNonQuery == 1)
                    result = true;
            }
            catch (Exception)
            {
                return false;
            }
            return result;
        }
        public static bool Delete<T>(T entity)
        {
            bool result = false;
            try
            {
                Helper h = new Helper();
                PropertyInfo[] props = typeof(T).GetProperties();

                var sql = "Delete from " + typeof(T).Name + " where ID=" + entity.GetType().GetProperty(props[0].Name).GetValue(entity, null);
                int resultExecuteNonQuery = h.con.Execute(sql, entity);
                if (resultExecuteNonQuery == 1)
                    result = true;
            }
            catch (Exception)
            {
                return false;
            }
            return result;
        }

        public static bool SaveImg(HttpPostedFileBase file, out string path)
        {
            WebImage img = new WebImage(file.InputStream);
            string photo = "";
            try
            {
                FileInfo photoInfo = new FileInfo(file.FileName);
                if (img.GetBytes().Count() > 4097152)
                {
                    path = null;
                    return false;
                }

                photo = Guid.NewGuid().ToString() + photoInfo.Extension;
                string tempPath = "~/Uploads/" + photo;
                img.Save("~/Uploads/" + photo, null, false);
                //FtpWebRequest request =(FtpWebRequest)WebRequest.Create(Helper.Settings.GetValue("FTPAddress") + @"//"+ photo);
                //request.Credentials = new NetworkCredential(Helper.Settings.GetValue("FTPUsername"), Helper.Settings.GetValue("FTPPassword"));
                //request.Method = WebRequestMethods.Ftp.UploadFile;
                WebClient ftp = new WebClient();
                ftp.Credentials = new NetworkCredential(Helper.Settings.GetValue("FTPUsername"), Helper.Settings.GetValue("FTPPassword"));
                ftp.UploadFile(Helper.Settings.GetValue("FTPAddress") + "/" + photo, HttpContext.Current.Server.MapPath(tempPath));

                //using (Stream fileStream = File.OpenRead(HttpContext.Current.Server.MapPath(tempPath)))
                //using (Stream ftpStream = request.GetRequestStream())
                //{
                //    fileStream.CopyTo(ftpStream);
                //}
                //File.Delete(HttpContext.Current.Server.MapPath(tempPath));

                path = "/Uploads/" + photo;

                //Helper.RefreshSettingsCache("WebAdminUI");
                return true;
            }
            catch (Exception ex)
            {
                path = ex.Message;
                return false;
            }


        }

        #endregion
        #region METHODS

        #region GeneralSettings
        public List<GeneralSettings> GetAllSettings()
        {
            var sql = "SELECT * FROM GeneralSettings;";
            return con.Query<GeneralSettings>(sql).ToList();
        }
        public GeneralSettings GetSetting(long ID)
        {
            var sql = "SELECT * FROM GeneralSettings where ID=@ID;";
            List<GeneralSettings> model = con.Query<GeneralSettings>(sql, new { ID = ID }).ToList();
            return model[0];
        }
        public bool SaveSetting(GeneralSettings setting)
        {
            bool result = false;
            var updatePlayerIDSql = "Update GeneralSettings set SettingKey=@key , SettingValue=@value where ID=@id;";
            int resultExecuteNonQuery = con.Execute(updatePlayerIDSql, new { id = setting.ID, key = setting.SettingKey, value = setting.SettingValue });
            if (resultExecuteNonQuery == 1)
                result = true;
            return result;
        }
        #endregion

        #region Announcements
        public List<Announcements> GetAllAnnouncements()
        {
            var sql = "SELECT * FROM Announcements where Status=1 ORDER BY ID DESC;";
            return con.Query<Announcements>(sql).ToList();

        }
        public List<Announcements> GetLastAnnouncements(int top)
        {
            var sql = "SELECT TOP " + top + " * FROM Announcements where Status=1 ORDER BY ID DESC;";
            return con.Query<Announcements>(sql).ToList();
        }
        public Announcements GetAnnouncement(long ID)
        {
            var sql = "SELECT * FROM Announcements where ID=@ID and Status=1;";
            List<Announcements> model = con.Query<Announcements>(sql, new { ID = ID }).ToList();
            return model[0];
        }
        public bool AddAnnouncement(Announcements anno)
        {
            bool result = false;
            var sql = "INSERT INTO Announcements (AnnouncementTitle,AnnouncementContent,CreateDate,Status) values (@title,@content,@date,@status);";
            int resultExecuteNonQuery = con.Execute(sql, new { title = anno.AnnouncementTitle, content = anno.AnnouncementContent, date = DateTime.Now, status = true });
            if (resultExecuteNonQuery == 1)
                result = true;
            return result;
        }
        public bool UpdateAnnouncement(Announcements anno)
        {
            bool result = false;
            var updatePlayerIDSql = "Update Announcements set AnnouncementTitle=@title,AnnouncementContent=@content,CreateDate=@date,Status=@status where ID=@id;";
            int resultExecuteNonQuery = con.Execute(updatePlayerIDSql, new { title = anno.AnnouncementTitle, content = anno.AnnouncementContent, date = DateTime.Now, status = anno.Status });
            if (resultExecuteNonQuery == 1)
                result = true;
            return result;
        }
        #endregion

        #region Blogs
        public List<Blogs> GetAllBlogs()
        {
            var sql = "SELECT * FROM Blogs where Status=1 ORDER BY ID DESC;";
            return con.Query<Blogs>(sql).ToList();
        }
        public List<Blogs> GetLastBlogs(int top)
        {
            var sql = "SELECT TOP " + top + " * FROM Blogs where Status=1 ORDER BY ID DESC;";
            return con.Query<Blogs>(sql).ToList();
        }
        public Blogs GetBlog(long ID)
        {
            var sql = "SELECT * FROM Blogs where ID=@ID and Status=1;";
            List<Blogs> model = con.Query<Blogs>(sql, new { ID = ID }).ToList();
            return model[0];
        }
        public bool AddBlog(Blogs blog)
        {
            bool result = false;
            var sql = "INSERT INTO Blogs (BlogTitle,BlogContent,BlogImageURL,BlogSeoURL,Status) values (@title,@content,@url,@seourl,@status);";
            int resultExecuteNonQuery = con.Execute(sql, new { title = blog.BlogTitle, content = blog.BlogContent, url = blog.BlogImageURL, seourl = blog.BlogSeoURL, status = true });
            if (resultExecuteNonQuery == 1)
                result = true;
            return result;
        }
        public bool UpdateBlog(Blogs blog)
        {
            bool result = false;
            var sql = "Update Blogs set BlogTitle=@title,BlogContent=@content,BlogImageURL=@url,BlogSeoURL=@seourl,Status=@status where ID=@id;";
            int resultExecuteNonQuery = con.Execute(sql, new { title = blog.BlogTitle, content = blog.BlogContent, url = blog.BlogImageURL, seourl = blog.BlogSeoURL, status = true });
            if (resultExecuteNonQuery == 1)
                result = true;
            return result;
        }
        #endregion

        #region Categories
        public List<Categories> GetAllCategories()
        {
            var sql = "SELECT * FROM Categories where Status=1 ORDER BY ID DESC;";
            return con.Query<Categories>(sql).ToList();
        }
        public List<Categories> GetLastCategories(int top)
        {
            var sql = "SELECT TOP " + top + " * FROM Categories where Status=1 ORDER BY ID DESC;";
            return con.Query<Categories>(sql).ToList();
        }
        public Categories GetCategory(long ID)
        {
            var sql = "SELECT * FROM Categories where ID=@ID and Status=1;";
            List<Categories> model = con.Query<Categories>(sql, new { ID = ID }).ToList();
            return model[0];
        }
        public string GetCategoryName(long ID)
        {
            var sql = "SELECT CategoryName FROM Categories where ID=@ID and Status=1;";
            List<Categories> model = con.Query<Categories>(sql, new { ID = ID }).ToList();
            return model[0].CategoryName;
        }
        public bool AddCategory(Categories category)
        {
            bool result = false;
            var sql = "INSERT INTO Categories (CategoryName,CategoryDescription,CategoryIcon,Status) values (@name,@desc,@icon,@status);";
            int resultExecuteNonQuery = con.Execute(sql, new { name = category.CategoryName, desc = category.CategoryDescription, icon = category.CategoryIcon, status = true });
            if (resultExecuteNonQuery == 1)
                result = true;
            return result;
        }
        public bool UpdateCategory(Categories category)
        {
            bool result = false;
            var sql = "Update Categories set CategoryName=@name,CategoryDescription=@desc,CategoryIcon=@icon,Status=@status where ID=@id;";
            int resultExecuteNonQuery = con.Execute(sql, new { name = category.CategoryName, desc = category.CategoryDescription, icon = category.CategoryIcon, status = true });
            if (resultExecuteNonQuery == 1)
                result = true;
            return result;
        }
        #endregion

        #region Editors
        public List<Editors> GetAllEditors()
        {
            var sql = "SELECT * FROM Editors where Status=1 ORDER BY ID DESC;";
            return con.Query<Editors>(sql).ToList();
        }
        public List<Editors> GetLastEditors(int top)
        {
            var sql = "SELECT TOP " + top + " * FROM Editors where Status=1 ORDER BY ID DESC;";
            return con.Query<Editors>(sql).ToList();
        }
        public Editors GetEditor(long ID)
        {
            var sql = "SELECT * FROM Editors where ID=@ID and Status=1;";
            List<Editors> model = con.Query<Editors>(sql, new { ID = ID }).ToList();
            return model[0];
        }
        public Editors GetEditor(string Nickname, string Email)
        {
            var sql = "SELECT * FROM Editors where (Nickname=@nick or Email=@email ) and Status=1;";
            List<Editors> model = con.Query<Editors>(sql, new { nick = Nickname, email = Email }).ToList();
            return model[0];
        }
        public bool AddEditor(Editors editor)
        {
            bool result = false;
            var sql = "INSERT INTO Editors (Firstname,Lastname,Nickname,Password,Email,ImageURL,HomeAddress,StaffDescription,Status) values (@firstname,@lastname,@nick,@pass,@email,@url,@address,@desc,@status);";
            int resultExecuteNonQuery = con.Execute(sql, new { firstname = editor.Firstname, lastname = editor.Lastname, nick = editor.Nickname, pass = editor.Password, email = editor.Email, url = editor.ImageURL, address = editor.HomeAddress, desc = editor.StaffDescription, status = true });
            if (resultExecuteNonQuery == 1)
                result = true;
            return result;
        }
        public bool UpdateEditor(Editors editor)
        {
            bool result = false;
            var sql = "Update Editors set Firstname=@firstname,Lastname=@lastname,Nickname=@nick,Password=@pass,Email=@email,ImageURL=@url,HomeAddress=@address,StaffDescription=@desc,Status=@status where ID=@id;";
            int resultExecuteNonQuery = con.Execute(sql, new { firstname = editor.Firstname, lastname = editor.Lastname, nick = editor.Nickname, pass = editor.Password, email = editor.Email, url = editor.ImageURL, address = editor.HomeAddress, desc = editor.StaffDescription, status = true });
            if (resultExecuteNonQuery == 1)
                result = true;
            return result;
        }

        public Editors LoginControl(Editors editor)
        {
            var sql = "SELECT * FROM Editors where Nickname=@nick and Password=@pass and Status=1;";
            List<Editors> model = con.Query<Editors>(sql, new { nick = editor.Nickname, pass = Helper.MD5Hex(editor.Password) }).ToList();
            if (model.Count > 0)
                return model[0];
            return null;
        }
        public Editors CookieControl(Editors editor)
        {
            var sql = "SELECT * FROM Editors where Nickname=@nick and SecretKey=@key and Status=1;";
            List<Editors> model = con.Query<Editors>(sql, new { nick = editor.Nickname, key = editor.SecretKey }).ToList();
            if (model.Count > 0)
                return model[0];
            return null;
        }


        #endregion

        #region MenuFoods
        public List<MenuFoods> GetAllMenuFoods()
        {
            var sql = "SELECT * FROM MenuFoods where Status=1 ORDER BY ID DESC;";
            return con.Query<MenuFoods>(sql).ToList();
        }
        public List<MenuFoods> GetLastMenuFoods(int top)
        {
            var sql = "SELECT TOP " + top + " * FROM MenuFoods where Status=1 ORDER BY ID DESC;";
            return con.Query<MenuFoods>(sql).ToList();
        }
        public MenuFoods GetMenuFood(long ID)
        {
            var sql = "SELECT * FROM MenuFoods where ID=@ID and Status=1;";
            List<MenuFoods> model = con.Query<MenuFoods>(sql, new { ID = ID }).ToList();
            return model[0];
        }
        public bool AddMenuFood(MenuFoods menu)
        {
            bool result = false;
            var sql = "INSERT INTO MenuFoods (FoodTitle,FoodDescription,FoodPrice,FoodImageURL,Status) values (@title,@desc,@price,@url,@status);";
            int resultExecuteNonQuery = con.Execute(sql, new { title = menu.FoodTitle, desc = menu.FoodDescription, price = menu.FoodPrice, url = menu.FoodImageURL, status = true });
            if (resultExecuteNonQuery == 1)
                result = true;
            return result;
        }
        public bool UpdateMenuFood(MenuFoods menu)
        {
            bool result = false;
            var sql = "Update MenuFoods set Firstname=@firstname,Lastname=@lastname,Nickname=@nick,Password=@pass,Email=@email,ImageURL=@url,HomeAddress=@address,StaffDescription=@desc,Status=@status where ID=@id;";
            int resultExecuteNonQuery = con.Execute(sql, new { title = menu.FoodTitle, desc = menu.FoodDescription, price = menu.FoodPrice, url = menu.FoodImageURL, status = true });
            if (resultExecuteNonQuery == 1)
                result = true;
            return result;
        }
        #endregion

        #region PhotoGalleries
        public List<PhotoGalleries> GetAllPhotoGalleries()
        {
            var sql = "SELECT * FROM PhotoGalleries where Status=1 ORDER BY ID DESC;";
            return con.Query<PhotoGalleries>(sql).ToList();
        }
        public List<PhotoGalleries> GetLastPhotoGalleries(int top)
        {
            var sql = "SELECT TOP " + top + " * FROM PhotoGalleries where Status=1 ORDER BY ID DESC;";
            return con.Query<PhotoGalleries>(sql).ToList();
        }
        public PhotoGalleries GetPhotoGallery(long ID)
        {
            var sql = "SELECT * FROM PhotoGalleries where ID=@ID and Status=1;";
            List<PhotoGalleries> model = con.Query<PhotoGalleries>(sql, new { ID = ID }).ToList();
            return model[0];
        }
        public bool AddGallery(PhotoGalleries gallery)
        {
            bool result = false;
            var sql = "INSERT INTO PhotoGalleries (ImageURL,ImageDescription,Status) values (@url,@desc,@status);";
            int resultExecuteNonQuery = con.Execute(sql, new { url = gallery.ImageURL, desc = gallery.ImageDescription, status = true });
            if (resultExecuteNonQuery == 1)
                result = true;
            return result;
        }
        public bool UpdateGallery(PhotoGalleries gallery)
        {
            bool result = false;
            var sql = "Update PhotoGalleries set ImageURL=@url,ImageDescription=@desc,Status=@status where ID=@id;";
            int resultExecuteNonQuery = con.Execute(sql, new { url = gallery.ImageURL, desc = gallery.ImageDescription, status = true });
            if (resultExecuteNonQuery == 1)
                result = true;
            return result;
        }
        #endregion

        #region Products
        public List<Products> GetAllProducts()
        {
            var sql = "SELECT * FROM Products where Status=1 ORDER BY ID DESC;";
            return con.Query<Products>(sql).ToList();
        }
        public List<Products> GetLastProducts(int top)
        {
            var sql = "SELECT TOP " + top + " * FROM Products where Status=1 ORDER BY ID DESC;";
            return con.Query<Products>(sql).ToList();
        }
        public Products GetProduct(long ID)
        {
            var sql = "SELECT * FROM Products where ID=@ID and Status=1;";
            List<Products> model = con.Query<Products>(sql, new { ID = ID }).ToList();
            return model[0];
        }
        public bool AddProduct(Products product)
        {
            bool result = false;
            var sql = "INSERT INTO Products (CategoryID,ProductName,ProductDescription,ProductImageURL,Status) values (@catID,@name,@desc,@url,@status);";
            int resultExecuteNonQuery = con.Execute(sql, new { catID = product.CategoryID, name = product.ProductName, desc = product.ProductDescription, url = product.ProductImageURL, status = true });
            if (resultExecuteNonQuery == 1)
                result = true;
            return result;
        }
        public bool UpdateProduct(Products product)
        {
            bool result = false;
            var sql = "Update Products set CategoryID=@catID,ProductName=@name,ProductDescription=@desc,ProductImageURL=@url,Status=@status where ID=@id;";
            int resultExecuteNonQuery = con.Execute(sql, new { catID = product.CategoryID, name = product.ProductName, desc = product.ProductDescription, url = product.ProductImageURL, status = true });
            if (resultExecuteNonQuery == 1)
                result = true;
            return result;
        }
        #endregion

        #region Reservations
        public List<Reservations> GetAllReservations()
        {
            var sql = "SELECT * FROM Reservations where Status=1 ORDER BY ID DESC;";
            return con.Query<Reservations>(sql).ToList();
        }
        public List<Reservations> GetLastReservations(int top)
        {
            var sql = "SELECT TOP " + top + " * FROM Reservations where Status=1 ORDER BY ID DESC;";
            return con.Query<Reservations>(sql).ToList();
        }
        public Reservations GetReservation(long ID)
        {
            var sql = "SELECT * FROM Reservations where ID=@ID and Status=1;";
            List<Reservations> model = con.Query<Reservations>(sql, new { ID = ID }).ToList();
            return model[0];
        }
        public bool AddReservation(Reservations reservation)
        {
            bool result = false;
            var sql = "INSERT INTO Reservations (Name,Email,PhoneNumber,Status) values (@name,@email,@phone,@status);";
            int resultExecuteNonQuery = con.Execute(sql, new { name = reservation.Name, email = reservation.PhoneNumber, phone = reservation.PhoneNumber, status = true });
            if (resultExecuteNonQuery == 1)
                result = true;
            return result;
        }
        public bool UpdateReservation(Reservations reservation)
        {
            bool result = false;
            var sql = "Update Reservations set Name=@name,Email=@email,PhoneNumber=@phone,Status=@status where ID=@id;";
            int resultExecuteNonQuery = con.Execute(sql, new { name = reservation.Name, email = reservation.PhoneNumber, phone = reservation.PhoneNumber, status = true });
            if (resultExecuteNonQuery == 1)
                result = true;
            return result;
        }
        #endregion

        #region SocialMedia
        public List<SocialMedia> GetAllSocialMedias()
        {
            var sql = "SELECT * FROM SocialMedia where Status=1 ORDER BY ID DESC;";
            return con.Query<SocialMedia>(sql).ToList();
        }
        public List<SocialMedia> GetLastSocialMedias(int top)
        {
            var sql = "SELECT TOP " + top + " * FROM SocialMedia where Status=1 ORDER BY ID DESC;";
            return con.Query<SocialMedia>(sql).ToList();
        }
        public SocialMedia GetSocialMedia(long ID)
        {
            var sql = "SELECT * FROM SocialMedia where ID=@ID and Status=1;";
            List<SocialMedia> model = con.Query<SocialMedia>(sql, new { ID = ID }).ToList();
            return model[0];
        }
        public bool AddSocialMedia(SocialMedia media)
        {
            bool result = false;
            var sql = "INSERT INTO SocialMedia (PlatformName,PlatformIcon,AccountName,AccountURL,Status) values (@name,@icon,@acc,@url,@status);";
            int resultExecuteNonQuery = con.Execute(sql, new { name = media.PlatformName, icon = media.PlatformIcon, acc = media.AccountName, url = media.AccountURL, status = true });
            if (resultExecuteNonQuery == 1)
                result = true;
            return result;
        }
        public bool UpdateSocialMedia(SocialMedia media)
        {
            bool result = false;
            var sql = "Update SocialMedia set PlatformName=@name,PlatformIcon=@icon,AccountName=@acc,AccountURL=@url,Status=@status where ID=@id;";
            int resultExecuteNonQuery = con.Execute(sql, new { name = media.PlatformName, icon = media.PlatformIcon, acc = media.AccountName, url = media.AccountURL, status = true });
            if (resultExecuteNonQuery == 1)
                result = true;
            return result;
        }
        #endregion

        #region Staffs
        public List<Staffs> GetAllStaffs()
        {
            var sql = "SELECT * FROM Staffs where Status=1 ORDER BY StaffOrderNumber ASC;";
            return con.Query<Staffs>(sql).ToList();
        }
        public List<Staffs> GetLastStaffs(int top)
        {
            var sql = "SELECT TOP " + top + " * FROM Staffs where Status=1 ORDER BY StaffOrderNumber ASC;";
            return con.Query<Staffs>(sql).ToList();
        }
        public Staffs GetStaff(long ID)
        {
            var sql = "SELECT * FROM Staffs where ID=@ID and Status=1;";
            List<Staffs> model = con.Query<Staffs>(sql, new { ID = ID }).ToList();
            return model[0];
        }
        public string GetStaffName(long ID)
        {
            var sql = "SELECT StaffName FROM Staffs where ID=@ID and Status=1;";
            List<Staffs> model = con.Query<Staffs>(sql, new { ID = ID }).ToList();
            return model[0].StaffName;
        }
        public bool AddStaff(Staffs staff)
        {
            bool result = false;
            var sql = "INSERT INTO Staffs (StaffName,StaffRole,StaffImageURL,StaffOrderNumber,Status) values (@name,@role,@url,@ordernumber,@status);";
            int resultExecuteNonQuery = con.Execute(sql, new { name = staff.StaffName, role = staff.StaffRole, url = staff.StaffImageURL, ordernumber = staff.StaffOrderNumber, status = true });
            if (resultExecuteNonQuery == 1)
                result = true;
            return result;
        }
        public bool UpdateStaff(Staffs staff)
        {
            bool result = false;
            var sql = "Update Staffs set StaffName=@name,StaffRole=@role,StaffImageURL=@url,StaffOrderNumber=@ordernumber,Status=@status where ID=@id;";
            int resultExecuteNonQuery = con.Execute(sql, new { name = staff.StaffName, role = staff.StaffRole, url = staff.StaffImageURL, ordernumber = staff.StaffOrderNumber, status = true });
            if (resultExecuteNonQuery == 1)
                result = true;
            return result;
        }
        #endregion

        #region StaffSocialMedia
        public List<StaffSocialMedia> GetAllStaffSocialMedias()
        {
            var sql = "SELECT * FROM StaffSocialMedia where Status=1 ORDER BY ID DESC;";
            return con.Query<StaffSocialMedia>(sql).ToList();
        }
        public List<StaffSocialMedia> GetLastStaffSocialMedias(int top)
        {
            var sql = "SELECT TOP " + top + " * FROM StaffSocialMedia where Status=1 ORDER BY ID DESC;";
            return con.Query<StaffSocialMedia>(sql).ToList();
        }
        public StaffSocialMedia GetStaffSocialMedia(long ID)
        {
            var sql = "SELECT * FROM StaffSocialMedia where ID=@ID and Status=1;";
            List<StaffSocialMedia> model = con.Query<StaffSocialMedia>(sql, new { ID = ID }).ToList();
            return model[0];
        }
        public bool AddStaffSocialMedia(StaffSocialMedia stMedia)
        {
            bool result = false;
            var sql = "INSERT INTO StaffSocialMedia (StaffID,PlatformName,PlatformIcon,AccountName,AccountURL,Status) values (@staffid,@name,@icon,@acc,@url,@status);";
            int resultExecuteNonQuery = con.Execute(sql, new { staffid = stMedia.StaffID, name = stMedia.PlatformName, icon = stMedia.PlatformIcon, acc = stMedia.AccountName, url = stMedia.AccountURL, status = true });
            if (resultExecuteNonQuery == 1)
                result = true;
            return result;
        }
        public bool UpdateStaffSocialMedia(StaffSocialMedia stMedia)
        {
            bool result = false;
            var sql = "Update StaffSocialMedia set StaffID=@staffid,PlatformName=@name,PlatformIcon=@icon,AccountName=@acc,AccountURL=@url,Status=@status where ID=@id;";
            int resultExecuteNonQuery = con.Execute(sql, new { staffid = stMedia.StaffID, name = stMedia.PlatformName, icon = stMedia.PlatformIcon, acc = stMedia.AccountName, url = stMedia.AccountURL, status = true });
            if (resultExecuteNonQuery == 1)
                result = true;
            return result;
        }
        #endregion


        #endregion

    }
}
