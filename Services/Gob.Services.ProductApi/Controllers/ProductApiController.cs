using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Gob.Services.ProductApi.Configs;
using Gob.Services.ProductApi.Models;
using Gob.Services.ProductApi.Models.Dtos;
using Gob.Services.ProductApi.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Gob.Services.ProductApi.Controllers
{
    [Authorize]
    [Route("api/products")]
    public class ProductApiController : ControllerBase
    {
        private readonly ResponseDto _response;
        private readonly IProductRepository _productRepository;
        private readonly ILogger _logger;

        public ProductApiController(
            IProductRepository productRepository,
            ILogger<ProductApiController> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
            this._response = new ResponseDto();
        }

        [HttpGet]
        public async Task<ResponseDto> Get()
        {
            try
            {
                IEnumerable<ProductDto> productDtos = await _productRepository.GetProducts();
                _response.Result = productDtos;
            }
            catch (Exception ex)
            {
                var errorMessage = "Error occured while adding products" +
                   $"\nRead stack for more info:\n {ex.Message}";
                _response.IsSuccess = false;
                _response.ErrorMessages =
                    new List<string>() { errorMessage };
                _logger.LogError(errorMessage);
            }
            return _response;
        }

        [HttpGet]
        [Route("{productId}")]
        public async Task<ResponseDto> Get(int productId)
        {
            try
            {
                ProductDto productDto = await _productRepository.GetProductById(productId);
                _response.Result = productDto;
            }
            catch (Exception ex)
            {
                var errorMessage = $"Error occured while getting the product with id: {productId.ToString()}" +
                  $"\nRead stack for more info:\n {ex.Message}";
                _response.IsSuccess = false;
                _response.ErrorMessages =
                    new List<string>() { errorMessage };
                _logger.LogError(errorMessage);
            }
            return _response;
        }


        [HttpPost]
        public async Task<ResponseDto> Post([FromBody] ProductDto productDto)
        {
            try
            {
                ProductDto addedProduct = await _productRepository.CreateUpdateProduct(productDto);
                _response.Result = addedProduct;
            }
            catch (Exception ex)
            {
                var errorMessage = $"Error occured while adding a new product with name: {productDto.Name.ToString()}" +
                    $"\nRead stack for more info:\n {ex.Message}";
                _response.IsSuccess = false;
                _response.ErrorMessages =
                    new List<string>() { errorMessage };
                _logger.LogError(errorMessage);
            }
            return _response;
        }

        [HttpPut]
        public async Task<ResponseDto> Put([FromBody] ProductDto productDto)
        {
            try
            {
                ProductDto updatedProduct = await _productRepository.CreateUpdateProduct(productDto);
                _response.Result = updatedProduct;
            }
            catch (Exception ex)
            {
                var errorMessage = $"Error occured while updating the product with name: {productDto.Name.ToString()}" +
                    $"\nRead stack for more info:\n {ex.Message}";
                _response.IsSuccess = false;
                _response.ErrorMessages =
                    new List<string>() { errorMessage };
                _logger.LogError(errorMessage);
            }
            return _response;
        }

        [Authorize(Roles = StaticDetail.Admin)]
        [HttpDelete] 
        [Route("{productId}")]
        public async Task<ResponseDto> Delete(int productId)
        {
            try
            {
                var isDeletionSuccess = await _productRepository.DeleteProduct(productId);
                _response.Result = isDeletionSuccess;
            }
            catch (Exception ex)
            {
                var errorMessage = $"Error occured while deleting the product with id: {productId.ToString()}" +
                    $"\nRead stack for more info:\n {ex.Message}";
                _response.IsSuccess = false;
                _response.ErrorMessages =
                    new List<string>() { errorMessage };
                _logger.LogError(errorMessage);
            }
            return _response;
        }
    }
}

