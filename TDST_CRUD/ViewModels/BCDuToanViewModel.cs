using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ViewModels
{
    public class BCDuToanViewModel
    {
        public int? IdChiTieu { get; set; }        
        public string TenChiTieu { get; set; }
        public long IdChiTietDuToan { get; set; }
        public string TenVietTat { get; set; }
        public string MaDonVi { get; set; }
        public int? STT { get; set; }        
        public int? Quy { get; set; }              
        public Decimal? SoThue { get; set; }        
        public Decimal? SoThucHien { get; set; }
        public Decimal? SoThucHien_KBac { get; set; }
        public Decimal? PhanTramThucHien { get; set; }
    }
}