using System.Collections.Generic;
using System.Linq;
using farmapi.Context;
using farmapi.Entities;
using farmapi.Models;

namespace farmapi.Services
{
    public class ProductService : IProductService
    {
        private FarmApiContext _context;

        public ProductService(FarmApiContext context)
        {
            _context = context;
        }

        public Product Create(int owner, ProductModel model)
        {
            var product = new Product
            {
                UserId = owner,
                Name = model.Name,
                Price = model.Price
            };
            _context.Products.Add(product);
            _context.SaveChanges();
            return product;
        }

        public void Delete(int id, int owner)
        {
            var product = Get(id);
            if (product == null)
            {
                return;
            }

            if (product.UserId != owner)
            {
                throw new System.Exception("delete product operation not allowed");
            }

            product.Delete();
            _context.SaveChanges();
        }

        public Product Get(int id)
        {
            return _context.Products.SingleOrDefault(x => x.Id == id);
        }

        public List<Product> GetUserProducts(int owner)
        {
            return _context.Products.Where(x => x.UserId == owner).ToList();
        }
        public List<Product> GetOtherUserProducts(int owner)
        {
            return _context.Products.Where(x => x.UserId != owner).ToList();
        }

        public Product Update(int id, int owner, ProductModel model)
        {
            var product = Get(id);

            if (product == null)
            {
                return null;
            }

            if (product.UserId != owner)
            {
                throw new System.Exception("update product operation not allowed");
            }

            product.Name = model.Name;
            product.Price = model.Price;
            _context.SaveChanges();
            return product;
        }
    }
}