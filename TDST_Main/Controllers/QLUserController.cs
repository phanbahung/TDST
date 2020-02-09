using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhanQuyen.DAO;
using TDST_CRUD.EF;
using System.Net;
using PhanQuyen.Models;

namespace TDST.Controllers
{
    public class QLUserController : BaseController
    {      

        // GET: QLUser
        //[HasCredential(RoleID = "USER_DanhSachUser_Get", MoTa = "Hiển thị danh sách user")]
        public ActionResult user()
        {
            Group_UserDao daoQLUser = new Group_UserDao();
            ViewBag.ListGroups =daoQLUser.ListGroup();
            return View(daoQLUser.ListUser());
        }

        [HttpGet]
        //[HasCredential(RoleID = "USER_DanhSachGroupUser_Get", MoTa = "Hiển thị danh sách nhóm user")]
        public ActionResult group()
        {
            Group_UserDao daoQLUser = new Group_UserDao();
            RoleDao daoRole = new RoleDao();
            ViewBag.ListRoles = daoRole.Get_DS_Role();
            return View(daoQLUser.ListGroup());
        }

        #region Group_User
        //[HasCredential(RoleID = "USER_ListUserByGroup_Get", MoTa = "Hiển thị danh sách user theo nhóm")]
        public JsonResult ListUserByGroup(string id)
        {
            Group_UserDao daoQLUser = new Group_UserDao();
            var  dvcc = daoQLUser.ListUser_ByGroup(int.Parse(id)).ToList();
            return Json(dvcc, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListGroupByUser(string id)
        {
            Group_UserDao daoQLUser = new Group_UserDao();
            var dvcc = daoQLUser.ListGroup_ByUser(id).ToList();
            return Json(dvcc, JsonRequestBehavior.AllowGet);
        }

        [HasCredential(RoleID = "USER_RemoveUserFromGroup_Post", MoTa = "Xóa User khỏi nhóm")]
        public JsonResult RemoveUserFromGroup(string userName, string group)
        {
            Group_UserDao dao = new Group_UserDao();
            var kq = dao.RemoveUserFromGroup(userName, int.Parse(group));
            //Check for NULL.
            //if (id != null)
            //{              
            //}
            return Json("0");// insertedRecords);
        }

        [HasCredential(RoleID = "USER_RemoveUserFromGroup_Post", MoTa = "Xóa User khỏi nhóm")]
        public JsonResult RemoveGroupFromUser(string userName, string group)
        {
            Group_UserDao dao = new Group_UserDao();
            var kq = dao.RemoveGroupFromUser(userName, int.Parse(group));
            //Check for NULL.
            //if (id != null)
            //{              
            //}
            return Json("0");// insertedRecords);
        }

        [HasCredential(RoleID = "USER_AddUserToGroup_Post", MoTa = "Thêm User vào nhóm")]
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
        
        public JsonResult DetailUser(string id)
        {           
            UserDao dao = new UserDao();
            return Json(dao.GetByIdUser(int.Parse(id)), JsonRequestBehavior.AllowGet);
        }

        //[HasCredential(RoleID = "USER_UpdateUser_Post", MoTa = "Update user")]
        public JsonResult UpdateUser(PUser_InputModel entity)
        {
            UserDao dao = new UserDao();
            if (entity == null)
            {
                entity = new PUser_InputModel();
            }

            PUser userToUpdate= new PUser();
            userToUpdate.FullName = entity.FullName;
            userToUpdate.IdUser =  int.Parse(entity.IdUser);
            userToUpdate.Status = entity.Status.Trim().ToUpper()=="TRUE"? true: false;
            return Json(dao.UpdateUser(userToUpdate));
        }

    }
}