using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace QLBanhang.Models
{
    public class ViewModel
    {
        public KhachHang KhachHang { get; set; }
        public CTHD CTHD { get; set; }
        public HoaDon HoaDon { get; set; }
        public LoaiSP LoaiSP { get; set; }
        public Nhanvien nhanvien { get; set; }
        public SanPham SanPham { get; set; }
        [DisplayFormat(DataFormatString ="{0:0.##0}")]
        public double Thanhtien { get; set; }
        public double Tongtien { get; set; }
    }
}