using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModels;
using TDST.Common;
using TDST_CRUD.Dao;
using TDST_CRUD.ViewModels;
using PhanQuyen.DAO;
using PhanQuyen.Models;

namespace TDST.Controllers
{
    public class UserController : BaseController
    {        

        public ActionResult Home()
        {
            var session = (UserLogin)Session[Common.CommonConstants.USER_SESSION];
            UserDao dao = new UserDao();
            List<CredentialViewModel> listCredentials = new List<CredentialViewModel>();
            if (session != null)
            {
                listCredentials = dao.GetListCredential_By_UserName(session.UserName);
            }


            return View(listCredentials);
        }

        public ActionResult Logout()
        {
            Session[CommonConstants.USER_SESSION] = null;
            return Redirect("/User/Index");
        }

       
    }
}