using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using farmapi.Entities;
using farmapi.Models;
using farmapi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace farmapi.Controllers
{
    [Route("api/My/Products")]
    [ApiController]
    [Authorize]
    public class UserProductsController : BaseApiController
    {
        private readonly IProductService _productService;

        public UserProductsController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Get products which belongs to authenticated user 
        /// </summary>
        [HttpGet]
        public ActionResult<ApiResponseModel<List<Product>>> Get()
        {
            var products = _productService.GetUserProducts(GetUserId());
            return Ok(new ApiResponseModel<List<Product>> { Data = products });
        }

        /// <summary>
        /// Get a product which belongs to authenticated user  
        /// </summary>
        /// <param name="id">Product Id</param>

        [HttpGet("{id}")]
        public ActionResult<ApiResponseModel<Entities.Product>> Get(int id)
        {
            var product = _productService.Get(id);
            if (product.UserId != GetUserId())
            {
                throw new Exception("Product belongs to another user");
            }
            return Ok(new ApiResponseModel<Entities.Product> { Data = product });
        }
        /// <summary>
        /// Delete product which belongs to authenticated user  
        /// </summary>
        /// <param name="id">Product ID</param>        
        [HttpDelete("{id}")]
        public ActionResult<ApiResponseModel> Delete(int id)
        {
            _productService.Delete(id, GetUserId());
            return Ok(new ApiResponseModel());
        }
        /// <summary>
        /// Update product which belongs to authenticated user  
        /// </summary>
        /// <param name="id">Product Id</param>       
        /// <param name="model">Product</param>       
        [HttpPut("{id}")]
        public ActionResult<ApiResponseModel<Entities.Product>> Update(int id, [FromBody] ProductModel model)
        {
            var product = _productService.Update(id, GetUserId(), model);
            return Ok(new ApiResponseModel<Entities.Product> { Data = product });
        }
        /// <summary>
        /// Creates new product
        /// </summary>
        /// <param name="model">Product</param>       
        [HttpPost]
        public ActionResult<ApiResponseModel<Entities.Product>> Create([FromBody] ProductModel model)
        {
            var product = _productService.Create(GetUserId(), model);
            return Ok(new ApiResponseModel<Entities.Product> { Data = product });
        }
    }
}