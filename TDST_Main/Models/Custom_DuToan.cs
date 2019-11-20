using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TDST.Models
{
    public class Custom_DuToan
    {
        public int IdChiTieu { get; set; }        
        public string TenChiTieu { get; set; }
        public int IdChiTietDuToan { get; set; }
        public string TenVietTat { get; set; }
        public int MaDonVi { get; set; }
        public int? MaCqThu { get; set; }
        public int? Quy { get; set; }
        public Decimal? SoThue { get; set; }
        public int? IdDuToan { get; set; }

    }
}