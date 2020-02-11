using System;
using System.Collections.Generic;
using System.Linq;
using TDST_CRUD.EF;
using System.Configuration;


namespace PhanQuyen.DAO
{
    public class GroupDao
    {
        TDSTDbContext db = null;
        public GroupDao()
        {
            db = new TDSTDbContext();
        }
      

        public string NewGroup(string groupName)
        {
            string ketQua = "";
            PGroup entity = new PGroup();
            entity.GroupName = groupName;
            int result = db.PGroups.Where(x => x.GroupName == entity.GroupName).ToList().Count();
            if (result ==0)
            {
                db.PGroups.Add(entity);
                db.SaveChanges();
                ketQua = entity.IdGroup.ToString();
            }
            else ketQua = "Trùng tên group";
            
            return ketQua;
        }
       
        

       
    }
}
