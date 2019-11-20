using Model.EF;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Model.Dao
{
    public class UserGroupDao
    {
        TDSTDbContext db = null;
        public UserGroupDao()
        {
            db = new OnlineShopDbContext();
        }

        public IEnumerable<UserGroup> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<UserGroup> model = db.UserGroups;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.ID.Contains(searchString) || x.Name.Contains(searchString));
            }

            return model.OrderBy(x=>x.ID).ToPagedList(page, pageSize);
        }

        public bool Update(UserGroup entity)
        {
            try
            {
                var ug = db.UserGroups.Find(entity.ID);
                ug.Name = entity.Name;
                ug.Roles = entity.Roles;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                //logging
                return false;
            }

        }

        public UserGroup DetailById (string Id)
        {
            return db.UserGroups.Find(Id);
        }

        //public long Insert(Category category)
        //{
        //    db.Categories.Add(category);
        //    db.SaveChanges();
        //    return category.ID;
        //}
        //public List<Category> ListAll()
        //{
        //    return db.Categories.Where(x => x.Status == true).ToList();
        //}
        //public ProductCategory ViewDetail(long id)
        //{
        //    return db.ProductCategories.Find(id);
        //}
    }
}
