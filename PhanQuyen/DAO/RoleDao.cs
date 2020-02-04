using System;
using System.Collections.Generic;
using System.Linq;
using TDST_CRUD.EF;
using System.Configuration;


namespace PhanQuyen.DAO
{
    public class RoleDao
    {
        TDSTDbContext db = null;
        public RoleDao()
        {
            db = new TDSTDbContext();
        }

        public List<PRole> Get_DS_Role()
        {
            return db.PRoles.ToList();
        }

        public void Truncate_RoleTabe()
        {
            db.Database.ExecuteSqlCommand("TRUNCATE TABLE PRoles");
            //db.SaveChanges();            
        }

        public long Insert(PRole entity)
        {
            db.PRoles.Add(entity);
            db.SaveChanges();
            return entity.IdRole;
        }
       
        

       
    }
}
