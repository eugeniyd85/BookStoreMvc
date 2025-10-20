using BookStoreMvc.DataAccess.Data;
using BookStoreMvc.DataAccess.Repository.IRepository;
using BookStoreMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreMvc.DataAccess.Repository
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        private ApplicationDbContext _db;

        public CompanyRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Company obj)
        {
            _db.Companies.Update(obj);

            //var objFromDb = _db.Companies.FirstOrDefault(u => u.Id == obj.Id);
            //if (objFromDb != null)
            //{
            //    objFromDb.Name = obj.Name;
            //    objFromDb.StreetAddress = obj.StreetAddress;
            //    objFromDb.City = obj.City;
            //    objFromDb.State = obj.State;
            //    objFromDb.PostalCode = obj.PostalCode;
            //    objFromDb.PhoneNumber = obj.PhoneNumber;
            //}
        }
    }
}
