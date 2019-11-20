using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TDST.Models
{
    public class Custom_DsChiTieu
    {       
      
        public string TenChiTieu { get; set; }
        public int IdChiTieu { get; set; }        
        public int STT { get; set; }                
        public int IdBoChiTieu { get; set; }
        public string TenBoChiTieu { get; set; }
        public int IdNhomChuong { get; set; }
        public int IdNhomTieuMuc { get; set; }
        public string TenNhomChuong { get; set; }
        public string TenNhomTieuMuc { get; set; }      

    }
}