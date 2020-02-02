using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhanQuyen.Models
{
    public class CredentialViewModel
    {   
           
        public string IdGroup { get; set; }
        public string GroupName { get; set; }

        public string IdAction { get; set; }
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