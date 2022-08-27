using System;
using System.Net.Http.Headers;
using System.Text;
using Gob.Web.Configs;
using Gob.Web.Models;
using Gob.Web.Services.IServices;
using Newtonsoft.Json;

namespace Gob.Web.Services
{
    public class BaseService : IBaseService
    {
        public IHttpClientFactory httpClient { get; set; }

        public BaseService(IHttpClientFactory httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<T> SendAsync<T>(ApiRequest apiRequest)
        {
            try
            {
                var client = httpClient.CreateClient("GobAPI");
                HttpRequestMessage requestMessage = new HttpRequestMessage();
                requestMessage.Headers.Add("Accept", "application/json");
                requestMessage.RequestUri = new Uri(apiRequest.Url);

                client.DefaultRequestHeaders.Clear();

                if(apiRequest.Data != null)
                {
                    requestMessage.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data),
                        Encoding.UTF8, "application/json");
                }
                
                // Add the access_token if it exist
                if (!string.IsNullOrEmpty(apiRequest.AccessToken))
                    client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", apiRequest.AccessToken);

                HttpResponseMessage responseMessage = null;

                switch (apiRequest.ApiRequestType)
                {
                    case StaticDetail.API_REQUEST_TYPE.POST:
                        requestMessage.Method = HttpMethod.Post;
                        break;
                    case StaticDetail.API_REQUEST_TYPE.PUT:
                        requestMessage.Method = HttpMethod.Put;
                        break;
                    case StaticDetail.API_REQUEST_TYPE.DELETE:
                        requestMessage.Method = HttpMethod.Delete;
                        break;
                    default:
                        requestMessage.Method = HttpMethod.Get;
                        break;
                }

                responseMessage = await client.SendAsync(requestMessage);
                var responseMessageContent = await responseMessage.Content.ReadAsStringAsync();
                var responseDto = JsonConvert.DeserializeObject<T>(responseMessageContent);
                return responseDto;
            }
            catch (Exception ex)
            {
                ResponseDto errorDto = new ResponseDto
                {
                    DisplayMessage = "Error",
                    IsSuccess = false,
                    ErrorMessages = new List<string> { ex.Message }
                };
                return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(errorDto));
            }
        }

        public void Dispose()
        {
            // GC.SuppressFinalize(this);
        }
    }
}

