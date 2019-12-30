using TDST.Models;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Collections.Generic;
using TDST_CRUD.EF;
using TDST_CRUD.Dao;


namespace TDST.Controllers
{
    public class DanhMucController : Controller
    {
        // GET: DanhMuc        


        #region Danh muc đơn vị
        [HttpGet]
        [OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, Duration = 2592000)]
        // 2592000: 30 ngày 
        // 86400: 1 ngày
        public ActionResult DonVi()
        {
            DanhMucDao dao = new DanhMucDao();            
            return View(dao.Get_DmDonVi());
        }
        #endregion

        #region Danh muc Chương - Nhóm Chương
        [HttpGet]
        [OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, Duration = 2592000)]
        // 2592000: 30 ngày 
        // 86400: 1 ngày
        public ActionResult Chuong()
        {
            DanhMucDao dao = new DanhMucDao();
            return View(dao.Get_DmChuong());
        }

        [HttpGet]
        [HasCredential(RoleID = "DANHMUC_NhomChuong_Get", MoTa = "Hiển thị nhóm chương")]
        public ActionResult NhomChuong()
        {
            DanhMucDao dao = new DanhMucDao();
            return View(dao.Get_DmNhomChuong());
        }

        [HasCredential(RoleID = "DANHMUC_NhomChuong_Get", MoTa = "Hiển thị nhóm chương")]
        public ActionResult EditNhomChuong(long id)
        {
            DanhMucDao dao = new DanhMucDao();            
            ViewBag.DmChuongs = dao.Get_DmChuong();
            ViewBag.IdNhomChuong = id;                 
            return View(dao.Get_DmNhomChuong_ById(id));           
        }


        [HasCredential(RoleID = "DANHMUC_AddChuong_To_NhomChuong", MoTa = "Thêm Chương vào Nhóm Chương (do NSD định nghĩa)")]
        public JsonResult Add_Chuong_To_NhomChuong(List<NhomCH_CH> entities)
        {
            long id = 0;// khởi tạo
            TDSTDbContext db = new TDSTDbContext();

            //Check for NULL.
            if (entities != null)
            {
                db.NhomCH_CH.Add(entities[0]);
                db.SaveChanges();
                id = entities[0].IdNhomCH_CH;
            }
            return Json(id);// insertedRecords;
        }

        [HasCredential(RoleID = "DANHMUC_RemoveChuong_From_NhomChuong", MoTa = "Xóa Chương khỏi NhómChương (do NSD định nghĩa)")]
        public JsonResult Remove_Chuong_From_NhomChuong(long? id)
        {
            TDSTDbContext db = new TDSTDbContext();
            //Check for NULL.
            //if (id != null)
            {
                NhomCH_CH item = db.NhomCH_CH.Find(id);
                db.NhomCH_CH.Remove(item);
                db.SaveChanges();
            }
            return Json("");// insertedRecords);
        }


        #endregion


        #region Danh muc TM - NhómTM
        [HttpGet]
        [OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, Duration = 2592000)]
        // 2592000: 30 ngày 
        // 86400: 1 ngày
        public ActionResult TieuMuc()
        {
            DanhMucDao dao = new DanhMucDao();
            return View(dao.Get_DmTieuMuc());
        }

        public ActionResult NhomTieuMuc()
        {
            DanhMucDao dao = new DanhMucDao();

            List <NhomTieuMuc> nhomTMs = dao.Get_DmNhomTieuMuc();
            int j;
            string ketQua;
            ketQua = "";
            int soLuongNhomTMs = nhomTMs.Count();

            for (int i = 0; i< soLuongNhomTMs-1; i++)
            {
                for (j=i+1; j< soLuongNhomTMs; j++)
                {
                    if (intersection2(nhomTMs[i].Ds_MaTieuMuc, nhomTMs[j].Ds_MaTieuMuc)!="" )
                    {
                        ketQua = ketQua + nhomTMs[i].TenNhomTieuMuc +"-" +nhomTMs[i].TenNhomTieuMuc;
                    }
                }
            }

            return View(nhomTMs);          

        } 
        #endregion

        private string intersection2(string x1, string x2)
        {
            x1 = x1.Replace(" ", "");
            x2 = x2.Replace(" ", "");
            string[] string1 = x1.Split(',');
            string[] string2 = x2.Split(',');
            var m = string1.Distinct();
            var n = string2.Distinct();
            var test = " ";

            var results = m.Intersect(n, StringComparer.OrdinalIgnoreCase);
            foreach (var k in results) test += k + " ";
            return test;
        }

      
    }
}