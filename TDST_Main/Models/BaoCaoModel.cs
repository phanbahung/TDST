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
    public class BaoCaoModel
    {

        private TDSTDbContext context = null;

        public BaoCaoModel()
        {
            context = new TDSTDbContext();
        }

        
        


        public List<BaoCaoTongHop> TongHopBaoCao(int idBaoCao, string maCqThu)
        {
            object[] sqlParams =
            {
                new SqlParameter ("@BaoCao", idBaoCao),
                new SqlParameter ("@MaCqThu", maCqThu),

            };
            var list = context.Database.SqlQuery<BaoCaoTongHop>("spBaoCao_TonghopBaoCao @BaoCao, @MaCqThu", sqlParams).ToList();
            return list;
        }

    }

    
}