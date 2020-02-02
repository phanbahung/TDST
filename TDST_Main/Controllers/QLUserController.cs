using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDST_CRUD.Dao;
using PhanQuyen.DAO;

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

        public ActionResult group()
        {
            QLUserDao daoQLUser = new QLUserDao();
            return View(daoQLUser.ListGroup());
        }

        public ActionResult groupuser(string id)
        {
            QLUserDao daoQLUser = new QLUserDao();
            return View(daoQLUser.ListUser_ByGroup(int.Parse(id)));
        }
        public ActionResult grouprole()
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