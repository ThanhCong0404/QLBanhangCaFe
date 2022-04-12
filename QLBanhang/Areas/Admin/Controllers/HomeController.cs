using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLBanhang.Models;

namespace QLBanhang.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        // GET: Admin/Home
        qlbanhangEntities db = new qlbanhangEntities();

        public ActionResult Index()
        {
            //validate
            var nhanvien = (QLBanhang.Models.Nhanvien)Session["nhanvien"];
            if(nhanvien == null || nhanvien.isAdmin == null || nhanvien.isAdmin == false)
            {
                return Redirect("/Home/");
            }    

            AdminModel model = new AdminModel();
            foreach(var item in db.HoaDons.Where(h => h.NgayLapHD <= DateTime.Now && h.NgayLapHD.Month >= DateTime.Now.Month && h.NgayLapHD.Year >= DateTime.Now.Year))
            {
                model.doanhthuthang += (double)item.TongTien;
            }    

            foreach(var item in db.HoaDons.ToList())
            {
                model.doanhthutong += (double)item.TongTien;
            }    

            return View(model);
        }
    }
}