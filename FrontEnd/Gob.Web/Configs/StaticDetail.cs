using System;
namespace Gob.Web.Configs
{
	public static class StaticDetail
	{
		public static string PRODUCT_API_BASE { get; set; }
		public enum API_REQUEST_TYPE
		{
			GET,
			POST,
			PUT,
			DELETE
		}
	}
}