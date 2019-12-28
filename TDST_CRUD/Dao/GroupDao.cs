using System;
using System.Collections.Generic;
using System.Linq;
using TDST_CRUD.EF;
using System.Configuration;


namespace TDST_CRUD.Dao
{
    public class GroupDao
    {
        TDSTDbContext db = null;
        public GroupDao()
        {
            db = new TDSTDbContext();
        }

        public int Create(Group entity)
        {
            db.Groups.Add(entity);
            db.SaveChanges();
            return entity.IdGroup;
        }

        public List<Group> List()
        {
            return db.Groups.ToList();
        }







    }
}
