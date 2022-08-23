using System;
using Gob.Web.Configs;
using Gob.Web.Models;
using Gob.Web.Services.IServices;

namespace Gob.Web.Services
{
    public class ProductService : BaseService, IProductService
    {
        private readonly IHttpClientFactory _httpClient;

        public ProductService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
            _httpClient = httpClientFactory;
        }

        public async Task<T> CreateProductAsync<T>(ProductDto productDto)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiRequestType = StaticDetail.API_REQUEST_TYPE.POST,
                Data = productDto,
                Url = StaticDetail.PRODUCT_API_BASE + "/api/products",
                AccessToken = ""
            });
        }

        public async Task<T> DeleteProductAsync<T>(int id)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiRequestType = StaticDetail.API_REQUEST_TYPE.DELETE,
                Url = StaticDetail.PRODUCT_API_BASE + "/api/products/" + id,
                AccessToken = ""
            });
        }

        public async Task<T> GetAllProductsAsync<T>()
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiRequestType = StaticDetail.API_REQUEST_TYPE.GET,
                Url = StaticDetail.PRODUCT_API_BASE + "/api/products",
                AccessToken = ""
            });
        }

        public async Task<T> GetProductByIdAsync<T>(int id)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiRequestType = StaticDetail.API_REQUEST_TYPE.GET,
                Url = StaticDetail.PRODUCT_API_BASE + "/api/products/" + id,
                AccessToken = ""
            });
        }

        public async Task<T> UpdateProductAsync<T>(ProductDto productDto)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiRequestType = StaticDetail.API_REQUEST_TYPE.PUT,
                Data = productDto,
                Url = StaticDetail.PRODUCT_API_BASE + "/api/products",
                AccessToken = ""
            });
        }
    }
}

