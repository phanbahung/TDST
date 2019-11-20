using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ModelViews
{
    public class DuToanModelView
    {
        public int IdChiTieu { get; set; }        
        public string TenChiTieu { get; set; }
        public int IdChiTietDuToan { get; set; }
        public string TenVietTat { get; set; }
        public int MaDonVi { get; set; }
        public int? MaCqThu { get; set; }
        public int? Quy { get; set; }      
        public int? IdDuToan { get; set; }
        public Decimal SoThue { get; set; }
        //public string SoTien { get; set; }
        public string SoThucHien { get; set; }
    }
}