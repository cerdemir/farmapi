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
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : BaseApiController
    {
        private readonly int _userId;
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _userId = GetUserId();
            _productService = productService;
        }

        [HttpGet]
        public ActionResult<ApiResponseModel<List<Product>>> Get()
        {
            var products = _productService.GetOtherUserProducts(_userId);
            return Ok(new ApiResponseModel<List<Product>> { Data = products });
        }
    }
}