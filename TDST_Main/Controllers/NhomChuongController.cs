using TDST.Models;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Collections.Generic;
using TDST_CRUD.EF;
using TDST_CRUD.Dao;
using BMTT.Models;


namespace TDST.Controllers
{
    public class NhomChuongController : BMTT.Controllers.BaseController
    {
        // GET: NhomChuong

        #region Danh muc Chương - Nhóm Chương
       

        //[HasCredential(RoleID = "NhomChuong_DanhSach_NhomChuong", MoTa = "Hiển thị danh sách các nhóm chương - tự định nghĩa")]
        public ActionResult NhomChuong()
        {
             DanhMucDao dao = new DanhMucDao();
            return View(dao.Get_DmNhomChuong());
        }     


       // [HasCredential(RoleID = "NhomChuong_View_ChiTiet", MoTa = "Hiển thị màn hình sửa nhóm chương")]
        public ActionResult edit(long id)
        {
            DanhMucDao dao = new DanhMucDao();
            ViewBag.DmChuongs = dao.Get_DmChuong();
            ViewBag.IdNhomChuong = id;
            return View(dao.Get_ChiTietNhomChuong_ById(id));
        }

        [HasCredential(RoleID = "NhomChuong_Delete_ChiTiet", MoTa = "Cho phép xóa một chương của nhóm chương")]
        public JsonResult DeleteChiTiet(long? id)
        {
            TDSTDbContext db = new TDSTDbContext();
            //Check for NULL.
            //if (id != null)
            {
                NhomCH_CH entity = db.NhomCH_CH.Find(id);
                db.NhomCH_CH.Remove(entity);
                db.SaveChanges();
            }
            return Json("");// insertedRecords);
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
    }
}