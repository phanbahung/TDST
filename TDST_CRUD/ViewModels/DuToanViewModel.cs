using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TDST_CRUD.ViewModels
{
    public  class DuToanViewModel
    {               
        public int IdDuToan { get; set; }       
        public string TenDuToan { get; set; }
        public int IdBoChiTieu { get; set; }
        public string TenBoChiTieu { get; set; }
        public DateTime? UpdateTime { get; set; }
        public DateTime? LastUpdate { get; set; }       
        public string UserUpdate { get; set; }
        public int? NamDuToan { get; set; }
        public bool? TrangThai { get; set; }
    }
}