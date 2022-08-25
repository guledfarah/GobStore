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
        
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            ProductDto product = new();
            var response = await _productService.GetProductByIdAsync<ResponseDto>(productId);
            if(response is {IsSuccess: true})
                product = JsonConvert.DeserializeObject<ProductDto>(response.Result.ToString());
            return View(product);
        }
        
        [HttpPost]
        public async Task<IActionResult> DeleteProduct(ProductDto productDto)
        {
            var response = await _productService.DeleteProductAsync<ResponseDto>(productDto.ProductId);
            if(response is { IsSuccess: true })
                return RedirectToAction("Index");
            return View(productDto);
        }

        public async Task<IActionResult> ManageProduct(int productId = 0)
        {
            ProductDto product = new();
            
            if (productId == 0)
                return View(product);
            
            var response = await _productService.GetProductByIdAsync<ResponseDto>(productId);
            if(response is {IsSuccess: true})
                product = JsonConvert.DeserializeObject<ProductDto>(response.Result.ToString());
            return View(product);
        }
        
        [HttpPost]
        public async Task<IActionResult> ManageProduct(ProductDto productDto)
        {
            if (productDto.ProductId != 0)
            {
                var response = await _productService.UpdateProductAsync<ResponseDto>(productDto);
                if(response is { IsSuccess: true })
                    return RedirectToAction("Index");
                return View(productDto);
            }
            else
            {
                var response = await _productService.CreateProductAsync<ResponseDto>(productDto);
                if(response is { IsSuccess: true })
                    return RedirectToAction("Index");
                return View(productDto);
            }
        }
    }
}

