using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using TDST.Models;
using TDST_CRUD.Dao;
using TDST_CRUD.EF;

namespace TDST.Controllers
{
    public class HeThongController : Controller
    {
        // GET: HeThong
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DsRole()
        {
            RoleDao daoRole = new RoleDao();
            return View(daoRole.Get_DS_Role());            
        }

        public void UpdateRole()
        {
            ReflectionController reflection = new ReflectionController();          
            Role role = new Role();
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
    }
}