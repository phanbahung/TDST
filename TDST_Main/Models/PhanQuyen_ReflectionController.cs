using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using PhanQuyen.Models;


namespace TDST.Models
{
    public class PhanQuyen_ReflectionController
    {



        //Assembly asm = Assembly.GetAssembly(typeof(MyWebDll.MvcApplication));

        //var controlleractionlist = asm.GetTypes()
        //        .Where(type => typeof(System.Web.Mvc.Controller).IsAssignableFrom(type))
        //        .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
        //        .Where(m => !m.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), true).Any())
        //        .Select(x => new { Controller = x.DeclaringType.Name, Action = x.Name, ReturnType = x.ReturnType.Name, Attributes = String.Join(",", x.GetCustomAttributes().Select(a => a.GetType().Name.Replace("Attribute", ""))) })
        //        .OrderBy(x => x.Controller).ThenBy(x => x.Action).ToList();



        //Lảy danh sách các Controller 
        public List<Type> GetControllers(string namespaces)
        {
            //List<Type> listController = new List<Type>();
            Assembly assembly = Assembly.GetExecutingAssembly();
            IEnumerable<Type> types = assembly.GetTypes().Where(type => typeof(Controller).IsAssignableFrom(type) && type.Namespace.Contains(namespaces)).OrderBy(x => x.Name);
            return types.ToList();
        }

        //Lay danh sách các action theo controller
        public IEnumerable<MemberInfo> GetActions(Type controller)
        {
            //List<string> listAction = new List<string>();

            IEnumerable<MemberInfo> memberInfo = controller.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly |
            BindingFlags.Public).Where(m => !m.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), true).Any()).OrderBy(x => x.Name);
           
            return memberInfo;
        }




        //Lay danh sách các attribute theo action
        public List<AttributeModel> GetAttributes(MemberInfo method)
        {
            List<AttributeModel> listAttributes = new List<AttributeModel>();

            // Lấy ds các Attribute: thực ra HasCredentialAttribute chỉ có 1 record
            var myAttribute = method.GetCustomAttributes(true).OfType<HasCredentialAttribute>().FirstOrDefault();
                if (myAttribute != null)
                {                 
                    listAttributes.Add(new AttributeModel { RoleId= myAttribute.RoleID, MoTa= myAttribute.MoTa });
                }
           
            return listAttributes;
        }

        //Lay danh sách các action theo controller
        public List<string> GetActions_Backup(Type controller)
        {
            List<string> listAction = new List<string>();

            IEnumerable<MemberInfo> memberInfo = controller.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly |
            BindingFlags.Public).Where(m => !m.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), true).Any()).OrderBy(x => x.Name);

            foreach (MemberInfo method in memberInfo)
            {
                if ((method.ReflectedType.IsPublic) && !method.IsDefined(typeof(NonActionAttribute)))
                {
                    listAction.Add(method.Name.ToString());
                }
            }
            return listAction;
        }


    }
}
        