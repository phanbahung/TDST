using System;
using System.Collections.Generic;
using System.Linq;
using TDST_CRUD.EF;
using PhanQuyen.Models;
//using PagedList;
using System.Configuration;

using PhanQuyen.Common;


namespace PhanQuyen.DAO
{
    public class UserDao
    {
        TDSTDbContext db = null;
        public UserDao()
        {
            db = new TDSTDbContext();
        }


        public string NewUser(string userName, string fullName)
        {
            string ketQua = "";
            PUser entity = new PUser();
            entity.UserName = userName;
            entity.FullName = fullName;
            entity.Status = true;
            entity.Password = Encryptor.MD5Hash(userName.Trim());
            int result = db.PUsers.Where(x => x.UserName == entity.UserName).ToList().Count();
            if (result == 0)
            {
                db.PUsers.Add(entity);
                db.SaveChanges();
                ketQua = entity.IdUser.ToString();
            }
            else ketQua = "Trùng user name";

            return ketQua;
        }

        public bool UpdateUser(PUser entity)
        {          
            var user = db.PUsers.Find(entity.IdUser);
            user.FullName = entity.FullName;
            user.Status = entity.Status;
            db.SaveChanges();
            return true; 
        }

        public bool CheckPassword(string userName, string password)
        {
            return db.PUsers.Count(x => x.UserName == userName && x.Password == password) > 0;
        }

        public bool ChangePass(string userName, string newPass)
        {
            try
            {
                var user = db.PUsers.SingleOrDefault(x => x.UserName == userName);
                user.Password = newPass;
                //user.ModifiedDate = DateTime.Now;
                //user.ModifiedBy = userName;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                //logging
                return false;
            }
        }
      

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

        public List<PUser> GetByIdUser(int idUser)      
        {
            var nk = (from u in db.PUsers
                      where (u.IdUser == idUser)
                      select new
                      {
                          IdUser = u.IdUser,
                          UserName = u.UserName,
                          FullName = u.FullName,
                          Status = u.Status
                      })
                            .ToList()
                            .Select(x => new PUser()
                            {
                                IdUser = x.IdUser,
                                UserName = x.UserName != null ? x.UserName.Trim() : string.Empty,
                                FullName = x.FullName != null ? x.FullName.Trim() : string.Empty,
                                Status = x.Status == null ? false: x.Status ,                                
                            });

            return nk.ToList();


        }
        //public long GetIdByUserName(string userName)
        //{
        //    return db.Users.SingleOrDefault(x => x.UserName == userName).Id;
        //}     


        public List<RoleAndZone_Model> GetListCredential_By_UserName(string userName)
        {
            var user = db.PUsers.Single(x => x.UserName == userName);
            var data = (from g in db.PGroups
                        join gu in db.PGroup_Users on g.IdGroup equals gu.IdGroup
                        join gr in db.PGroup_Roles on gu.IdGroup equals gr.IdGroup
                        where gu.UserName == user.UserName                     
                        select new 
                        {                             
                            RoleName = gr.RoleName,
                            ZoneData = g.DataZone                          
                        })
                         .ToList()
                            .Select(x => new RoleAndZone_Model()
                            {
                                RoleName = x.RoleName,
                                ZoneData = x.ZoneData                              
                            }); ;
            
            return data.ToList();
        }
                


        public int Login(string userName, string passWord, bool isLoginAdmin = false)
        {
            var result =   db.PUsers.SingleOrDefault(x => x.UserName == userName);

            if (result != null)
            {
                if (result.Password!= passWord.Trim())
                {
                    return PConstants.LOGIN_PASS_WRONG;
                }
                else if (result.Status == false)
                {
                    return PConstants.LOGIN_USER_LOCKED;
                }
                else return PConstants.LOGIN_SUCCESS;    
            }
                  
                return PConstants.LOGIN_USER_NOT_EXIST;

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
        

       
    }
}
