using System.Collections.Generic;
using farmapi.Entities;
using farmapi.Models;
using Microsoft.AspNetCore.Mvc;

namespace farmapi.Services
{
    public interface IProductService
    {
        Product Get(int id);
        void Delete(int id, int owner);
        Product Update(int id, int owner, Models.ProductModel model);
        Product Create(int owner, ProductModel model);
        List<Product> GetUserProducts(int owner);
        List<Product> GetOtherUserProducts(int owner);
    }
}