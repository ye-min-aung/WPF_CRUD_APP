using CGMWPF.Common;
using DataAccess.Repositories;
using Model;
using Service.CommonInterfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.LocalServices
{
    public class ProductService : ProductInterface
    {
        private readonly ProductRepository repository;
        public ProductService() 
        {
            this.repository = new ProductRepository();
        }
        public async Task<int> DeleteProduct(Product obj)
        {
            int result = 0;
            result = await repository.DeleteProduct(obj);
            return iConvert.ToInt(result);
        }

        public async Task<Product> GetProductById(int id)
        {
            return await repository.GetProducttById(id);
        }

        public async Task<ObservableCollection<Product>> GetProductsAsync(string? searchString)
        {
            var result = new ObservableCollection<Product>();
            var productlist = await repository.GetProductList(searchString);
            if (productlist == null)
            {
                return result;
            }
            else
            {
                result = new ObservableCollection<Product>(productlist);
                return result;
            }
        }

        public async Task<int> UpdateAsync(Product obj, string methodName)
        {
            int result = 0;
            if (methodName == "SaveProduct")
            {
                result = await repository.SaveProduct(obj);
                return iConvert.ToInt(result);
            }
            else if (methodName == "UpdateProduct")
            {
                result = await repository.UpdateProduct(obj);
                return iConvert.ToInt(result);
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
