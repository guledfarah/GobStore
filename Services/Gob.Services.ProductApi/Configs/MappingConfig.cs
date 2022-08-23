using System;
using AutoMapper;
using Gob.Services.ProductApi.Models;
using Gob.Services.ProductApi.Models.Dtos;

namespace Gob.Services.ProductApi.Configs
{
	public class MappingConfig
	{
		public static MapperConfiguration RegisterMaps()
		{
			var mappingConfig = new MapperConfiguration(config =>
			{
				config.CreateMap<ProductDto, Product>().ReverseMap();
			});
			return mappingConfig;
		}
	}
}