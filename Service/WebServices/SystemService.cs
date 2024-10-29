﻿
using CGMWPF.Model;
using CGMWPF.Service;
using Model;
using Newtonsoft.Json;
using Service.CommonInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Service.WebServices
{
    public class SystemService 
    {
        public SystemService() 
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(iAppSettings.ApiEndpoint + CTRL_NAME);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add(Constant.HTTP_HEADER_APITOKEN, iAppSettings.ApiToken);
        }
        /// <summary>
        /// Define httpclient
        /// </summary>
        private HttpClient httpClient;

        /// <summary>
        /// Define ctrl_name
        /// </summary>
        private const string CTRL_NAME = "system";

        public async Task<User?> LoginAsync(string email, string password)
        {
            string result = string.Empty;
            try
            {
                User userData = new User();
                userData.Email = email;
                userData.Password = password;
                var content = new StringContent(JsonConvert.SerializeObject(userData), Encoding.UTF8, @"application/json");
                HttpResponseMessage responseMessage = await httpClient.PostAsync(httpClient.BaseAddress + "/login", content);
                responseMessage.EnsureSuccessStatusCode();
                result = await responseMessage.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException ex)
            {
                iAppSettings.Log.Logger.Error(ex.Message);
                return new User();
            }
            catch (Exception ex)
            {
                iAppSettings.Log.Logger.Error(ex.Message);
                return new User();
            }
            if (string.IsNullOrEmpty(result))
            {
                return new User();
            }
            return (User?)JsonConvert.DeserializeObject(result, typeof(User));
        }
        /// <summary>
        /// Dispose httpclient
        /// </summary>
        public void Dispose()
        {
            httpClient.Dispose();
        }
    }
}
