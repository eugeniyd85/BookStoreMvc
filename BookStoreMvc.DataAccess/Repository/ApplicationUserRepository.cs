using BookStoreMvc.DataAccess.Data;
using BookStoreMvc.DataAccess.Repository.IRepository;
using BookStoreMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreMvc.DataAccess.Repository
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private ApplicationDbContext _db;
        //spend more time to understand this
        public ApplicationUserRepository(ApplicationDbContext db) : base(db) // clarify meaning of base(db) here
        {
            _db = db;
        }
    }
}
