using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLBanhang.Models;

namespace QLBanhang.Controllers
{
    public class CTHDsController : Controller
    {
        private qlbanhangEntities db = new qlbanhangEntities();

        // GET: CTHDs
        public ActionResult Index(int MaHD)
        {
            List<KhachHang> khachHangs = db.KhachHangs.ToList();
            List<HoaDon> hoaDons = db.HoaDons.ToList();
            List<Nhanvien> nhanviens = db.Nhanviens.ToList();
            List<SanPham> sanPhams = db.SanPhams.ToList();
            List<CTHD> cTHDs = db.CTHDs.ToList();
            var main = from h in hoaDons
                       join k in khachHangs on h.MaKH equals k.MaKH
                       where (h.MaHD == MaHD)
                       select new ViewModel
                       {
                           HoaDon = h,
                           KhachHang = k,
                       };
            var sub = from c in cTHDs
                      join s in sanPhams on c.MaSP equals s.MaSP
                      where (c.MaHD == MaHD)
                      select new ViewModel
                      {
                          CTHD = c,
                          SanPham = s,
                          Thanhtien = Convert.ToDouble(c.DongiaBan * c.Soluong)
                      };
            double amount = 0;
                       
            sub.ToList().ForEach(e => amount += e.Thanhtien );
            var tempAmount = new ViewModel
                {
                    Tongtien = amount
                };
        // truyen sang view 
        ViewBag.Main = main;
        ViewBag.Sub = sub;
            ViewBag.Tongtien = tempAmount;
            return View();
        }

        // GET: CTHDs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CTHD cTHD = db.CTHDs.Find(id);
            if (cTHD == null)
            {
                return HttpNotFound();
            }
            return View(cTHD);
        }

        // GET: CTHDs/Create
        public ActionResult Create()
        {
            ViewBag.MaHD = new SelectList(db.HoaDons, "MaHD", "MaKH");
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP");
            return View();
        }

        // POST: CTHDs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaHD,MaSP,Soluong,DongiaBan,Giamgia")] CTHD cTHD)
        {
            if (ModelState.IsValid)
            {
                db.CTHDs.Add(cTHD);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaHD = new SelectList(db.HoaDons, "MaHD", "MaKH", cTHD.MaHD);
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", cTHD.MaSP);
            return View(cTHD);
        }

        // GET: CTHDs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CTHD cTHD = db.CTHDs.Find(id);
            if (cTHD == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaHD = new SelectList(db.HoaDons, "MaHD", "MaKH", cTHD.MaHD);
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", cTHD.MaSP);
            return View(cTHD);
        }

        // POST: CTHDs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaHD,MaSP,Soluong,DongiaBan,Giamgia")] CTHD cTHD)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cTHD).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaHD = new SelectList(db.HoaDons, "MaHD", "MaKH", cTHD.MaHD);
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", cTHD.MaSP);
            return View(cTHD);
        }

        // GET: CTHDs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CTHD cTHD = db.CTHDs.Find(id);
            if (cTHD == null)
            {
                return HttpNotFound();
            }
            return View(cTHD);
        }

        // POST: CTHDs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            CTHD cTHD = db.CTHDs.Find(id);
            db.CTHDs.Remove(cTHD);
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
    }
}
