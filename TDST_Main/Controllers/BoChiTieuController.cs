using TDST.Models;
using System.Web.Mvc;
using System.Collections.Generic;
using TDST_CRUD.EF;
using System.Linq;
using System.Data.Entity.Infrastructure;
using TDST_CRUD.Dao;

using TDST_CRUD.ViewModels;

namespace TDST.Controllers
{
    public class BoChiTieuController : BaseController
    {
        TDSTDbContext db = new TDSTDbContext();
        // GET: BoChiTieu
        [HttpGet]
        [HasCredential(RoleID = "BOCHITIEU_VIEW", MoTa = "Hiển thị form bộ chỉ tiêu")]
        public ActionResult Index()
        {
            BoChiTieuDao dao = new BoChiTieuDao();
            return View(dao.Ds_BoChiTieu());
        }

        [HasCredential(RoleID = "BOCHITIEU_VIEW_TO_EDIT", MoTa = "Hiển thị form ds chỉ tiêu của BCT cụ thể")]
        public ActionResult edit(int id)
        {
            DanhMucDao dao = new DanhMucDao();
            BoChiTieuDao daoBCT = new BoChiTieuDao();
            ViewBag.ListNhomChuongs = dao.DmNhomChuong();
            ViewBag.ListNhomTieuMucs = dao.DmNhomTieuMuc();
            ViewBag.IdBoChiTieu = id;


            //if (id == null)
            //{                                 
            //    //RedirectToAction("Index");
            //    return View(new List<ChiTieuModelView>());
            //}
            //else
            return View(daoBCT.Get_DsChiTieu(id));

        }

        [HasCredential(RoleID = "BOCHITIEU_InsertChiTieu", MoTa = "Cho phép thêm một chỉ tiêu mới")]
        public JsonResult InsertChiTieu(List<BoChiTieuChiTiet> chiTieus)
        {
            int idChiTieu = 0;// khởi tạo
           TDSTDbContext db = new TDSTDbContext();

            //Check for NULL.
            if (chiTieus != null)
            {
                db.BoChiTieuChiTiets.Add(chiTieus[0]);
                db.SaveChanges();
                idChiTieu = chiTieus[0].IdChiTieu;
            }
            return Json(idChiTieu);// insertedRecords);
        }

        [HasCredential(RoleID = "BOCHITIEU_DeleteChiTieu", MoTa = "Cho phép xóa một chỉ tiêu")]
        public JsonResult DeleteChiTieu(int? id)
        {
            
            TDSTDbContext db = new TDSTDbContext();
            //Check for NULL.
            //if (id != null)
            {
                BoChiTieuChiTiet chiTieu = db.BoChiTieuChiTiets.Find(id);
                db.BoChiTieuChiTiets.Remove(chiTieu);
                db.SaveChanges();
            }
            return Json("");// insertedRecords);
        }

        [HasCredential(RoleID = "BOCHITIEU_UpdateChiTieus", MoTa = "Cho phép update các thay đổi của chỉ tiêu trong BCT")]
        public JsonResult UpdateChiTieus(List<BoChiTieuChiTiet> chiTieus)
        {
            using (TDSTDbContext entities = new TDSTDbContext())
            {
                //Check for NULL.
                if (chiTieus == null)
                {
                    chiTieus = new List<BoChiTieuChiTiet>();
                }
                BoChiTieuChiTiet capnhat;
                //Loop and insert records.
                foreach (BoChiTieuChiTiet chiTieu in chiTieus)
                {
                    capnhat = entities.BoChiTieuChiTiets.Find(chiTieu.IdChiTieu);
                   // if ((chiTieu.TenChiTieu != null) && (chiTieu.TenChiTieu.Trim() != ""))
                        capnhat.TenChiTieu = chiTieu.TenChiTieu;
                   // if (chiTieu.IdNhomChuong != null) 
                        capnhat.IdNhomChuong = chiTieu.IdNhomChuong;
                   // if (chiTieu.IdNhomTieuMuc != null)
                        capnhat.IdNhomTieuMuc = chiTieu.IdNhomTieuMuc;
                   // if (chiTieu.STT != null) 
                        capnhat.STT = chiTieu.STT;
                    entities.SaveChanges();
                }               
                return Json("Thành công");// insertedRecords);
            }
        }

        [HasCredential(RoleID = "BOCHITIEU_CreateBCT_Get", MoTa = "Hiển thị màn hình thêm chỉ tiêu mới")]
        public ActionResult Create()
        {
        //    string[] strings = new[] {["",""],["", ""]};
        //    var selectListItems = strings.Select(x => new SelectListItem() { Text = x, Value = x, Selected => x == "item 1" });

        //string[] strings = new[] { .. strings.. };
        //var sl = strings.Select(s => new SelectListItem { Value = s }).ToList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HasCredential(RoleID = "BOCHITIEU_CreateBCT_Post", MoTa = "cho pehsp thêm một BCT mới")]
        public ActionResult Create([Bind(Include = "TenBoChiTieu,Nam")]BoChiTieu bct)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bct.UserUpdate = "admin";
                    db.BoChiTieus.Add(bct);
                    db.SaveChanges();
                    return RedirectToAction("thietlap");
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.)
                ModelState.AddModelError("", "Không thể lưu BCT mới. Bạn thử lại!.");
            }            
            return View(bct);
        }
        

        public void SetViewBag (long ? selectedId =null)
        {
            var bct = new BoChiTieuDao();
            List<BoChiTieu> boChiTieu = bct.Ds_BoChiTieu();
            SelectList boChiTieuList = new SelectList(boChiTieu, "IdBoChiTieu", "TenBoChiTieu", selectedId);
            ViewBag.BoChiTieuList = boChiTieuList;
        }

        [HttpPost]
        public ActionResult UpdateNoiDungBTC(BoChiTieuChiTiet chitieu)
        {
            using (TDSTDbContext entities = new TDSTDbContext())
            {
                BoChiTieuChiTiet updatedDSChiTieu = (from c in entities.BoChiTieuChiTiets
                                                     where c.IdChiTieu == chitieu.IdChiTieu
                                            select c).FirstOrDefault();

                updatedDSChiTieu.IdNhomChuong = chitieu.IdNhomChuong;
                updatedDSChiTieu.IdNhomTieuMuc = chitieu.IdNhomTieuMuc;
                updatedDSChiTieu.STT = chitieu.STT;
                updatedDSChiTieu.TenChiTieu = chitieu.TenChiTieu;                
                entities.SaveChanges();
            }
            return new EmptyResult();
        }



        public JsonResult GetbyId(int idCTBC)
        {
            var baocao = new BoChiTieuDao();
            // if (idCTBC == null) idCTBC = "0"; // Nha Trang

            var model = baocao.Get_DsChiTieu(idCTBC);
            return Json(model.ToList(), JsonRequestBehavior.AllowGet);

        }

       
    }
}