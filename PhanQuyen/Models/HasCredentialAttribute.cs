using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;


namespace PhanQuyen.Models
{
    public class HasCredentialAttribute : AuthorizeAttribute
    {
        public string RoleID { set; get; }
        public string MoTa { set; get; }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            //var session = (UserLogin)HttpContext.Current.Session[Common.CommonConstants.USER_SESSION];
            //if (session == null)
            //{
            //    return false;
            //}

            //List<string> privilegeLevels = this.GetCredentialByLoggedInUser(session.UserName); // Call another method to get rights of the user from DB

            //// if (privilegeLevels.Contains(this.RoleID) || session.GroupID == CommonConstants.ADMIN_GROUP)
            //if (privilegeLevels.Contains(this.RoleID))
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
            return true;
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new ViewResult
            {
               // ViewName = "~/Areas/Admin/Views/Shared/401.cshtml"
            };
        }
        private List<string> GetCredentialByLoggedInUser(string userName)
        {
            var credentials = (List<string>)HttpContext.Current.Session[PConstants.USER_SESSION];
            return credentials;
        }
    }
}