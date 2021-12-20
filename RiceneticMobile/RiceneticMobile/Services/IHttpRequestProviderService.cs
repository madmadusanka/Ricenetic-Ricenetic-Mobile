using Newtonsoft.Json;
using RiceneticMobile.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Graycorp.Mobile.Services.Interfaces
{
    public interface IHttpRequestProviderService
    {
        Task<T> GetAsync<T>(string url);
        Task<ModelOutput> PostAsync(string url, object content, bool isAuthenticationRequired = true);
        Task<HttpResponseMessage> PutAsync(string url, object content);
        //Task<AuthResponseModel> LoginRequest(string url, AuthModel authModel);
    }
}
