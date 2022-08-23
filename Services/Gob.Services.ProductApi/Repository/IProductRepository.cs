using System;
using Gob.Services.ProductApi.Models.Dtos;

namespace Gob.Services.ProductApi.Repository
{
	public interface IProductRepository
	{
		Task<IEnumerable<ProductDto>> GetProducts();
		Task<ProductDto> GetProductById(int productId);
		Task<ProductDto> CreateUpdateProduct(ProductDto productDto);
		Task<bool> DeleteProduct(int productId);
	}
}

