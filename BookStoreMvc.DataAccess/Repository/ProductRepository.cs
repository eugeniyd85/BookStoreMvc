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
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Product obj)
        {
            _db.Products.Update(obj);
        }
    }
}
