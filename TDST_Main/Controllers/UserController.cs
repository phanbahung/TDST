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
using PhanQuyen.Common;

namespace TDST.Controllers
{
    public class UserController : BaseController
    {        

        public ActionResult Home()
        {
            var session = (UserLogin)Session[Common.CommonConstants.USER_SESSION];
            UserDao dao = new UserDao();
            
            List<string> listCredentials = new List<string>();
            if (session != null)
            {
                listCredentials = dao.GetListCredential_By_UserName(session.UserName);
            }


            return View(listCredentials);
        }

        [HttpGet]
        public ActionResult ChangePass()
        {
            var session = (UserLogin)Session[PConstants.USER_SESSION];
            UserChangePassModel returnUser = new UserChangePassModel();
            if (session != null)
            {
                //var user = new UserDao().GetById(session.UserName);               
                returnUser.UserName = session.UserName.Trim();
                returnUser.OldPass = "";
                returnUser.NewPass1 = "";
                returnUser.NewPass2 = "";
            }
            else return RedirectToAction("Index", "Login");
            return View(returnUser);
        }

        [HttpPost]
        public ActionResult ChangePass(UserChangePassModel model)
        {
            string encryptedOldPass = Encryptor.MD5Hash(model.OldPass);


            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                if (!dao.CheckPassword(model.UserName, encryptedOldPass))
                {
                    ModelState.AddModelError("", "Mật khẩu cũ không đúng");
                }
                else if (model.NewPass1 != model.NewPass2)
                {
                    ModelState.AddModelError("", "Mật khẩu mới không giống nhau");
                }

                else if (model.NewPass1.Length < 5)
                {
                    ModelState.AddModelError("", "Mật khẩu phải ít nhất 5 kí tự");
                }

                else
                {
                    string encryptedNewPass = Encryptor.MD5Hash(model.NewPass1);
                    bool kq = dao.ChangePass(model.UserName, encryptedNewPass);

                    if (kq)
                    {
                        ModelState.AddModelError("", "Đổi mật khẩu thành công!");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Đổi mật khẩu không thành công.");
                    }
                }
            }
            return View();
        }


        public ActionResult Logout()
        {
            Session[CommonConstants.USER_SESSION] = null;
            return Redirect("/User/Index");
        }

       
    }
}