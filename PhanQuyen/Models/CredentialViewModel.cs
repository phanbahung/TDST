using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhanQuyen.Models
{
    public class CredentialViewModel
    {   
           
        public int IdGroup { get; set; }
        public string GroupName { get; set; }

        public string RoleName { get; set; }
        public string ActionName { get; set; }
        public string ControllerName { get; set; }      
        

        //  [IdUG]
        //  [UserName]        
        //  [IdGroup]
        //  [GroupName]
        //  [IdGA]
        //  [IdGroup]        
        //  [IdAction]
        //  [ActionName]
        //  [ControllerName]


    }
}