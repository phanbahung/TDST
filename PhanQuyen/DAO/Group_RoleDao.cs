using System;
using System.Collections.Generic;
using System.Linq;
using TDST_CRUD.EF;
using PhanQuyen.Models;


namespace PhanQuyen.DAO
{
    public class Group_RoleDao
    {
        TDSTDbContext db = null;
        public Group_RoleDao()
        {
            db = new TDSTDbContext();
        }     

        public List<PRole> ListRoles()
        {
            return db.PRoles.ToList();
        }


        #region Quản lý GroupRole

        public List<GroupRole_ViewModel> ListRole_ByGroup(int idGroup)
        {

            var nk = (from u in db.PGroup_Roles
                      where (u.IdGroup == idGroup)
                      select new
                      {
                          IdGroup = u.IdGroup,
                          RoleName = u.RoleName,
                          IdGR = u.IdGR,
                      }).ToList()
                      .Select(x => new GroupRole_ViewModel()
                      {
                          IdGroup = x.IdGroup,
                          RoleName = x.RoleName,
                          IdGR = x.IdGR
                      });

            return nk.ToList();
        }

        public int RemoveRoleFromGroup(string RoleName)
        {
            PGroup_Roles entity = db.PGroup_Roles.Where(x => x.RoleName == RoleName).SingleOrDefault();
            db.PGroup_Roles.Remove(entity);
            db.SaveChanges();
            return 0;
        }


        public string AddRoleToGroup(PGroup_Roles entity)
        {
            string ketQua = "";
            // B1-- Không tồn tại Role này
            int countRole = db.PRoles.Where(x => x.RoleName == entity.RoleName).ToList().Count();
            if (countRole > 0)
            {
                // 2-- Đã có trong nhóm hay chưa
                countRole = db.PGroup_Roles.Where(x => x.RoleName == entity.RoleName&x.IdGroup==entity.IdGroup).ToList().Count();
                if (countRole == 0)
                {
                    db.PGroup_Roles.Add(entity);
                    db.SaveChanges();
                    ketQua = entity.IdGR.ToString();
                }
                else ketQua = "Đã có Role này trong nhóm!";
            }
            else ketQua = "Không tồn tại Role này!";

            return ketQua;// insertedRecords);
        }

        #endregion Quản lý GroupRole



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
