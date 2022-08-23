﻿using System;
using Gob.Web.Models;

namespace Gob.Web.Services.IServices
{
	public interface IProductService : IBaseService
	{
		Task<T> GetAllProductsAsync<T>();
        Task<T> GetProductByIdAsync<T>(int id);
        Task<T> CreateProductAsync<T>(ProductDto productDto);
        Task<T> UpdateProductAsync<T>(ProductDto productDto);
        Task<T> DeleteProductAsync<T>(int id);
    }
}