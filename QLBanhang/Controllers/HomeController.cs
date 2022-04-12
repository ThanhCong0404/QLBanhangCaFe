using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using QLBanhang.Models;
using PagedList;
using System.Security.Cryptography;
using System.Text;

namespace QLBanhang.Controllers
{
    public class HomeController : Controller
    {
        qlbanhangEntities db = new qlbanhangEntities();
        public ActionResult Index(string currentFilter,int?page ,int maloaisp = 0, string SearchString = "")
        {
            Session["Category"] = db.LoaiSPs.ToList();
            if (SearchString != "")
            {
                int pageSize = 8;
                int pageNumber = (page ?? 1);
                page = 1;
                var SanPham = db.SanPhams.Where(x => x.TenSP.ToUpper().Contains(SearchString.ToUpper())).ToList();
                return View(SanPham.ToPagedList(pageNumber, pageSize));
            }
            else
                SearchString = currentFilter;
                ViewBag.CurrentFilter = currentFilter;
            if (maloaisp == 0)
            {
                int pageSize = 8;
                int pageNumber = (page ?? 1);
                var sanPhams = db.SanPhams.ToList();
                //var sanPhams = db.SanPham.Include(s => s.LoaiSP).Where(x => x.MaLoaiSP == maloaisp).OrderBy(x =>x.TenSP);
                //phai oder tr skipp
                return View(sanPhams.ToPagedList(pageNumber,pageSize));
                //return View(sanPhams);
            }
            else
            {
                int pageSize = 8;
                int pageNumber = (page ?? 1);
                var sanPhams = db.SanPhams.Where(x => x.MaLoaiSP == maloaisp).ToList();

                return View(sanPhams.ToPagedList(pageNumber, pageSize));
            }    
        }

        //private ActionResult View(object p)
        //{
        //    throw new NotImplementedException();
        //}

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
     
            return View();
        }

        public ActionResult test()
        {
            return View();
        }


        //GET: Register

        public ActionResult Register()
        {
            return View();
        }

        //POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(FormCollection collection, KhachHang kh)
        {
            if (ModelState.IsValid)
            {

                var Email = collection["Email"];
                var MatKhau = collection["MatKhau"];
                var TenKH = collection["TenKH"];
                var Fax = collection["Fax"];
                var DiaChi = collection["DiaChi"];
                var DienThoai = collection["DienThoai"];
                var MatKhauXacNhan = collection["MatKhauXacNhan"];

                var check = db.KhachHangs.FirstOrDefault(s => s.Email == Email);
                if (check == null)
                {
                    if( MatKhauXacNhan != MatKhau)
                    {
                        ViewBag.error = "Nhập sai mật khẩu xác nhận!";
                        return this.Register();
                    }

                    else
                    {
                        kh.Email = Email;
                        kh.MatKhau = GetMD5(MatKhau);
                        kh.TenKH = TenKH;
                        kh.Fax = Fax;
                        kh.DiaChi = DiaChi;
                        kh.DienThoai = DienThoai;

                        db.Configuration.ValidateOnSaveEnabled = false;
                        db.KhachHangs.Add(kh);
                        db.SaveChanges();

                        ViewBag.error = "Success!!!!!!";
                        return this.Register();

                    }
                   
                    
                }
                else
                {
                    ViewBag.error = "Email already exists";
                    return this.Register();
                }


            }

            return this.Register();


        }

        public ActionResult Login()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(FormCollection collection, string email, string password)
        {
            email = collection["Email"];
            password = collection["MatKhau"];
            if (ModelState.IsValid)
            {


                var f_password = GetMD5(password);
                var data = db.KhachHangs.Where(s => s.Email.Equals(email) && s.MatKhau.Equals(f_password)).FirstOrDefault();
                var nhanvien = db.Nhanviens.Where(s => s.Email.Equals(email) && s.MatKhau.Equals(password)).FirstOrDefault();


                if (data != null)
                {
                    //add session
                    Session["FullName"] = data.TenKH;
                    Session["Email"] = data.Email;
                    Session["idUser"] = data.MaKH;
                    Session["user"] = data;
                    return RedirectToAction("Index");
                }

                else if(nhanvien != null)
                {
                    
                    Session["nhanvien"] = nhanvien;
                    return RedirectToAction("Index");
                }


               
            }
            ViewBag.error = "Login failed";
            return this.Login();
        }


        //Logout
        public ActionResult Logout()
        {
            Session["user"] = null;
            Session["nhanvien"] = null;
            return RedirectToAction("Login");
        }



        //create a string MD5
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }

    }
}