using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhanQuyen.DAO;
using TDST_CRUD.EF;
using System.Net;

namespace TDST.Controllers
{
    public class QLUserController : BaseController
    {
        // GET: QLUser
        public ActionResult Index()
        {
            return View();
        }

        // GET: QLUser
        public ActionResult user()
        {
            Group_UserDao daoQLUser = new Group_UserDao();
            return View(daoQLUser.ListUser());
        }

        [HttpGet]
        public ActionResult group()
        {
            Group_UserDao daoQLUser = new Group_UserDao();
            return View(daoQLUser.ListGroup());
        }      

        public ActionResult role()
        {
            Group_RoleDao dao = new Group_RoleDao();
            return View(dao.ListRoles());
        }

        #region Grup_Usser

        public JsonResult ListUserByGroup(string id)
        {
            Group_UserDao daoQLUser = new Group_UserDao();
            var  dvcc = daoQLUser.ListUser_ByGroup(int.Parse(id)).ToList();
            return Json(dvcc, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RemoveUserFromGroup(string id)
        {
            Group_UserDao dao = new Group_UserDao();
            var kq = dao.RemoveUserFromGroup(id);
            //Check for NULL.
            //if (id != null)
            //{              
            //}
            return Json("0");// insertedRecords);
        }

        public JsonResult AddUserToGroup(string userName, string group)
        {
            long idNewNhatKy = 0;// khởi tạo
            string ketQua = "";
            Group_UserDao dao = new Group_UserDao();
            PGroup_Users newUser = new PGroup_Users();
            newUser.UserName = userName;
            newUser.IdGroup = int.Parse(group);

            //Check for NULL.
            //if (chiTieus != null)
            {
                ketQua = dao.AddUserToGroup(newUser);
                idNewNhatKy = newUser.IdGU;
            }
            return Json(ketQua);// insertedRecords);
        }
        #endregion

        #region Grup_Usser

        public JsonResult ListRoleByGroup(string id)
        {
            Group_RoleDao daoQLRole = new Group_RoleDao();
            var dvcc = daoQLRole.ListRole_ByGroup(int.Parse(id)).ToList();
            return Json(dvcc, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RemoveRoleFromGroup(string id)
        {
            Group_RoleDao dao = new Group_RoleDao();
            var kq = dao.RemoveRoleFromGroup(id);
            //Check for NULL.
            //if (id != null)
            //{              
            //}
            return Json("0");// insertedRecords);
        }

        public JsonResult AddRoleToGroup(string RoleName, string group)
        {
            long idNewNhatKy = 0;// khởi tạo
            string ketQua = "";
            Group_RoleDao dao = new Group_RoleDao();
            PGroup_Roles newRole = new PGroup_Roles();
            newRole.RoleName = RoleName;
            newRole.IdGroup = int.Parse(group);

            //Check for NULL.
            //if (chiTieus != null)
            {
                ketQua = dao.AddRoleToGroup(newRole);
                idNewNhatKy = newRole.IdGR;
            }
            return Json(ketQua);// insertedRecords);
        }
        #endregion

    }
}