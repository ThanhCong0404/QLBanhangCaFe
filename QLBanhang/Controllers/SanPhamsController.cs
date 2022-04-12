using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLBanhang.Models;
using System.IO;


namespace QLBanhang.Controllers
{
    public class SanPhamsController : Controller
    {
        private qlbanhangEntities db = new qlbanhangEntities();

        public qlbanhangEntities Db { get => db; set => db = value; }
        public object Save { get; private set; }

        // GET: SanPhams
        public ActionResult Index()
        {
            var sanPham = Db.SanPhams.ToList();
            return View(sanPham.ToList());
        }

        // GET: SanPhams/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = Db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // GET: SanPhams/Create
        public ActionResult Create()
        {
            ViewBag.MaLoaiSP = new SelectList(Db.LoaiSPs, "MaLoaiSP", "TenLoaiSP");
            return View();
        }

        // POST: SanPhams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSP,TenSP,Donvitinh,Dongia,MaLoaiSP,HinhSP")] SanPham sanPham ,HttpPostedFileBase HinhSP)
        {
            if (ModelState.IsValid)
            {
                if(HinhSP!=null && HinhSP.ContentLength >0)
                {
                    string filename = Path.GetFileName(HinhSP.FileName);
                    string path = Server.MapPath("~/Content/Images/" + filename);
                    sanPham.HinhSP =filename;
                    HinhSP.SaveAs(path);
                }    
                Db.SanPhams.Add(sanPham);
                Db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaLoaiSP = new SelectList(Db.LoaiSPs, "MaLoaiSP", "TenLoaiSP", sanPham.MaLoaiSP);
            return View(sanPham);
        }

        // GET: SanPhams/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = Db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaLoaiSP = new SelectList(Db.LoaiSPs, "MaLoaiSP", "TenLoaiSP", sanPham.MaLoaiSP);
            return View(sanPham);
        }

        // POST: SanPhams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSP,TenSP,Donvitinh,Dongia,MaLoaiSP,HinhSP")] SanPham sanPham,
            HttpPostedFileBase HinhUpload, string HinhSP)
        {
            if (ModelState.IsValid)
            {
                if (HinhUpload != null && HinhUpload.ContentLength > 0)
                {
                    string filename = Path.GetFileName(HinhUpload.FileName);
                    string path = Server.MapPath("~/Content/images/" + filename);
                    sanPham.HinhSP = filename;
                    HinhUpload.SaveAs(path);
                }
                else
                {
                    sanPham.HinhSP = HinhSP; //giu hinh cu
                }    
                Db.Entry(sanPham).State = EntityState.Modified;
                Db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaLoaiSP = new SelectList(Db.LoaiSPs, "MaLoaiSP", "TenLoaiSP", sanPham.MaLoaiSP);
            return View(sanPham);
        }

        // GET: SanPhams/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = Db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // POST: SanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SanPham sanPham = Db.SanPhams.FirstOrDefault(s=>s.MaSP == id);
            Db.SanPhams.Remove(sanPham);
            Db.SaveChanges();
            System.IO.File.Delete(Server.MapPath("~/Content/images/" + sanPham.HinhSP));
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
