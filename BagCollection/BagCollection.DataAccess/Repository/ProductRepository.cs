using BagCollection.DataAccess.Data;
using BagCollection.DataAccess.Repository.IRepository;
using BagCollection.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BagCollection.DataAccess.Repository
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
            var objFromDb = _db.Products.FirstOrDefault(u => u.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.Title = obj.Title;
                objFromDb.Description = obj.Description;
                objFromDb.ListPrice = obj.ListPrice;
                objFromDb.LowPrice = obj.LowPrice;
                objFromDb.Color = obj.Color;
                objFromDb.Brand = obj.Brand;
                objFromDb.CategoryId = obj.CategoryId;
                if (obj.ImageUrl != null)
                {
                    objFromDb.ImageUrl = obj.ImageUrl;
                }
                if (obj.ImageUrl1 != null)
                {
                }
                if (obj.ImageUrl2 != null)
                {
                    objFromDb.ImageUrl2 = obj.ImageUrl2;
                }
                if (obj.ImageUrl3 != null)
                {
                    objFromDb.ImageUrl3 = obj.ImageUrl3;
                }
                if (obj.ImageUrl4 != null)
                {
                    objFromDb.ImageUrl4 = obj.ImageUrl4;
                }
            }
        }
    }

}
