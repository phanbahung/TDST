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
using ViewModels;

namespace TDST_CRUD.Dao
{
    public class DuToanDao
    {
        TDSTDbContext db = null;
        public DuToanDao()
        {
            db = new TDSTDbContext();
        }

        public DuToan Details(int idDuToan)
        {
            return db.DuToans.Find(idDuToan);
        }

        public int ChotSoThu(int idDuToan, int quy, string nam)
        {       
                object[] sqlParams_DT =
          {
                new SqlParameter ("@IdDuToan", idDuToan),
                new SqlParameter ("@Nam", quy),
                new SqlParameter ("@Quy",nam),
                new SqlParameter ("@MaDonVi_Tao", "")
           };
            
            int idDuToan_new = db.Database.SqlQuery<int>("sp_HoTro_ChotSo @IdDuToan, @Nam, @Quy, MaDonVi_Tao", sqlParams_DT).SingleOrDefault();       
            return idDuToan_new;
            
        }

        public int Insert(DuToan entity)
        {

            // @TenDuToan nvarchar(200),
            //@NamDuToan int,
            //@IdboChiTieu int,
            //@UserUpdate char(30)
            object[] sqlParams_DT =
          {
                new SqlParameter ("@TenDuToan", entity.TenDuToan),
                new SqlParameter ("@NamDuToan", entity.NamDuToan),
                new SqlParameter ("@IdBoChiTieu", entity.IdBoChiTieu),
                new SqlParameter ("@UserUpdate", "pbhung"),
           };
            int idDuToan_new = db.Database.SqlQuery<int>("spDuToan_Create_DuToan @TenDuToan, @NamDuToan, @IdBoChiTieu, UserUpdate", sqlParams_DT).SingleOrDefault();

            return idDuToan_new;
            
            //db.DuToans.Add(entity);
            //db.SaveChanges();
            //return entity.IdDuToan;
        }

        public List<DuToan> Get_NamDuToan()
        {
            //myList.DistinctBy(x => x.id);
            // OR Otherwise, you can use a group:
            //myList.GroupBy(x => x.id).Select(g => g.First());     

            return (db.DuToans.ToList()).GroupBy(i => i.NamDuToan).Select(group => group.First()).ToList(); ;
        }

        public List<DuToan> Ds_DuToan()
        {
            return db.DuToans.ToList();           
        }

        public List<DuToanViewModel> Get_DS_DuToan(int nam, int idBoChiTieu)
        {
            //var list = db.DuToans.Where(dt => dt.NamDuToan == nam && dt.IdBoChiTieu== idBoChiTieu).ToList();
            IEnumerable <DuToanViewModel> lstDuToan  = from dt in db.DuToans
                                     join bct in db.BoChiTieus on dt.IdBoChiTieu equals bct.IdBoChiTieu
                                     //where (dt.NamDuToan == nam) && (dt.IdBoChiTieu == idBoChiTieu)
                                     select new DuToanViewModel
                                     {
                                        IdBoChiTieu = dt.IdBoChiTieu,
                                        TenBoChiTieu= bct.TenBoChiTieu,
                                        IdDuToan = dt.IdDuToan,
                                        NamDuToan = dt.NamDuToan,
                                        TrangThai= dt.TrangThai,
                                        TenDuToan= dt.TenDuToan,
                                        LastUpdate= dt.LastUpdate,
                                        UpdateTime= dt.UpdateTime,
                                        UserUpdate= dt.UserUpdate
                                     };
            return lstDuToan.ToList();
        }

        public int Get_IdBoChiTieu_By_IdDuToan(int idDuToan)
        {
            return db.DuToans.Where(x => x.IdDuToan == idDuToan).Select(x => x.IdBoChiTieu).SingleOrDefault();
        }

        public List<BCDuToanViewModel> Get_ChotSo_CuaCqThue_KhoBac(int idDuToan)
        {
            IEnumerable<BCDuToanViewModel> lstDuToan = from dtct in db.DuToanChiTiets
                                                       join bctct in db.BoChiTieuChiTiets on dtct.IdChiTieu equals bctct.IdChiTieu
                                                       join dv in db.DonVis on dtct.MaDonVi equals dv.MaDonVi
                                                       where (dtct.IdDuToan == idDuToan) & (dv.Cap==2)
                                                       select new BCDuToanViewModel
                                                       {
                                                           IdChiTieu = dtct.IdChiTieu,
                                                           TenChiTieu = bctct.TenChiTieu + " (" + bctct.IdNhomChuong + " - " + bctct.IdNhomTieuMuc + ")",
                                                           TenVietTat = dtct.TenVietTat,
                                                           MaDonVi = dtct.MaDonVi,
                                                           STT = dv.STT,
                                                           Quy = dtct.Quy,
                                                           SoThucHien_KBac = dtct.SoThucHien_KBac,
                                                           //PhanTramThucHien = dtct.PhanTramThucHien ,
                                                       };
            return lstDuToan.ToList();
        }

        public List<BCDuToanViewModel> Get_ThucHien_Sau_ChotSo(int idDuToan)
        {
            IEnumerable<BCDuToanViewModel> lstDuToan = from dtct in db.DuToanChiTiets
                                                       join bctct in db.BoChiTieuChiTiets on dtct.IdChiTieu equals bctct.IdChiTieu
                                                       join dv in db.DonVis on dtct.MaDonVi equals dv.MaDonVi
                                                       where (dtct.IdDuToan == idDuToan)
                                                       select new BCDuToanViewModel
                                                       {
                                                            IdChiTieu = dtct.IdChiTieu ,
                                                            TenChiTieu = bctct.TenChiTieu + " ("+ bctct.IdNhomChuong+" - "+bctct.IdNhomTieuMuc +")",                                                            
                                                            TenVietTat = dtct.TenVietTat ,
                                                            MaDonVi = dtct.MaDonVi ,
                                                            STT = dv.STT,
                                                            Quy = dtct.Quy ,                                                                                                                       
                                                            SoThucHien = dtct.SoThucHien ,
                                                            //PhanTramThucHien = dtct.PhanTramThucHien ,
                                                     };
            return lstDuToan.ToList();
        }


        public List<BCDuToanViewModel> Get_DuToan(int idDuToan, string maDonVi, int quy)
        {
            object[] sqlParams =
            {
                new SqlParameter ("@IdDuToan", idDuToan),
                new SqlParameter ("@MaDonVi", maDonVi),
                new SqlParameter ("@Quy", quy),

            };
            var list = db.Database.SqlQuery<BCDuToanViewModel>("spDuToan_Select @IdDuToan, @MaDonVi, @Quy", sqlParams).ToList();
            return list;
        }

        public List<BCDuToanViewModel> Get_SoThucHien(int idDuToan, string maDonVi, int quy)
        {    
           object[] sqlParams =
            {
                new SqlParameter ("@IdDuToan", idDuToan),
                new SqlParameter ("@MaDonVi", maDonVi),
                new SqlParameter ("@Quy", quy),
            };
            // Thứ tự là rất quan trọng
            var list = db.Database.SqlQuery<BCDuToanViewModel>("[spBaoCao_TongHopSoThucHien_RealTime] @IdDuToan, @MaDonVi, @Quy", sqlParams).ToList();
            return list;
        }

        public List<DuToanViewModel> Get_SoThucHien_KhoaSo(int idDuToan, string maDonVi, int quy)
        {
              IEnumerable <DuToanViewModel> lstDuToan  = from dt in db.DuToans
                                     join bct in db.BoChiTieus on dt.IdBoChiTieu equals bct.IdBoChiTieu
                                     //where (dt.NamDuToan == nam) && (dt.IdBoChiTieu == idBoChiTieu)
                                     select new DuToanViewModel
                                     {
                                        IdBoChiTieu = dt.IdBoChiTieu,
                                        TenBoChiTieu= bct.TenBoChiTieu,
                                        IdDuToan = dt.IdDuToan,
                                        NamDuToan = dt.NamDuToan,
                                        TrangThai= dt.TrangThai,
                                        TenDuToan= dt.TenDuToan,
                                        LastUpdate= dt.LastUpdate,
                                        UpdateTime= dt.UpdateTime,
                                        UserUpdate= dt.UserUpdate
                                     };
            return lstDuToan.ToList();
            
        }



        public string UpDate_DuToan(int idDuToan, int maDonVi, int idChiTieu, int quy, Decimal soThue)
        {
            object[] sqlParams =
            {
                new SqlParameter ("@IdDuToan", idDuToan),
                new SqlParameter ("@MaDonVi", maDonVi),
                new SqlParameter ("@IdChiTieu", idChiTieu ),
                new SqlParameter ("@SoThue", soThue ),
                new SqlParameter ("@Quy", quy),

            };
            var list = db.Database.SqlQuery<String>("spDuToan_Update_DuToan @IdDuToan, @MaDonVi, @IdChiTieu, @Quy, @SoThue ", sqlParams).SingleOrDefault();
            return list;
        }








    }
}
