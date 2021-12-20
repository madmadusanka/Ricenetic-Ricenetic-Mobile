using Graycorp.Mobile.Services;
using Graycorp.Mobile.Services.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using RiceneticMobile.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: Dependency(typeof(HttpRequestProviderService))]
namespace Graycorp.Mobile.Services
{
    public class HttpRequestProviderService : IHttpRequestProviderService
    {
        private readonly JsonSerializerSettings jsonSerializerSettings;

        #region Constructors & initialization
  

        public HttpRequestProviderService()
        {
            jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                DateTimeZoneHandling = DateTimeZoneHandling.Local,
                NullValueHandling = NullValueHandling.Ignore

            };

        }

        private void _constructor()
        {

        }
        #endregion

        #region Services
        #endregion

        #region Methods
        public async Task<T> GetAsync<T>(string url)
        {
            T result = default;

            try
            {
                HttpClient httpClient = await CreateHttpClientAsync(true);
                HttpResponseMessage response = await httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<T>(responseData, jsonSerializerSettings);
                    Console.Write(result);
                }
                else
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        //await authenticationService.Signout();
                        //if (await authenticationService.Authenticate())
                        //{
                        //    return await GetAsync<T>(url);
                        //}
                    }
                    else
                    {
                        Exception ex = new Exception(response.StatusCode.ToString());
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return result;
        }

        public async Task<ModelOutput> PostAsync(string url, object content, bool isAuthenticationRequired = false)
        {
            HttpResponseMessage response = null;
            ModelOutput output = new ModelOutput();


            try
            {
                HttpClient httpClient = await CreateHttpClientAsync(isAuthenticationRequired);
                if (!isAuthenticationRequired)
                {
                    string json = JsonConvert.SerializeObject(
                        content,
                        new JsonSerializerSettings()
                        {
                            ContractResolver = new CamelCasePropertyNamesContractResolver()
                        });
                    HttpContent httpContent = new StringContent(json);

                    response = await httpClient.PostAsync(url, httpContent);

                    if (response?.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        //if (await authenticationService.Authenticate())
                        //{
                        //    return await PostAsync(url, content);
                        //}
                        string responseData = await response.Content.ReadAsStringAsync();
                        output = JsonConvert.DeserializeObject<ModelOutput>(responseData, jsonSerializerSettings);
                    }
                }
                else
                {
       
                }
            }
            catch (Exception ex)
            {
                //throw;
                return output;
            }
            
            return output;
        }

        //public async Task<AuthResponseModel> LoginRequest(string url, AuthModel authModel )
        //{
        //    AuthResponseModel result = null;

        //    try
        //    {
        //        HttpClient httpClient = await CreateHttpClientAsync(false);
        //        string param = Constants.ClientIdKey + "=" + AppConfigs.ClientId + "&" + Constants.ClientSecretKey + "=" + AppConfigs.ClientSecret + "&" +Constants.GrantTypeKey+"="+Constants.PasswordKey +"&" + Constants.Usernamekey + "=" + authModel.UserName +"&"+ Constants.PasswordKey +"="+ authModel.Password ;
        //        var stringContent = new StringContent(param, Encoding.UTF8, "application/x-www-form-urlencoded");
        //        HttpResponseMessage response = await httpClient.PostAsync(url, stringContent);

        //        if (response.IsSuccessStatusCode)
        //        {
        //            string responseData = await response.Content.ReadAsStringAsync();
        //            result = JsonConvert.DeserializeObject<AuthResponseModel>(responseData, jsonSerializerSettings);
        //        }

        //    }
        //    catch (Exception) 
        //    {
        //        throw;
        //    }
        //    return result;
        //}

        public async Task<HttpResponseMessage> PutAsync(string url, object content)
        {
            HttpResponseMessage response = null;

            try
            {
                HttpClient httpClient = await CreateHttpClientAsync(true);
                string json = JsonConvert.SerializeObject(
                    content,
                    new JsonSerializerSettings()
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    });

                HttpContent httpContent = new StringContent(json);

                response = await httpClient.PutAsync(url, httpContent);

                if (response?.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    //if ()
                    //{
                    //    return await PostAsync(url, content);
                    //}
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return response;
        }

        private async Task<HttpClient> CreateHttpClientAsync(bool isAuthenticationRequired)
        {
            HttpClient httpClient = null;
            if (isAuthenticationRequired)
            {
                HttpClientHandler httpClientHandler = new HttpClientHandler();
                httpClient = new HttpClient(httpClientHandler);
                //httpClient.BaseAddress = new Uri(AppConfigs.BaseAPIUrl);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //httpClient.DefaultRequestHeaders.Add(Constants.TenantKey, AppConfigs.TenantValue);
                //string authToken = await SecureStorage.GetAsync(Constants.AuthTokenKey);
                //string tokenType = await SecureStorage.GetAsync(Constants.TokenType);
                //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.token);

            }
            else
            {
                HttpClientHandler httpClientHandler = new HttpClientHandler();
                httpClient = new HttpClient(httpClientHandler);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            }
            //httpClient.BaseAddress = new Uri(AppConfigs.BaseAPIUrl);
            return httpClient;
        }
        #endregion
    }
}

