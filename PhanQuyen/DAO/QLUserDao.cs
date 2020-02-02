using System;
using System.Collections.Generic;
using System.Linq;
using TDST_CRUD.EF;
using PhanQuyen.Models;


namespace PhanQuyen.DAO
{
    public class QLUserDao
    {
        TDSTDbContext db = null;
        public QLUserDao()
        {
            db = new TDSTDbContext();
        }

        public List<PUser> ListUser()
        {
            return db.PUsers.ToList();
        }

        public List<PRole> LisAction()
        {
            return db.PRoles.ToList();
        }

        public List<PGroup> ListGroup()
        {
            return db.PGroups.ToList();
        }

        public List<PGroup_Users> ListUser_ByGroup(int idGroup)
        {
            return db.PGroup_Users.Where(x => x.IdGroup == idGroup).ToList();
            
        }

        public List<PGroup_Roles> ListRole_ByGroup(int idGroup)
        {
            return db.PGroup_Roles.Where(x => x.IdGroup == idGroup).ToList();
        }

        //public long Insert(User entity)
        //{
        //    db.Users.Add(entity);
        //    db.SaveChanges();
        //    return entity.Id;
        //}

        //public long Edit(User entity)
        //{
        //    db.Users.Add(entity);
        //    db.SaveChanges();
        //    return entity.Id;
        //}
        //public long ResetPass(User entity)
        //{
        //    db.Users.Add(entity);
        //    db.SaveChanges();
        //    return entity.Id;
        //}

        //public long LockUser(User entity)
        //{
        //    db.Users.Add(entity);
        //    db.SaveChanges();
        //    return entity.Id;
        //}

        public List<CredentialViewModel> GetListCredential(string userName)
        {
            var user = db.PUsers.Single(x => x.UserName == userName);
            var data = (from ug in db.PGroup_Users
                        join g in db.PGroups on ug.IdUG equals g.IdGroup
                        join ga in db.PGroup_Roles on g.IdGroup equals ga.IdGR
                        //where ug.UserName == user.UserName
                        select new CredentialViewModel
                        {
                            //RoleID = a.RoleID,
                            //UserGroupID = a.UserGroupID
                        });

            return data.ToList();
        }




    }
}
