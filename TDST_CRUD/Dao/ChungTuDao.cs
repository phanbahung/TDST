using TDST_CRUD.EF;
//using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using TDST_CRUD.ViewModels;
using System.Data;

namespace TDST_CRUD.Dao
{
    public class ChungTuDao
    {
        TDSTDbContext db = null;
        public ChungTuDao()
        {
            db = new TDSTDbContext();
        }

        public List<GiaoDich> TraCuuChungTu(string id)
        {
            return db.GiaoDichs.ToList();
        }

        //public List<GiaoDich> DanhSach_FileChungTu()
        //{
        //    return db.GiaoDichs.ToList().DistinctBy(y=>y.Fi );
        //}


        public List<FileChungTuViewModel> ThongKeFileChungTu(string fileChungTu)
        {          
            object[] sqlParams =
            {
                new SqlParameter ("@FileChungTu", fileChungTu),
            };
            var list = db.Database.SqlQuery<FileChungTuViewModel>("spChungTu_ThongKeFile @FileChungTu", sqlParams).ToList();
            return list;
        }

        


        public List<ThongKeChungTuViewModel> ThongKeChungTu(int thang)
        {
            object[] sqlParams =
            {
                new SqlParameter ("@Thang", thang),
            };
            var list = db.Database.SqlQuery<ThongKeChungTuViewModel>("spDsGiaoDich_ThongKeChungTuTheoNgay @Thang", sqlParams).ToList();
            return list;
        }
         
        public int Count_ChungTu (string khct_soct, string tin, string ma_chuong, string ma_tmuc)
        {
            return db.GiaoDichs.Where(x => x.Khct_soct == khct_soct && x.Tin == tin&&x.Ma_chuong==ma_chuong&&x.Ma_tmuc==ma_tmuc).Count();

        }

        public long Insert_ChungTu(string khct_soct, string ten_nnthue, string ngay_kbac, string ngay_kb, string ngay_ht, string ky_thue, string ma_chuong
                                       , string ma_tmuc, string so_tien, string tk_co_dtl, string tin, string ma_cqthu
                                        ,long nhomChuong, long nhomTieuMuc, string maDonVi, string fileName)
        {

            long returnId = 0;

           if (Count_ChungTu( khct_soct,  tin, ma_chuong,  ma_tmuc)==0)        
               
            { 
                GiaoDich gd = new GiaoDich();
           
                gd.Khct_soct = khct_soct;
                gd.Ten_nnthue = ten_nnthue;
                if (CheckDate(ngay_ht))
                    gd.Ngay_ht = DateTime.ParseExact(ngay_ht, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);


                //string iString = "2005-05-05 22:12 PM";
                //DateTime oDate = DateTime.ParseExact(iString, "yyyy-MM-dd HH:mm tt", System.Globalization.CultureInfo.InvariantCulture);
                //MessageBox.Show(oDate.ToString());
                if (CheckDate(ngay_kbac))
                    gd.Ngay_kbac = DateTime.ParseExact(ngay_kbac, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                if (CheckDate(ngay_kb))
                    gd.Ngay_kb = DateTime.ParseExact(ngay_kb, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                gd.Tin = tin;           
                gd.Ma_chuong = ma_chuong;
                gd.Ma_tmuc = ma_tmuc;
                gd.So_tien = Decimal.Parse(so_tien);
                gd.Tk_co_dtl = tk_co_dtl;            
                gd.Ma_cqthu = ma_cqthu;

                // các trường không có trong file gốc, thêm vào để quản lý
                gd.NhomChuong = nhomChuong;
                gd.NhomTieuMuc = nhomTieuMuc;
                gd.MaDonViQLKhoanThu = maDonVi;
                gd.FileChungTu = fileName;
                gd.NgayCapNhat = DateTime.Now;  

                db.GiaoDichs.Add(gd);
                    db.SaveChanges();

                returnId = gd.Id;
                gd = null;
            }

            return returnId;
        }


        protected bool CheckDate(String date)
        {
            try
            {
                DateTime dt = DateTime.Parse(date);
                return true;
            }
            catch
            {
                return false;
            }
        }



    }
}
