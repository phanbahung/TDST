using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhanQuyen.Common
{
    public static class PConstants
    {
        public static string USER_SESSION = "USER_SESSION";
        public static string SESSION_CREDENTIALS = "SESSION_CREDENTIALS";
        public static string CartSession = "CartSession";

        public static string MEMBER_GROUP = "MEMBER";
        public static string ADMIN_GROUP = "ADMIN";
        public static string MOD_GROUP = "MOD";

        public static int LOGIN_SUCCESS = 1;
        public static int LOGIN_USER_NOT_EXIST = 0;
        public static int LOGIN_USER_LOCKED = -1;
        public static int LOGIN_PASS_WRONG = -2;
        public static int LOGIN_USER_DENIED_LOGIN = -3;
       

       


        public static string CurrentCulture { set; get; }
    }
}