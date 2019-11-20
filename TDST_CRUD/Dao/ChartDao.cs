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
using ViewModels;
using TDST_CRUD.ViewModels;
using System.Data;

namespace TDST_CRUD.Dao
{
    public class ChartDao
    {
        TDSTDbContext db = null;
        public ChartDao()
        {
            db = new TDSTDbContext();
        }

        public List<GiaoDich> TraCuuChungTu(string id)
        {
            return db.GiaoDichs.ToList();
        }

       


        public List<Chart_LuyKeViewModel> TongHopSoThucHien_LuyKe(int quy)
        {
            object[] sqlParams =
            {
                new SqlParameter ("@Quy", quy),
            };
            var list = db.Database.SqlQuery<Chart_LuyKeViewModel>("spBaoCao_TongHopSoThucHien_LuyKe @Quy", sqlParams).ToList();
            return list;        }


       

    }
}
