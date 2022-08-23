using System;
using static Gob.Web.Configs.StaticDetail;

namespace Gob.Web.Models
{
	public class ApiRequest
	{
		public API_REQUEST_TYPE ApiRequestType { get; set; } = API_REQUEST_TYPE.GET;
		public string Url { get; set; }
		public object Data { get; set; }
		public string AccessToken { get; set; }
	}
}