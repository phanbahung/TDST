using System;
using System.Collections.Generic;
using System.Linq;
using TDST_CRUD.EF;
using TDST_CRUD.ViewModels;

namespace TDST_CRUD.Dao
{
    public class QLUserDao
    {
        TDSTDbContext db = null;
        public QLUserDao()
        {
            db = new TDSTDbContext();
        }

        public List<User> ListUser()
        {
            return db.Users.ToList();
        }

        public List<Role> LisAction()
        {
            return db.Roles.ToList();
        }

        public List<Group> ListGroup()
        {
            return db.Groups.ToList();
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
            var user = db.Users.Single(x => x.UserName == userName);
            var data = (from ug in db.User_Groups
                        join g in db.Groups on ug.IdUG equals g.IdGroup
                        join ga in db.Group_Roles on g.IdGroup equals ga.IdGR
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
