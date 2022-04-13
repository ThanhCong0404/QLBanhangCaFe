using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLBanhang.Models;
using System.Net;
using System.Net.Mail;
using MySql.Data.MySqlClient.Memcached;
using System.Web.Helpers;

namespace QLBanhang.Controllers
{
    public class GiohangController : Controller
    {
        private qlbanhangEntities db = new qlbanhangEntities();
        private readonly object hang;
        public static string alert = null;

        // GET: Giohang
        public ActionResult Index()
        {
            List<Cartltem> giohang = Session["giohang"] as List<Cartltem>;
            ViewBag.DanhSach = giohang;
            ViewBag.alert = alert;
            if(Session["user"] != null)
            {
                var toEmail = (Models.KhachHang)Session["user"];
                ViewBag.toEmail = toEmail.Email;
            }    
            
            return View();
        }
        // khai bao phương thức thêm sản phẩm vào dõ hang
        public RedirectToRouteResult AddToCart(int MaSP)
        {
            if (Session["giohang"] == null)
            {
                Session["giohang"] = new List<Cartltem>();
            }
            List<Cartltem> giohang = Session["giohang"] as List<Cartltem>;
            //kiem tra san pham co chua
            if (giohang.FirstOrDefault(m => m.MaSP == MaSP) == null)// chua có san pham
            {
                SanPham sp = db.SanPhams.Find(MaSP);
                Cartltem newItem = new Cartltem();
                newItem.MaSP = MaSP;
                newItem.TenSP = sp.TenSP;
                newItem.SoLuong = 1;
                newItem.DonGia = Convert.ToDouble(sp.Dongia);
                giohang.Add(newItem);
            }
            else//san pham da cop trong gio hàng
            {
                Cartltem cardltem = giohang.FirstOrDefault(m => m.MaSP == MaSP);
                cardltem.SoLuong++;
            }
            Session["giohang"] = giohang;
            return RedirectToAction("Index");
        }



        public RedirectToRouteResult Update(int MaSP, int txtSoLuong)
        {
            alert = null;

            List<Cartltem> giohang = Session["giohang"] as List<Cartltem>;
            Cartltem item = giohang.FirstOrDefault(m => m.MaSP == MaSP);

            if (item != null)
            {
                if((Int32)txtSoLuong > db.SanPhams.Where(x => x.MaSP == item.MaSP).FirstOrDefault().SoLuong)
                {
                    alert = "So Luong San Pham Con Lai Khong Du ";
                    return RedirectToAction("Index");
                }    

                item.SoLuong = txtSoLuong;
                Session["giohang"] = giohang;
            }
            return RedirectToAction("Index");
        }
        public RedirectToRouteResult DelCartItem(int MaSP)
        {
            List<Cartltem> giohang = Session["giohang"] as List<Cartltem>;
            Cartltem item = giohang.FirstOrDefault(m => m.MaSP == MaSP);
            if (item != null)
            {
                giohang.Remove(item);
                Session["giohang"] = giohang;
            }
            return RedirectToAction("Index");
        }
        public ActionResult Order (string Email)
        {
            alert = null;

            if(Session["user"] == null)
            {
               return Redirect("/Home/Login");
            }
            


            List<Cartltem> giohang = Session["giohang"] as List<Cartltem>;
            string sMsg = "<html><body><table border='1'><caption> Thông Tin đạt hàng</caption><tr><th>STT</th><th>Tên Hàng</th><th>Số Lượng</th><th> Đơn giá</th><th>Thanh Tiền</th></tr>";
            int i = 0;
            float tongtien = 0;
            foreach(Cartltem item in giohang)
            {
                i++;
                sMsg += "<tr>";
                sMsg += "<td>" + i.ToString() + "</td>";
                sMsg += "<td>" + item.TenSP + "</td>";
                sMsg += "<td>" + item.SoLuong.ToString() + "</td>";
                sMsg += "<td>" + item.DonGia.ToString() + "</td>";
                sMsg += "<td>" + string.Format("{0:#,###}",item.SoLuong*item.DonGia) + "</td>";
                sMsg += "<tr>";
                tongtien += item.SoLuong * (int)item.DonGia;
            }

            sMsg += "<tr><th_colspan='5'>Tõng cộng:"
               + string.Format("{0:#,###}", tongtien) + "</th></tr></table>";
            MailMessage mail = new MailMessage("anhpro12anh@gmail.com", Email, "thong tin đơn hàng", sMsg);
            SmtpClient client = new SmtpClient("smtp. gmail.com", 587);
            //xem li
            client.Credentials = new NetworkCredential("anhpro12anh@", "tuananhnguyen2001");
            client.EnableSsl = true;

            mail.IsBodyHtml = true;
            //client.Send(mail);

            WebMail.Send(Email, "Thông tin đơn đặt hàng", sMsg, null, null, null, true, null, null, null, null,null,null);


            HoaDon hoadon = new HoaDon();
            var kh = (QLBanhang.Models.KhachHang)Session["user"];
            hoadon.MaKH = kh.MaKH;
            hoadon.NgayGiaoHang = DateTime.Now;
            hoadon.NgayLapHD = DateTime.Now;
            hoadon.TongTien = tongtien;
            db.HoaDons.Add(hoadon);
            db.SaveChanges();
            CTHD cthd = new CTHD();
            foreach (var item in Session["giohang"] as List<Cartltem>)
            {

                cthd.MaHD = db.HoaDons.FirstOrDefault(m=> m.MaHD == hoadon.MaHD).MaHD;
                cthd.MaSP = item.MaSP;
                cthd.Soluong = int.Parse(item.SoLuong.ToString());
                cthd.DongiaBan = float.Parse( item.ThanhTien.ToString());
                db.SanPhams.Where(x => x.MaSP == cthd.MaSP).FirstOrDefault().SoLuongDaBan += cthd.Soluong;
                db.SanPhams.Where(x => x.MaSP == cthd.MaSP).FirstOrDefault().SoLuong -= (int)cthd.Soluong;


                Session["giohang"] = null;
                alert = "Dat Hang Thanh Cong ! Successfull";
            }
            db.CTHDs.Add(cthd);
            db.SaveChanges();

            return RedirectToAction("Index");

        }
    }
}