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
    /// <summary>
    /// Product listing
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : BaseApiController
    {

        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Products belongs to other users
        /// </summary>
        /// <returns>List of product</returns>
        [HttpGet]
        public ActionResult<ApiResponseModel<List<Product>>> Get()
        {
            var products = _productService.GetOtherUserProducts(GetUserId());
            return Ok(new ApiResponseModel<List<Product>> { Data = products });
        }
    }
}