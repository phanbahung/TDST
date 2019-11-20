using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TDST_CRUD.ViewModels
{
    
    public class DonVi_QL_NNTViewModel
    {
        public int Id { get; set; }
        public string MST { get; set; }
        public string TenNNT { get; set; }
        public string MaDonVi { get; set; }
        public string TenDonVi { get; set; }
        public string TenVietTat1 { get; set; }
        public string TenVietTat2 { get; set; }
        public DateTime? TuNgay { get; set; }
        public DateTime? DenNgay { get; set; }
    }
}
