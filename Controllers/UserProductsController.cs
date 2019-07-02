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
    [Route("api/my/products")]
    [ApiController]
    [Authorize]
    public class UserProductsController : BaseApiController
    {
        private readonly int _userId;
        private readonly IProductService _productService;

        public UserProductsController(IProductService productService)
        {
            _userId = GetUserId();
            _productService = productService;
        }

        [HttpGet]
        public ActionResult<ApiResponseModel<List<Product>>> Get()
        {
            var products = _productService.GetUserProducts(_userId);
            return Ok(new ApiResponseModel<List<Product>> { Data = products });
        }

        [HttpGet("{id}")]
        public ActionResult<ApiResponseModel<Entities.Product>> Get(int id)
        {
            var product = _productService.Get(id);
            return Ok(new ApiResponseModel<Entities.Product> { Data = product });
        }

        [HttpDelete("{id}")]
        public ActionResult<ApiResponseModel> Delete(int id)
        {
            _productService.Delete(id, _userId);
            return Ok(new ApiResponseModel());
        }

        [HttpPut("{id}")]
        public ActionResult<ApiResponseModel<Entities.Product>> Update(int id, [FromBody] ProductModel model)
        {
            var product = _productService.Update(id, _userId, model);
            return Ok(new ApiResponseModel<Entities.Product> { Data = product });
        }

        [HttpPost]
        public ActionResult<ApiResponseModel<Entities.Product>> Create([FromBody] ProductModel model)
        {
            var product = _productService.Create(_userId, model);
            return Ok(new ApiResponseModel<Entities.Product> { Data = product });
        }
    }
}