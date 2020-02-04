using System;
using System.Collections.Generic;
using System.Linq;
using TDST_CRUD.EF;
using PhanQuyen.Models;
//using PagedList;
using System.Configuration;

//using Common;


namespace PhanQuyen.DAO
{
    public class UserDao
    {
        TDSTDbContext db = null;
        public UserDao()
        {
            db = new TDSTDbContext();
        }

        //public long Insert(User entity)
        //{
        //    db.Users.Add(entity);
        //    db.SaveChanges();
        //    return entity.Id;
        //}

        //public bool Update(User entity)
        //{
        //    try
        //    {
        //        var user = db.Users.Find(entity.Id);

        //        if (!string.IsNullOrEmpty(entity.Password))
        //        {
        //            user.Password = entity.Password;
        //        }

        //        db.SaveChanges();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        //logging
        //        return false;
        //    }

        //}

        //public bool ChangePass(string userName, string newPass)
        //{
        //    try
        //    {
        //        var user = db.Users.SingleOrDefault(x=>x.UserName==userName);                
        //        user.Password = newPass;

        //        db.SaveChanges();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        //logging
        //        return false;
        //    }

        //}

        //public IEnumerable<User> ListAllPaging(string searchString, int page, int pageSize)
        //{
        //    IQueryable<User> model = db.Users;
        //    if (!string.IsNullOrEmpty(searchString))
        //    {
        //        model = model.Where(x => x.UserName.Contains(searchString) || x.Name.Contains(searchString));
        //    }

        //    return model.OrderByDescending(x => x.Id).ToPagedList(page, pageSize);
        //}

        public PUser GetByUserName(string userName)
        {
            return db.PUsers.SingleOrDefault(x => x.UserName == userName);
        }

        //public long GetIdByUserName(string userName)
        //{
        //    return db.Users.SingleOrDefault(x => x.UserName == userName).Id;
        //}
        //public User ViewDetail(int id)
        //{
        //    return db.Users.Find(id);
        //}


        public List<CredentialViewModel> GetListCredential_By_UserName(string userName)
        {
            var user = db.PUsers.Single(x => x.UserName == userName);
            var data = (from ug in db.PGroup_Users
                        join g in db.PGroups on ug.IdGU equals g.IdGroup
                        join ga in db.PGroup_Roles on g.IdGroup equals ga.IdGR
                        where ug.UserName == user.UserName
                        select new CredentialViewModel
                        {
                            //RoleID = a.RoleID,
                            //UserGroupID = a.UserGroupID
                        });
            
            return data.ToList();
        }


        //public List<string> GetListCredential_Backup(string userName)
        //{
        //    var user = db.Users.Single(x => x.UserName == userName);
        //    var data = (from ug in db.User_Groups
        //                join g in db.Groups on ug.IdUG equals g.IdGroup
        //                join ga in db.Group_Actions on g.IdGroup equals ga.IdGA
        //                where ug.UserName == user.UserName
        //                select new
        //                {
        //                    RoleID = a.RoleID,
        //                    UserGroupID = a.UserGroupID
        //                }).AsEnumerable().Select(x => new Credential()
        //                {
        //                    RoleID = x.RoleID,
        //                    UserGroupID = x.UserGroupID
        //                });
        //    List<string> meomeo = data.Select(x => x.RoleID).ToList();
        //    return data.Select(x => x.RoleID).ToList();
        //}


        public int Login(string userName, string passWord, bool isLoginAdmin = false)
        {
            var result =   db.PUsers.SingleOrDefault(x => x.UserName == userName);
            return 1;                          
        }

        //public bool ChangeStatus(long id)
        //{
        //    var user = db.Users.Find(id);            

        //    user.Status = !user.Status;
        //    db.SaveChanges();
        //    return user.Status;
        //}

        public bool Delete(int id)
        {
            try
            {
                var user = db.PUsers.Find(id);
                db.PUsers.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool CheckUserName(string userName)
        {
            return db.PUsers.Count(x => x.UserName == userName) > 0;
        }

        public bool CheckPassword(string userName, string password)
        {            
            return db.PUsers.Count(x => x.UserName == userName &&x.Password==password) > 0;
        }

       
    }
}
