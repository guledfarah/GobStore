using System;
using Gob.Web.Configs;
using Gob.Web.Models;
using Gob.Web.Services.IServices;

namespace Gob.Web.Services
{
    public class ProductService : BaseService, IProductService
    {
        private readonly IHttpClientFactory _httpClient;
        private IProductService _productServiceImplementation;

        public ProductService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
            _httpClient = httpClientFactory;
        }

        public async Task<T> CreateProductAsync<T>(ProductDto productDto, string accessToken)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiRequestType = StaticDetail.API_REQUEST_TYPE.POST,
                Data = productDto,
                Url = StaticDetail.PRODUCT_API_BASE + "/api/products",
                AccessToken = accessToken
            });
        }

        public async Task<T> DeleteProductAsync<T>(int id, string accessToken)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiRequestType = StaticDetail.API_REQUEST_TYPE.DELETE,
                Url = StaticDetail.PRODUCT_API_BASE + "/api/products/" + id,
                AccessToken = accessToken
            });
        }

        public async Task<T> GetAllProductsAsync<T>(string accessToken)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiRequestType = StaticDetail.API_REQUEST_TYPE.GET,
                Url = StaticDetail.PRODUCT_API_BASE + "/api/products",
                AccessToken = accessToken
            });
        }

        public async Task<T> GetProductByIdAsync<T>(int id, string accessToken)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiRequestType = StaticDetail.API_REQUEST_TYPE.GET,
                Url = StaticDetail.PRODUCT_API_BASE + "/api/products/" + id,
                AccessToken = accessToken
            });
        }

        public async Task<T> UpdateProductAsync<T>(ProductDto productDto, string accessToken)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiRequestType = StaticDetail.API_REQUEST_TYPE.PUT,
                Data = productDto,
                Url = StaticDetail.PRODUCT_API_BASE + "/api/products",
                AccessToken = accessToken
            });
        }
    }
}

