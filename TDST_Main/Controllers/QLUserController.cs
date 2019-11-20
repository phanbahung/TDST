using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDST_CRUD.Dao;

namespace TDST.Controllers
{
    public class QLUserController : Controller
    {
        // GET: QLUser
        public ActionResult Index()
        {
            return View();
        }

        // GET: QLUser
        public ActionResult user()
        {
            QLUserDao daoQLUser = new QLUserDao();
            return View(daoQLUser.ListUser());
        }

        public ActionResult nhom()
        {
            QLUserDao daoQLUser = new QLUserDao();
            return View(daoQLUser.ListGroup());
        }

        public ActionResult role()
        {
            QLUserDao daoQLUser = new QLUserDao();
            return View(daoQLUser.LisAction());
        }
    }
}