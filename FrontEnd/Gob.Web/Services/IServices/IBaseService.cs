using System;
using Gob.Web.Models;

namespace Gob.Web.Services.IServices
{
	public interface IBaseService : IDisposable
	{
		Task<T> SendAsync<T>(ApiRequest apiRequest);
	}
}

