using TDST_CRUD.EF;
//using Model.ViewModel;
//using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;


namespace TDST_CRUD.Dao
{
    public class DanhMucDao
    {
        TDSTDbContext db = null;
        public DanhMucDao()
        {
            db = new TDSTDbContext();
        }

        public List<string> Get_ListMaCqThu()
        {
            return db.DonVis.Select(x=>x.MaCqThu).Distinct().ToList();
        }

        public List<DonVi> Get_DmDonVi()
        {           
            return db.DonVis.OrderBy(x=>x.STT).ToList();
        }

        #region Chương ,nhóm chương
        public List<Chuong> Get_DmChuong()
        {
            return db.Chuongs.ToList();
        }

        public List<NhomChuong> Get_DmNhomChuong()
        {
            //return db.NhomChuongs.ToList();
            return db.NhomChuongs.ToList();
        }

        public List<NhomCH_CH> Get_DmNhomChuong_ById(long id)
        {
            //return db.NhomChuongs.ToList();
            return db.NhomCH_CH.Where(x=>x.IdNhomChuong==id).ToList();
        }


        #endregion

        public List<TieuMuc> Get_DmTieuMuc()
        {
            return db.TieuMucs.ToList();
        }

        public List<NhomTieuMuc> Get_DmNhomTieuMuc()
        {
            return db.NhomTieuMucs.ToList();
        }
               

        public List<NhomTieuMuc> Get_NhomTieuMuc_ScanIsTrue()
        {
            return db.NhomTieuMucs.Where(x=>x.Scan==true).ToList();
        }









    }
}
