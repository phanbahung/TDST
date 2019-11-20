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
    public class BoChiTieuModel
    {
        private TDSTDbContext context = null;

        public BoChiTieuModel()
        {
            context = new TDSTDbContext();
        }

        public List<Custom_DsChiTieu> Get_DsChiTieu(string idBoChiTieu)
        {
            object[] sqlParams =
            {
                new SqlParameter ("@IdBoChiTieu", idBoChiTieu)
                
            };
            var list = context.Database.SqlQuery<Custom_DsChiTieu>("spBoChiTieu_Select_NoiDung @IdBoChiTieu", sqlParams).ToList();
            return list;
        }

        public List<BoChiTieu> Get_BoChiTieu()
        {           
            //var list = context.Database.SqlQuery<BoChiTieu>("spBoChiTieu_Select").ToList();
            var list= db.BoChiTieu.ToList();
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
            var list = context.Database.SqlQuery<String>("spDuToan_Update_DuToan @MaDonVi, @IdChiTieu, @Quy, @SoThue ", sqlParams).SingleOrDefault();
            return list;
        }


    }
}