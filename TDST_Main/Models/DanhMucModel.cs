using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TDST_CRUD.EF;
using TDST;
using System.Data.SqlClient;

namespace TDST.Models
{
    
    public class DanhMucModel
    {
        private TDSTDbContext context = null;

        public DanhMucModel()
            {
                context = new TDSTDbContext();
            }

        public List<DmDonVi> Get_DmDonVi(string id)
        {
            object[] sqlParams =
            {
                new SqlParameter ("@MaDonViCha", id),
            };
            //var list = context.Database.SqlQuery<DmDonVi>("spDmDonVi_Select @MaDonViCha", sqlParams).ToList();
            var list = context.DmDonVi.ToList();
            return list;
        }

      


        public List<DmChuong> Get_DmChuong(string id)
        {
            //object[] sqlParams =
            //{
            //    new SqlParameter ("@NhomChuong", id),
            //};
            var list = context.DmChuong.ToList();
            //var list = context.Database.SqlQuery<DmChuong>("spDmChuong_Select").ToList();
            return list;
        }

        public List<DmNhomChuong> Get_DmNhomChuong()
        {
            var list = context.Database.SqlQuery<DmNhomChuong>("spDmNhomChuong_Select").ToList();                       
            return list;
        }

        public List<DmNhomTieuMuc> Get_DmNhomTieuMuc()
        {
            var list = context.Database.SqlQuery<DmNhomTieuMuc>("spDmNhomTieuMuc_Select").ToList();
            return list;
        }


        public List<DmTieuMuc> Get_DmTieuMuc(string id)
        {
            //object[] sqlParams =
            //{
            //    new SqlParameter ("@Muc", id),
            //};
            var list = context.Database.SqlQuery<DmTieuMuc>("spDmTieuMuc_Select").ToList();
            return list;
        }

    }
}