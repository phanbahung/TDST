using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TDST.Models
{
    public class BaoCaoTongHop
    {        
      //  public int IdCTBC { get; set; }
        public int IdCTBC { get; set; }
        public string TenChiTieu { get; set; }
        public string TenNhomCH_TM { get; set; }        
        public int NhomCH { get; set; }
        public int NhomTM { get; set; }        
        public string TenDonVi { get; set; }
        public int MaCqThu { get; set; }
        public string SoThue { get; set; }
        public int STT_ChiTieu { get; set; }
        //public string STT_DonVi { get; set; }
    }
}