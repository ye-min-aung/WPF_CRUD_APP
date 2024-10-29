using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.CommonInterfaces
{
    public interface ProductInterface
    {
        Task<ObservableCollection<Product>> GetProductsAsync(string? searchString);
        Task<int> UpdateAsync(Product obj, string methodName);
        Task<int> DeleteProduct(Product obj);
        Task<Product> GetProductById(int id);
    }
}
