using System;
using AutoMapper;
using Gob.Services.ProductApi.DbContexts;
using Gob.Services.ProductApi.Models;
using Gob.Services.ProductApi.Models.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Gob.Services.ProductApi.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public ProductRepository(
            ApplicationDbContext dbContext,
            IMapper mapper,
            ILogger<ProductRepository> logger)
        {
            _db = dbContext;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ProductDto> CreateUpdateProduct(ProductDto productDto)
        {
            Product product = _mapper.Map<Product>(productDto);
            if (product.ProductId > 0)
            {
                _db.Products.Update(product);
            }
            else
            {
                _db.Products.Add(product);
            }
            await _db.SaveChangesAsync();
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<bool> DeleteProduct(int productId)
        {
            try
            {
                Product product = await _db.Products.FirstOrDefaultAsync(p => p.ProductId == productId);
                if (product == null)
                    return false;
                _db.Products.Remove(product);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured at product id {0} deletion." +
                    "\nRead stack for more info:\n {1}", productId, ex.Message);
                return false;
            }
        }

        public async Task<ProductDto> GetProductById(int productId)
        {
            Product product = await _db.Products.FirstOrDefaultAsync(x => x.ProductId == productId);
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            List<Product> products = await _db.Products.ToListAsync();
            return _mapper.Map<List<ProductDto>>(products);
        }
    }
}

