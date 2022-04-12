using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLBanhang.Models
{
    [Serializable]
    public class Cartltem
    {
        public int MaSP { get; set; }
        public string TenSP { get; set; }
        public Double DonGia { get; set; }
        public int SoLuong { get; set; }
        public double ThanhTien
        {
            get { return SoLuong * DonGia; }
        }
    }
}