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
    public class NguoiNopThueDao
    {
        TDSTDbContext db = null;
        public NguoiNopThueDao()
        {
            db = new TDSTDbContext();
        }

        public string Get_MaDonViQuanLy_KhoanThu(string mst, string tieuMuc, string ngay_kbac, string maCqThu)
        {
            DateTime ngayKhoBac = DateTime.ParseExact(ngay_kbac, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            //List<QuanLyTM> dvQuanLyTM = db.QuanLyTMs.Where(x =>x.TieuMuc==tieuMuc & x.TuNgay< ngayKhoBac).OrderByDescending(x=>x.TuNgay).ToList();

            List<QuanLyTM> dvQuanLyTM = (from qltm in db.QuanLyTMs
                                        join dv in db.DonVis on qltm.MaDonVi equals dv.MaDonVi
                                        where (dv.MaCqThu == maCqThu) & (qltm.TieuMuc==tieuMuc)
                                        & (qltm.TuNgay< ngayKhoBac) & (qltm.DenNgay>ngayKhoBac)
                                        select qltm).ToList();


            List<QuanLyNNT> dvQuanLyNNT= db.QuanLyNNTs.Where(x => x.MST == mst && x.Status==0).ToList();
            string kq = "";

            if (dvQuanLyTM != null && dvQuanLyTM.Count != 0)
                kq = dvQuanLyTM.Take(1).SingleOrDefault().MaDonVi;
            else
            if (dvQuanLyNNT != null && dvQuanLyNNT.Count!=0)
                kq = dvQuanLyNNT.Take(1).SingleOrDefault().MaDonVi;

            return kq;
                      
        }

        public List<NguoiNopThueViewModel> Ds_NguoiNopThue()
        {           
           // var list = db.NguoiNopThues.ToList();            
            IEnumerable<NguoiNopThueViewModel> lst = from nnt in db.NguoiNopThues
                                                     //join dv in db.DonVis on nnt.MaDonVi equals dv.MaDonVi
                                                     select new NguoiNopThueViewModel
                                                     {
                                                        // DenNgay=nnt.DenNgay,
                                                         Id=nnt.Id,
                                                         // MaDonVi= nnt.MaDonVi,
                                                          MST =nnt.MST,
                                                          //TenDonVi=dv.TenDonVi,
                                                          //TenVietTat1=dv.TenVietTat1,
                                                        // TenVietTat2 = dv.TenVietTat2,
                                                         TenNNT =nnt.TenNNT,
                                                          //TuNgay=nnt.TuNgay                                                         
                                                     };
            return lst.ToList();           
        }

        public List<NguoiNopThueViewModel> Ds_DonViQuanLyNNT()
        {
            //from nk in db.NhatKys
            //join cv in db.CongViecs on nk.IdCongViec equals cv.IdCongViec
            //join nhomcv in db.NhomCongViecs on cv.IdNhomCongViec equals nhomcv.IdNhomCongViec
            //join ut in db.MucUuTiens on nk.IdUuTien equals ut.IdUuTien into mucUuTien
            //join dmtrangthai in db.TrangThais on nk.IdTrangThai equals dmtrangthai.idTrangThai into DmTrangThai
            //where (nk.NgayNhatKy >= tuNgay) && (nk.NgayNhatKy <= denNgay)            

            //from mut in mucUuTien.DefaultIfEmpty()
            //from dmtt in DmTrangThai.DefaultIfEmpty()

            //select new NhatKy_BC01_Model
            //{
            //    NguoiThucHien = nk.NguoiThucHien,
            //    NhomCongViec = cv.IdNhomCongViec,
            //    TenCongViec = nk.TenCongViec,
            //    MoTaSuCo = nk.MoTaSuCo,
            //    VietTatNhomCongViec = nhomcv.VietTatNhomCongViec,
            //    NgayNhatKy = nk.NgayNhatKy,
            //    ThoiGianPhatHien = nk.ThoiGianPhatHien,
            //    ThoiGianXuLy = nk.ThoiGianXuLy,
            //    GhiChu = nk.GhiChu,
            //    TrangThai = dmtt != null ? dmtt.TenTrangThai : "Không có lỗi",
            //    TenUuTien = mut != null ? mut.TenUuTien : "",
            //    IdUuTien = mut != null ? mut.IdUuTien : "",
            //    KetQua = nk.KetQua
            //};
            
            // var list = db.NguoiNopThues.ToList();            
            IEnumerable<NguoiNopThueViewModel> lst = from nnt in db.NguoiNopThues
                                                    join qlnnt in db.QuanLyNNTs on nnt.MST equals qlnnt.MST into QuanLy                                                   

                                                    from ql in QuanLy.DefaultIfEmpty()
                                                    join dv in db.DonVis on ql.MaDonVi equals dv.MaDonVi into QuanLyFull
                                                    from qlf in QuanLyFull.DefaultIfEmpty()
                                                     select new NguoiNopThueViewModel
                                                     {                                                       
                                                         Id = nnt.Id,                                                        
                                                         MST = nnt.MST,
                                                         TenNNT = nnt.TenNNT,
                                                         MaDonVi = ql != null ? ql.MaDonVi : "Chưa phân quản lý",
                                                         //MaDonVi = ql.MaDonVi,
                                                         TenVietTat1 = qlf != null ? qlf.TenVietTat1 : "Chưa phân quản lý", 
                                                         TuNgay =ql.TuNgay,
                                                         DenNgay=ql.DenNgay,                                                      
                                                     };
            return lst.ToList();
        }


        public List<QuanLyTM> Ds_DonViQuanLyTM()
        {            
            return db.QuanLyTMs.ToList();
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

        public void UpDate_MaDonVi_4_NNT(string mst, string tenNNT, DateTime tuNgay, DateTime denNgay, string maCqThu , string maDonVi)
        {
            // Lấy bản ghi có Status =0
            QuanLyNNT quanLy = db.QuanLyNNTs.Where(x => x.MST == mst & x.Status==0).SingleOrDefault();
            NguoiNopThue nnt = db.NguoiNopThues.Where(x=>x.MST== mst).SingleOrDefault();
            NguoiNopThue entityNNT = new NguoiNopThue {MST=mst, TenNNT=tenNNT};
            QuanLyNNT entityQLNNT = new QuanLyNNT {MST=mst,TuNgay=tuNgay,DenNgay=denNgay, MaDonVi=maDonVi };
            int? maxStatus = db.QuanLyNNTs.Where(x => x.MST == mst).OrderByDescending(x => x.Status).Take(1).Select(x=>x.Status).SingleOrDefault();

            TimeSpan? TS_HieuTN_DNDb = tuNgay - quanLy.DenNgay;
            int int_HieuTN_DNDb = TS_HieuTN_DNDb.Value.Days;


            if (quanLy==null) // NNT này chưa có ai quản lý
            {
                if (nnt == null) // và chưa có NNT này thì cập nhật vào bảng NguoiNopThue
                {
                    db.NguoiNopThues.Add(entityNNT);
                    db.SaveChanges();
                    db.QuanLyNNTs.Add(entityQLNNT);
                    db.SaveChanges();
                }
            }
            else
            { //đã có DonVi quản ly suy ra đã có MST trong CSDL
                if(quanLy.DenNgay!= null)
                {

                }
                else
                {

                }



            }





        }






    }
}
