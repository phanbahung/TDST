    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    namespace TDST_CRUD.ViewModels
{
        public class FileChungTuViewModel
        {
            //[DatabaseGenerated(DatabaseGeneratedOption.None)]
            //public int Id { get; set; }

           
            public int Nam_KhoBac { get; set; }
            public string Ma_cqthu { get; set; }
            public string TenDonVi { get; set; }
            public int SoLuongChungTu { get; set; }
            public Decimal TongTien { get; set; }
            public string FileName { get; set; }
            public string ma_chuong { get; set; }
            public string ma_tmuc { get; set; }
            public string Ten { get; set; }
    }
    }



