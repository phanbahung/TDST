using System;
using System.Collections.Generic;

using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using TDST.Models;
using TDST_CRUD.EF;
using PhanQuyen.Models;

namespace TDST.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        //[HasCredential(RoleID = "ORDER_UPDATESTATUS", MoTa ="kkeke")]
        public ActionResult Index()
        {
            PhanQuyen_ReflectionController reflection = new PhanQuyen_ReflectionController();
            List<Type> listController = reflection.GetControllers("TDST.Controllers");
            string result = "<ul>";
            foreach (Type controller in listController)
            {
                result += "<li>" + controller.Name;
                IEnumerable<MemberInfo> listAction = reflection.GetActions(controller);
                result += "<ul>";
                foreach (MemberInfo action in listAction)
                {
                    result += "<li>" + action.Name.ToString() + "</li>";
                    result += "<ul>";

                    List<AttributeModel> listAttrib = reflection.GetAttributes(action);

                    foreach(AttributeModel attrib in listAttrib)
                    {
                        result += "<li>" + attrib.RoleId + " - "+attrib.MoTa+"</li>";
                    }

                    result += "</ul>";

                }
                result += "</ul></li>";
            }
            result += "</ul>"; 
            ViewBag.result = result;
            return View();
        }



        


    }
}