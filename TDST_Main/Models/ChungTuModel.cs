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
    public class ChungTuModel
    {
        private TDSTDbContext context = null;

        public ChungTuModel()
        {
            context = new TDSTDbContext();
        }

       


        public List<GiaoDich> TraCuuChungTu(string id)
        {
            object[] sqlParams =
            {
                new SqlParameter ("@Id", id),
            };
            var list = context.Database.SqlQuery<GiaoDich>("spDsGiaoDich_Select @Id", sqlParams).ToList();
            return list;
        }

        public List<ThongKeChungTuClass> ThongKeChungTu(int thang)
        {
            object[] sqlParams =
            {
                new SqlParameter ("@Thang", thang),
            };
            var list = context.Database.SqlQuery<ThongKeChungTuClass>("spDsGiaoDich_ThongKeChungTuTheoNgay @Thang", sqlParams).ToList();
            return list;
        }


        public string  Insert_ChungTu(string khct_soct , string ngay_kbac, string ngay_ht, string ma_chuong 
                                        , string ma_tmuc , string so_tien, string tk_co_dtl, string tin, string ma_cqthu)        {    


            object[] sqlParams =
            {
                new SqlParameter ("@Action", "insert"),
                new SqlParameter ("@Khct_soct", khct_soct),                 
                new SqlParameter ("@Ngay_ht",ngay_ht),
                new SqlParameter ("@Ngay_kbac",ngay_kbac),
                new SqlParameter ("@Ma_chuong",ma_chuong),
                new SqlParameter ("@Ma_tmuc",ma_tmuc),
                new SqlParameter ("@So_tien",so_tien),
                new SqlParameter ("@Tk_co_dtl",tk_co_dtl),
                new SqlParameter ("@Tin",tin),
                new SqlParameter ("@Ma_cqthu",ma_cqthu),

            };
            var res = context.Database.SqlQuery<string>("spDsGiaoDich_CRUD @Action, @Khct_soct, @ngay_kbac, @ngay_ht, @ma_chuong , @ma_tmuc, @so_tien, @tk_co_dtl, @tin, @ma_cqthu", sqlParams).SingleOrDefault();
            return res;
        }      



    }
}