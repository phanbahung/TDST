﻿using System;
using System.Collections.Generic;
using System.Linq;
using TDST_CRUD.EF;
using PhanQuyen.Models;


namespace PhanQuyen.DAO
{
    public class Group_UserDao
    {
        TDSTDbContext db = null;
        public Group_UserDao()
        {
            db = new TDSTDbContext();
        }

        public List<PUser> ListUser()
        {
            return db.PUsers.ToList();
        }       

        public List<PGroup> ListGroup()
        {
            return db.PGroups.ToList();
        }

        #region Quản lý GroupUser

        public List<GroupUser_ViewModel> ListUser_ByGroup(int idGroup)
        {          

            var nk = (from u in db.PGroup_Users
                      where (u.IdGroup == idGroup)
                      select new
                      {
                          IdGroup = u.IdGroup,
                          UserName = u.UserName,
                          IdGU = u.IdGU,
                      }).ToList()
                      .Select(x => new GroupUser_ViewModel()
                      {
                          IdGroup = x.IdGroup,
                          UserName = x.UserName,
                          IdGU = x.IdGU
                      });

            return nk.ToList();
        }

        public int RemoveUserFromGroup (string userName)
        {
            PGroup_Users entity = db.PGroup_Users.Where(x => x.UserName == userName).SingleOrDefault();
            db.PGroup_Users.Remove(entity);
            db.SaveChanges();
           return 0;
        }
     

        public string AddUserToGroup(PGroup_Users entity)
        {
            string ketQua = "";
            // B1-- Không tồn tại user này
            int countUser = db.PUsers.Where(x => x.UserName == entity.UserName).ToList().Count();
            if (countUser>0)
            {
                // 2-- Đã có trong nhóm hay chưa
                countUser = db.PGroup_Users.Where(x => x.UserName == entity.UserName).ToList().Count();
                if (countUser == 0)
                {
                    db.PGroup_Users.Add(entity);
                    db.SaveChanges();
                    ketQua = "0";
                }
                else ketQua = "Đã có user này trong nhóm!";
            }
            else ketQua = "Không tồn tại user này!";

            return ketQua;// insertedRecords);
        }

        #endregion Quản lý GroupUser
        

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
                        join g in db.PGroups on ug.IdGU equals g.IdGroup
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