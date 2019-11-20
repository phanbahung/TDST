using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TDST.Controllers
{
    public class PhanQuyenController : Controller
    {
        // GET: PhanQuyen
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DSUser()
        {
            return View();
        }
        public ActionResult DSNhomUser()
        {
            return View();
        }

        public ActionResult DSRole()
        {
            return View();
        }

    }
}