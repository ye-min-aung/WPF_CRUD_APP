using CGMWPF.Back.WebAPI;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace WebApi.API
{
    [Route("Product")]
    [ApiController]
    public class ProductAPI : ControllerBase
    {
        private readonly ProductRepository repo;
        public ProductAPI(ProductRepository _repo)
        {
            this.repo = _repo;
        }

        /// <summary>
        /// Get product lists from table and filter if search string is not empty
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>
        /// The <see cref="Task{ActionResult{List{Product}?}}"/>
        /// </returns>
        [HttpPost("GetProductList")]
        public async Task<ActionResult<List<Product>?>> GetProductList(Product obj)
        {
            Microsoft.Extensions.Primitives.StringValues requestHeaders;
            Request.Headers.TryGetValue(APISetting.KEY_APITOKEN, out requestHeaders);
            if (APISetting.Configuration.GetValue(APISetting.KEY_APITOKEN, string.Empty).Equals(requestHeaders) == false)
            {
                return Unauthorized();
            }
            return await repo.GetProductList(obj.Keyword);
        }

        /// <summary>
        /// Save product data into table
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>
        /// The <see cref="Task{ActionResult{int}}"/>
        /// </returns>
        [HttpPost("SaveProduct")]
        public async Task<ActionResult<int>> SaveProduct(Product obj)
        {
            Microsoft.Extensions.Primitives.StringValues requestHeaders;
            Request.Headers.TryGetValue(APISetting.KEY_APITOKEN, out requestHeaders);
            if (APISetting.Configuration.GetValue(APISetting.KEY_APITOKEN, string.Empty).Equals(requestHeaders) == false)
            {
                return Unauthorized();
            }
            return await repo.SaveProduct(obj);
        }

        /// <summary>
        /// Get product data by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// The <see cref="Task{ActionResult{Product}}"/>
        /// </returns>
        [HttpGet("GetProduct/{id}")]
        public async Task<ActionResult<Product>> GetPostById([FromRoute] int id)
        {
            Microsoft.Extensions.Primitives.StringValues requestHeaders;
            Request.Headers.TryGetValue(APISetting.KEY_APITOKEN, out requestHeaders);
            if (APISetting.Configuration.GetValue(APISetting.KEY_APITOKEN, string.Empty).Equals(requestHeaders) == false)
            {
                return Unauthorized();
            }
           
            return await repo.GetProducttById(id);
        }

        /// <summary>
        /// Update product data
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>
        /// The <see cref="Task{ActionResult{int}}"/>
        /// </returns>
        [HttpPost("UpdateProduct")]
        public async Task<ActionResult<int>> UpdateProduct(Product obj)
        {
            Microsoft.Extensions.Primitives.StringValues requestHeaders;
            Request.Headers.TryGetValue(APISetting.KEY_APITOKEN, out requestHeaders);
            if (APISetting.Configuration.GetValue(APISetting.KEY_APITOKEN, string.Empty).Equals(requestHeaders) == false)
            {
                return Unauthorized();
            }
            return await repo.UpdateProduct(obj);
        }

        /// <summary>
        /// Delete product data by id
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>
        /// The <see cref="Task{ActionResult{int}}"/>
        /// </returns>
        [HttpPost("DeleteProduct")]
        public async Task<ActionResult<int>> DeletePost(Product obj)
        {
            Microsoft.Extensions.Primitives.StringValues requestHeaders;
            Request.Headers.TryGetValue(APISetting.KEY_APITOKEN, out requestHeaders);
            if (APISetting.Configuration.GetValue(APISetting.KEY_APITOKEN, string.Empty).Equals(requestHeaders) == false)
            {
                return Unauthorized();
            }
            return await repo.DeleteProduct(obj);
        }
    }
}
