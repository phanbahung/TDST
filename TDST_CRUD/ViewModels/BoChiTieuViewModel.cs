using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ViewModels
{
    public class BoChiTieuModelView
    {      

        public int IdBoChiTieu { get; set; }        
        public string TenBoChiTieu { get; set; }
        public int? Nam { get; set; }    
        public DateTime? UpdateTime { get; set; }
        public DateTime? LastUpdate { get; set; }
        public DateTime? UserUpdate { get; set; }
     
    }
}