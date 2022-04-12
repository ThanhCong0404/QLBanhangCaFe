using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLBanhang.Models;
using System.Collections;
namespace QLBanhang.Controllers
{
    public class MenuController : Controller
    {
        // GET: Menu
        public ActionResult Index()
        {
            using (qlbanhangEntities db = new qlbanhangEntities())
            {
                var loaisp = db.LoaiSPs.ToList();
                Hashtable tenloaisp = new Hashtable();
                foreach(var item in loaisp)
                {
                    tenloaisp.Add(item.MaLoaiSP, item.TenLoaiSP);
                }
                ViewBag.TenLoaiSP = tenloaisp;
                return PartialView("Index");
            }    
            
        }
    }
}