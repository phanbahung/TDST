using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModels;
using TDST.Common;
using TDST_CRUD.Dao;

using BMTT.DAO;
using BMTT.Models;
using BMTT.Common;

namespace TDST.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PermissionDenied()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Session[CommonConstants.USER_SESSION] = null;
            return Redirect("/Login/Index");
        }

        public ActionResult Login(Login_ViewModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var result = dao.Login(model.UserName, Encryptor.MD5Hash(model.Password), false);
                if (result == PConstants.LOGIN_SUCCESS)
                {
                    var user = dao.GetByUserName(model.UserName);
                    var userSession = new UserLogin();
                    userSession.UserName = user.UserName;
                    userSession.FullName = user.FullName;
                    //userSession.UserID = user.Id;
                    //userSession.GroupID = user.GroupId;
                    var listCredentials = dao.GetListCredential_By_UserName(model.UserName);
                    Session.Add(CommonConstants.SESSION_CREDENTIALS, listCredentials);
                    Session.Add(CommonConstants.USER_SESSION, userSession);
                    return RedirectToAction("Index", "User");
                }
                else if (result == PConstants.LOGIN_USER_NOT_EXIST)
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại.");
                }
                else if (result == PConstants.LOGIN_USER_LOCKED)
                {
                    ModelState.AddModelError("", "Tài khoản đang bị khoá.");
                }
                else if (result == PConstants.LOGIN_PASS_WRONG)
                {
                    ModelState.AddModelError("", "Mật khẩu không đúng.");
                }
              
            }
            return View("Index");
        }
    }
}