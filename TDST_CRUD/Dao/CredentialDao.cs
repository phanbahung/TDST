using Model.EF;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;

namespace Model.Dao
{
    public class CredentialDao
    {
        OnlineShopDbContext db = null;
        public CredentialDao()
        {
            db = new OnlineShopDbContext();
        }

        public IEnumerable<Credential> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Credential> model = db.Credentials;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.UserGroupID.Contains(searchString) || x.RoleID.Contains(searchString));
            }

            return model.OrderByDescending(x=>x.RoleID).ToPagedList(page, pageSize);
        }


       
    }
}
