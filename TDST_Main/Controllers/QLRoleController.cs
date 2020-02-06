using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhanQuyen.DAO;
using TDST_CRUD.EF;
using System.Net;
using PhanQuyen.Models;
using System.Reflection;
using TDST.Models;
using TDST_CRUD.Dao;


namespace TDST.Controllers
{
    public class QLRoleController : BaseController
    {
        // GET: HeThong
        //[HasCredential(RoleID = "HETHONG_QuanTriRole_Get", MoTa = "Hiển thị màn hình quản trị role - ADMIN dùng")]
        public ActionResult QuanTriRole()
        {
            return View();
        }

        //[HasCredential(RoleID = "HETHONG_QuanTriRole_DanhSach", MoTa = "Hiển thị danh sách role")]
        public ActionResult DsRole()
        {
            RoleDao daoRole = new RoleDao();
            return View(daoRole.Get_DS_Role());
        }

        //[HasCredential(RoleID = "HETHONG_QuanTriRole_Post", MoTa = "Chức năng cập nhật role - ADMIN dùng")]
        public void UpdateRole()
        {
            ReflectionHoLaoController reflection = new ReflectionHoLaoController();
            PRole role = new PRole();
            List<AttributeModel> listAttrib;
            RoleDao daoRole = new RoleDao();
            daoRole.Truncate_RoleTabe();
            List<Type> listController = reflection.GetControllers("TDST");

            foreach (Type controller in listController)
            {
                IEnumerable<MemberInfo> listAction = reflection.GetActions(controller);
                foreach (MemberInfo action in listAction)
                {
                    listAttrib = reflection.GetAttributes(action);
                    foreach (AttributeModel attrib in listAttrib)
                    {
                        role.ControllerName = controller.Name;
                        role.ActionName = action.Name;
                        role.RoleName = attrib.RoleId;
                        role.MoTa = attrib.MoTa;
                        daoRole.Insert(role);
                    }
                }
            }

        }
        // GET: QLUser
        //[HasCredential(RoleID = "CHUNGTU_UploadXMLFiles_Get", MoTa = "Hiển thị màn hình upload file XML")]
        public ActionResult Index()
        {
            return View();
        }

        // GET: QLUser
        //[HasCredential(RoleID = "ROLE_DanhSachUser_Get", MoTa = "Hiển thị danh sách user")]
        public ActionResult user()
        {
            Group_RoleDao dao = new Group_RoleDao();
            return View(dao.ListRoles());
        }

        [HttpGet]
        //[HasCredential(RoleID = "ROLE_DanhSachGroupUser_Get", MoTa = "Hiển thị danh sách nhóm user")]
        public ActionResult group()
        {
            Group_UserDao daoQLUser = new Group_UserDao();
            return View(daoQLUser.ListGroup());
        }

        #region Group_User
        //[HasCredential(RoleID = "ROLE_ListUserByGroup_Get", MoTa = "Hiển thị danh sách role theo nhóm")]
        public JsonResult ListRoleByGroup(string id)
        {
            Group_RoleDao dao = new Group_RoleDao();
            var  dvcc = dao.ListRole_ByGroup(int.Parse(id)).ToList();
            return Json(dvcc, JsonRequestBehavior.AllowGet);
        }

        [HasCredential(RoleID = "ROLE_RemoveRoleFromGroup_Post", MoTa = "Xóa role khỏi nhóm")]
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
        [HasCredential(RoleID = "ROLE_AddRoleToGroup_Post", MoTa = "Thêm role vào nhóm")]
        public JsonResult AddRoleToGroup(string roleName, string group)
        {
            long idNewNhatKy = 0;// khởi tạo
            string ketQua = "";
            Group_RoleDao dao = new Group_RoleDao();
            PGroup_Roles newUser = new PGroup_Roles();
            newUser.RoleName = roleName;
            newUser.IdGroup = int.Parse(group);

            //Check for NULL.
            //if (chiTieus != null)
            {
                ketQua = dao.AddRoleToGroup(newUser);
                idNewNhatKy = newUser.IdGR;
            }
            return Json(ketQua);// insertedRecords);
        }
        #endregion      

    }
}