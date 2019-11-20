using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TDST_CRUD.EF;
using TDST;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace TDST.Models
{
    public class DuToanModel
    {
        private TDSTDbContext db = null;

        public DuToanModel()
        {
            db = new TDSTDbContext();
        }


        public List<DuToan> Get_NamDuToan(int? namDuToan)
        {
            //myList.DistinctBy(x => x.id);
            // OR Otherwise, you can use a group:
            //myList.GroupBy(x => x.id).Select(g => g.First());       
                 
                     
            return (db.DuToans.ToList()).GroupBy(i => i.NamDuToan).Select(group => group.First()).ToList(); ;
        }

        public List<DuToan> Get_TenDuToan_TheoNam(int nam)
        {
            var list = db.DuToans.Where(dt => dt.NamDuToan == nam).ToList();
            return list;
        }

        public List<Custom_DuToan> Get_DuToan(string coQuanBanHanh, string maDonVi, int quy)
        {
            object[] sqlParams =
            {
                new SqlParameter ("@CoQuanBanHanh", coQuanBanHanh),
                new SqlParameter ("@MaDonVi", maDonVi),
                new SqlParameter ("@Quy", quy),

            };
            var list = db.Database.SqlQuery<Custom_DuToan>("spDuToan_Select @CoQuanBanHanh, @MaDonVi, @Quy", sqlParams).ToList();
            return list;
        }


        public string UpDate_DuToan(int maDonVi, int idChiTieu, int quy, Decimal soThue)
        {
            object[] sqlParams =
            {
                new SqlParameter ("@MaDonVi", maDonVi),
                new SqlParameter ("@IdChiTieu", idChiTieu ),
                new SqlParameter ("@SoThue", soThue ),
                new SqlParameter ("@Quy", quy),    

            };
            var list = db.Database.SqlQuery<String>("spDuToan_Update_DuToan @MaDonVi, @IdChiTieu, @Quy, @SoThue ", sqlParams).SingleOrDefault();
            return list;
        }


    }
}