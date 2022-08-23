using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gob.Web.Models;
using Gob.Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Gob.Web.Controllers
{
    public class ProductController : Controller
    {
        private IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            List<ProductDto> products = new();
            var response = await _productService.GetAllProductsAsync<ResponseDto>();
            if(response != null && response.IsSuccess)
                products = JsonConvert.DeserializeObject<List<ProductDto>>(response.Result.ToString());
            return View(products);
        }

        public IActionResult DeleteProduct()
        {
            throw new NotImplementedException();
        }

        public IActionResult ManageProduct(int? productId)
        {
            throw new NotImplementedException();
        }
    }
}

