using TDST_CRUD.EF;
//using Model.ViewModel;
//using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Web;
using System.Data.SqlClient;
using TDST_CRUD.ViewModels;

namespace TDST_CRUD.Dao
{
    public class BoChiTieuDao
    {
        TDSTDbContext db = null;
        public BoChiTieuDao()
        {
            db = new TDSTDbContext();
        }

        public List<ChiTieuViewModel> Get_DsChiTieu(int idBoChiTieu)
        {
            object[] sqlParams =
            {
                new SqlParameter ("@IdBoChiTieu", idBoChiTieu)

            };
            var list = db.Database.SqlQuery<ChiTieuViewModel>("spBoChiTieu_Select_NoiDung @IdBoChiTieu", sqlParams).ToList();
            


            return list;
        }

        public List<BoChiTieu> Ds_BoChiTieu()
        {
            //var list = context.Database.SqlQuery<BoChiTieu>("spBoChiTieu_Select").ToList();
            var list = db.BoChiTieus.ToList();
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
