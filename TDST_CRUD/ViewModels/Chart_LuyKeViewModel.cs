using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TDST_CRUD.ViewModels
{
    public  class Chart_LuyKeViewModel
    {               
        public string MaDonVi { get; set; }       
        public string TenDonVi { get; set; }        
        public string TenVietTat1 { get; set; }
        public string TenVietTat2 { get; set; }
        public int STT { get; set; }
        public decimal SoThucHien_LuyKe { get; set; }
    }
}