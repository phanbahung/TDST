using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDST_CRUD.Dao;

namespace TDST.Controllers
{
    public class NNTController : BaseController
    {
        // GET: NNT
        public ActionResult Index()
        {
            NguoiNopThueDao nntDao = new NguoiNopThueDao();

            return View(nntDao.Ds_DonViQuanLyNNT());
        }

        public ActionResult qltm()
        {
            NguoiNopThueDao nntDao = new NguoiNopThueDao();

            return View(nntDao.Ds_DonViQuanLyTM());
        }


    }
}