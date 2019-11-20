using System;
using System.Collections.Generic;
using System.Linq;
using TDST_CRUD.EF;
using System.Configuration;


namespace TDST_CRUD.Dao
{
    public class RoleDao
    {
        TDSTDbContext db = null;
        public RoleDao()
        {
            db = new TDSTDbContext();
        }

        public List<Role> Get_DS_Role()
        {
            return db.Roles.ToList();
        }

        public void Truncate_RoleTabe()
        {
            db.Database.ExecuteSqlCommand("TRUNCATE TABLE Roles");
            //db.SaveChanges();            
        }

        public long Insert(Role entity)
        {
            db.Roles.Add(entity);
            db.SaveChanges();
            return entity.IdRole;
        }
       
        

       
    }
}
