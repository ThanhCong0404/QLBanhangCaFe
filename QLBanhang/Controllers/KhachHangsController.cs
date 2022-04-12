using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using QLBanhang.Models;

namespace QLBanhang.Controllers
{
    public class KhachHangsController : Controller
    {
        private qlbanhangEntities db = new qlbanhangEntities();

        // GET: KhachHangs
        public ActionResult Index()
        {
            return View(db.KhachHangs.ToList());
        }

        // GET: KhachHangs/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang khachHang = db.KhachHangs.Find(id);
            if (khachHang == null)
            {
                return HttpNotFound();
            }
            return View(khachHang);
        }

        // GET: KhachHangs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: KhachHangs/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaKH,TenKH,DiaChi,DienThoai,Fax,Email")] KhachHang khachHang)
        {
            if (ModelState.IsValid)
            {
                db.KhachHangs.Add(khachHang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(khachHang);
        }

        // GET: KhachHangs/Edit/5
        
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang khachHang = db.KhachHangs.Find(id);
            if (khachHang == null)
            {
                return HttpNotFound();
            }

            return View(khachHang);
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(KhachHang kh)
        {

            var khachHangDb = db.KhachHangs.FirstOrDefault(k => k.MaKH == kh.MaKH);

            if (ModelState.IsValid)
            {
                khachHangDb.TenKH = kh.TenKH;
                khachHangDb.Email = kh.Email;
                khachHangDb.DiaChi = kh.DiaChi;
                khachHangDb.DienThoai = kh.DienThoai;
                khachHangDb.Fax = kh.Fax;               

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kh);
        }



        public ActionResult EditfromKhachHang(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang khachHang = db.KhachHangs.Find(id);
            if (khachHang == null)
            {
                return HttpNotFound();
            }
            return View(khachHang);
        }
        [HttpPost]
        public ActionResult EditfromKhachHang(FormCollection collection,KhachHang kh)
        {
            var khachHangM = db.KhachHangs.FirstOrDefault(k => k.MaKH == kh.MaKH);
            var Email = collection["Email"];
            var MatKhau = collection["MatKhau"];
            var TenKH = collection["TenKH"];
            var Fax = collection["Fax"];
            var DiaChi = collection["DiaChi"];
            var DienThoai = collection["DienThoai"];
            var MatKhauCu = GetMD5(collection["MatKhauCu"]);
            var MatKhauMoi = collection["MatKhauMoi"];
            var MatKhauXacNhan = collection["MatKhauXacNhan"];



            if (MatKhauMoi != MatKhauXacNhan)
            {
                ViewBag.error = "Nhập sai mật khẩu xác nhận";
                return this.EditfromKhachHang(kh.MaKH);
            }
            if(MatKhauCu != khachHangM.MatKhau)
            {
                ViewBag.error = "Nhập sai mật khẩu cũ!";
                return this.EditfromKhachHang(kh.MaKH);
            }    
            else
                    {
                        
                        

                        db.Configuration.ValidateOnSaveEnabled = false;
                        db.SaveChanges();

                        ViewBag.message = "Success!!!!!!";
                        

                    }
                    return this.EditfromKhachHang(kh.MaKH);
                    
        
        }

        // GET: KhachHangs/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang khachHang = db.KhachHangs.Find(id);
            if (khachHang == null)
            {
                return HttpNotFound();
            }
            return View(khachHang);
        }

        // POST: KhachHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KhachHang khachHang = db.KhachHangs.Find(id);
            db.KhachHangs.Remove(khachHang);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

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
