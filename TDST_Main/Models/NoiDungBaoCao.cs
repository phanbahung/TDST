using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TDST.Models
{
    public class NoiDungBaoCao
    {        
        public int IdCTBC { get; set; }        
        public string TenChiTieu { get; set; }
        //public string TenNhomCH_TM { get; set; }
        public string CH_TM { get; set; }
        public int STT { get; set; }
        public int CTBCCha { get; set; }
        public int IdBC { get; set; }
        public bool DisplayOnReport { get; set; }
        public int? Cap { get; set; }
        public int NhomCH { get; set; }
        public int NhomTM { get; set; }
        public string TenNhomChuong { get; set; }
        public string TenNhomTieuMuc { get; set; }
        //public string TenDonVi { get; set; }
        //public string MaCqThu { get; set; }
        //public string SoThue { get; set; }
        //public string STT_ChiTieu { get; set; }
        //public string STT_DonVi { get; set; }
    }
}