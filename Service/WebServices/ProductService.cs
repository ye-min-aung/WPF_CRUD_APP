using CGMWPF.Service;
using Model;
using Service.CommonInterfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using CGMWPF.Common;
using System.Runtime.CompilerServices;

namespace Service.WebServices
{
    public class ProductService : ProductInterface,IDisposable
    {
        public ProductService()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(iAppSettings.ApiEndpoint + CTRL_NAME);
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add(Constant.HTTP_HEADER_APITOKEN, iAppSettings.ApiToken);
        }
        /// <summary>
        /// Define httpClient
        /// </summary>
        private HttpClient httpClient;

        /// <summary>
        /// Define ctrl_name
        /// </summary>
        private const string CTRL_NAME = "Product";
        public async Task<ObservableCollection<Product>> GetProductsAsync(string? searchString)
        {
            string result = string.Empty;
            try
            {
                Product productData = new Product();
                productData.Keyword = searchString;
                var content = new StringContent(JsonConvert.SerializeObject(productData), Encoding.UTF8, @"application/json");
                HttpResponseMessage responseMessage = await httpClient.PostAsync(httpClient.BaseAddress + "/GetProductList", content);
                responseMessage.EnsureSuccessStatusCode();
                result = await responseMessage.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException ex)
            {
                iAppSettings.Log.Logger.Error(ex.Message);
                return new ObservableCollection<Product>();
            }
            catch (Exception ex)
            {
                iAppSettings.Log.Logger.Error(ex.Message);
                return new ObservableCollection<Product>();
            }
            if (string.IsNullOrEmpty(result))
            {
                return new ObservableCollection<Product>();
            }
            var returnData = JsonConvert.DeserializeObject(result, typeof(ObservableCollection<Product>));
            if (returnData == null)
            {
                return new ObservableCollection<Product>();
            }
            else
            {
                return (ObservableCollection<Product>)returnData;
            }
        }
       

        public async Task<Product> GetProductById(int id)
        {
            string result = string.Empty;
            try
            {
                HttpResponseMessage responseMessage = await httpClient.GetAsync(httpClient.BaseAddress + "/GetProduct/" + id);
                responseMessage.EnsureSuccessStatusCode();
                result = await responseMessage.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException ex)
            {
                iAppSettings.Log.Logger.Error(ex.Message);
                return new Product();
            }
            catch (Exception ex)
            {
                iAppSettings.Log.Logger.Error(ex.Message);
                return new Product();
            }
            if (string.IsNullOrEmpty(result))
            {
                return new Product();
            }
            var objData = JsonConvert.DeserializeObject(result, typeof(Product));
            if (objData == null)
            {
                return new Product();
            }
            else
            {
                return (Product)objData;
            }
        }
        public async Task<int> UpdateAsync(Product obj, string methodName)
        {
            string result = string.Empty;
            try
            {
                //Product p = new Product() 
                //{
                //    Name = "sdfdsfdf",
                //    Description = "fasfasf",
                //    IsActive = true,
                //    IsDeleted= false,
                //    CreatedDate= DateTime.Now,
                //    CreatedUserId= "8",
                //};

                var content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, @"application/json");
                HttpResponseMessage responseMessage = await httpClient.PostAsync(httpClient.BaseAddress + "/" + methodName, content);
                responseMessage.EnsureSuccessStatusCode();
                result = await responseMessage.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException ex)
            {
                iAppSettings.Log.Logger.Error(ex.Message);
                return Constant.APIRESULT_ERROR;
            }
            catch (Exception ex)
            {
                iAppSettings.Log.Logger.Error(ex.Message);
                return Constant.APIRESULT_ERROR;
            }
            if (string.IsNullOrEmpty(result))
            {
                return Constant.APIRESULT_ERROR;
            }
            return iConvert.ToInt(result);
        }
        public async Task<int> DeleteProduct(Product obj)
        {
            string result = string.Empty;
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, @"application/json");
                HttpResponseMessage responseMessage = await httpClient.PostAsync(httpClient.BaseAddress + "/DeleteProduct", content);
                responseMessage.EnsureSuccessStatusCode();
                result = await responseMessage.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException ex)
            {
                iAppSettings.Log.Logger.Error(ex.Message);
                return Constant.APIRESULT_ERROR;
            }
            catch (Exception ex)
            {
                iAppSettings.Log.Logger.Error(ex.Message);
                return Constant.APIRESULT_ERROR;
            }
            if (string.IsNullOrEmpty(result))
            {
                return Constant.APIRESULT_ERROR;
            }
            return iConvert.ToInt(result);
        }

        public void Dispose()
        {
            httpClient.Dispose();
        }
    }
}
