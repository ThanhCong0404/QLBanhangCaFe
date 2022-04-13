using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using QLBanhang.Models;

namespace QLBanhang.Controllers
{
    public class HoaDonsController : Controller
    {
        private qlbanhangEntities db = new qlbanhangEntities();

        // GET: HoaDons
        public ActionResult Index()
        {
            var hoaDon = db.HoaDons.Include(h => h.KhachHang).Include(h => h.Nhanvien);
            return View(hoaDon.ToList());
        }

        // GET: HoaDons/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoaDon hoaDon = db.HoaDons.Find(id);
            if (hoaDon == null)
            {
                return HttpNotFound();
            }
            return View(hoaDon);
        }

        // GET: HoaDons/Create
        public ActionResult Create()
        {
            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "TenKH");
            ViewBag.MaNV = new SelectList(db.Nhanviens, "MaNV", "HoNV");
            return View();
        }

        // POST: HoaDons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaHD,MaKH,MaNV,NgayLapHD,NgayGiaoHang")] HoaDon hoaDon)
        {
            if (ModelState.IsValid)
            {
                db.HoaDons.Add(hoaDon);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "TenKH", hoaDon.MaKH);
            ViewBag.MaNV = new SelectList(db.Nhanviens, "MaNV", "HoNV", hoaDon.MaNV);
            return View(hoaDon);
        }

        // GET: HoaDons/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoaDon hoaDon = db.HoaDons.FirstOrDefault(m=>m.MaHD == id);
            if (hoaDon == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "TenKH", hoaDon.MaKH);
            ViewBag.MaNV = new SelectList(db.Nhanviens, "MaNV", "HoNV", hoaDon.MaNV);
            return View(hoaDon);
        }

        // POST: HoaDons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaHD,MaKH,MaNV,NgayLapHD,NgayGiaoHang")] HoaDon hoaDon)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hoaDon).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "TenKH", hoaDon.MaKH);
            ViewBag.MaNV = new SelectList(db.Nhanviens, "MaNV", "HoNV", hoaDon.MaNV);
            return View(hoaDon);
        }

        // GET: HoaDons/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoaDon hoaDon = db.HoaDons.FirstOrDefault(m=> m.MaHD== id);
            if (hoaDon == null)
            {
                return HttpNotFound();
            }
            return View(hoaDon);
        }

        // POST: HoaDons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            HoaDon hoaDon = db.HoaDons.Find(id);
            db.HoaDons.Remove(hoaDon);
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

        public ActionResult XacNhanDonHang (int mahd)
        {
            var nv = (QLBanhang.Models.Nhanvien)Session["nhanvien"];
            HoaDon hd = db.HoaDons.FirstOrDefault(s => s.MaHD == mahd);
            
            if(hd.MaNV==null)
            {
                

                var emailKhachHang = db.KhachHangs.Where(k => k.MaKH == hd.MaKH).FirstOrDefault().Email;
                string sMg = "Đơn hàng có mã đơn " + hd.MaHD.ToString() + " đã được xác nhận . Chúng tôi sẽ giao đến bạn sớm nhất có thể ! ";
                WebMail.Send(emailKhachHang, "Thông tin đơn đặt hàng", sMg , null, null, null, true, null, null, null, null, null, null);
                hd.TinhTrangDonHang = "Đã xác nhận!";
                hd.MaNV = nv.MaNV;
                db.SaveChanges();
            }    
            
            return RedirectToAction("Index");
        }

        public ActionResult TuChoiDonHang(int mahd)
        {
            var nv = (QLBanhang.Models.Nhanvien)Session["nhanvien"];
            HoaDon hd = db.HoaDons.FirstOrDefault(s => s.MaHD == mahd);

                var emailKhachHang = db.KhachHangs.Where(k => k.MaKH == hd.MaKH).FirstOrDefault().Email;
                string sMg = "Đơn hàng có mã đơn " + hd.MaHD.ToString() + " Chưa được xác nhận . Chờ xử lý ! ";
                WebMail.Send(emailKhachHang, "Thông tin đơn đặt hàng", sMg, null, null, null, true, null, null, null, null, null, null);
            hd.TinhTrangDonHang = "Chưa Được Xác Nhận";
                db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
