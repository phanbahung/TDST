using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TDST_CRUD.ViewModels
{
    public class ChiTieuViewModel
    {       
       public int IdChiTieu { get; set; } 
        public string TenChiTieu { get; set; }              
        public int STT { get; set; }
        public int? IdNhomChuong { get; set; }
        public int? IdNhomTieuMuc { get; set; }
        public string TenBoChiTieu { get; set; }
        public int Nam { get; set; }
        public int IdBoChiTieu { get; set; }  
        public string TenNhomChuong { get; set; }
        public string TenNhomTieuMuc { get; set; }

      //  sct.IdChiTieu
      //,dsct.TenChiTieu
      //,dsct.STT           
      //,dsct.IdNhomChuong
      //,dsct.IdNhomTieuMuc
      //,bct.TenBoChiTieu
      //,bct.Nam
      //,bct.IdBoChiTieu
      //, nhomCH.TenNhomChuong
      //, nhomTM.TenNhomTieuMuc

    }
}