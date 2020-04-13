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
        [OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, Duration = 2592000)]
        // 2592000: 30 ngày 
        // 86400: 1 ngày
        public ActionResult TieuMuc()
        {
            DanhMucDao dao = new DanhMucDao();
            return View(dao.Get_DmTieuMuc());
        }








    }
}