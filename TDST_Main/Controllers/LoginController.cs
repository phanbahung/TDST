using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModels;
using TDST.Common;
using TDST_CRUD.Dao;

using PhanQuyen.DAO;
using PhanQuyen.Models;

namespace TDST.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

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

        public ActionResult Login(Login_ViewModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var result = dao.Login(model.UserName, Encryptor.MD5Hash(model.Password), false);
                if (result == 1)
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
                    return RedirectToAction("Home", "User");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại.");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Tài khoản đang bị khoá.");
                }
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Mật khẩu không đúng.");
                }
                else if (result == -3)
                {
                    ModelState.AddModelError("", "Tài khoản của bạn không có quyền đăng nhập.");
                }
                else
                {
                    ModelState.AddModelError("", "Đăng nhập không đúng.");
                }
            }
            return View("Index");
        }
    }
}