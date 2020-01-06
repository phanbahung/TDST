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
    public class NhomChuongController : Controller
    {
        // GET: NhomChuong
        [HttpGet]
        [HasCredential(RoleID = "NhomChuong_DanhSach_NhomChuong", MoTa = "Hiển thị danh sách các nhóm chương - tự định nghĩa")]
        public ActionResult Index()
        {
             DanhMucDao dao = new DanhMucDao();
            return View(dao.Get_DmNhomChuong());
        }     


        [HasCredential(RoleID = "NhomChuong_View_ChiTiet", MoTa = "Hiển thị màn hình sửa nhóm chương")]
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
    }
}