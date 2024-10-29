using CGMWPF.Back.DataAccess;
using CGMWPF.Back.DataAccess.Data;
using DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class ProductRepository
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ProductRepository() 
        {
            _dataContext = new DataContext<Product>(new DBContext());
        }
        /// <summary>
        /// Define datacontext for posts table
        /// </summary>
       private DataContext<Product> _dataContext;

        /// <summary>
        /// Fetch post lists and filter with search string
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns>
        /// The <see cref="Task{List{Model.Product}?}"/>
        /// </returns>
        public async Task<List<Model.Product>?> GetProductList(string searchString)
        {
            try
            {
                using (var _context = new DBContext())
                {
                    var query = (from p in _context.Products
                                 join u in _context.Users on p.CreatedUserId equals u.Id.ToString()
                                 where p.IsDeleted == false
                                 orderby p.Id descending
                                 select new Model.Product
                                 {
                                     Id = p.Id,
                                     Name = p.Name,
                                     Description = p.Description,
                                     IsActive = p.IsActive,
                                     Active = p.IsActive ? "Active" : "InActive",
                                     CreatedAt = p.CreatedDate.ToString("MM/dd/yyyy"),
                                     CreatedBy = u.FirstName + " " + u.LastName,
                                     CreatedUserId = p.CreatedUserId,
                                 }).ToList();
                    if (!string.IsNullOrEmpty(searchString))
                    {
                        string keyword = searchString.ToLower();
                        query = query.Where(x =>
                        x.Name.ToLower().Contains(keyword) ||
                        x.Description.ToLower().Contains(keyword) ||
                        x.CreatedBy.ToLower().Contains(keyword)
                        ).ToList();
                    }
                    return query;
                }
            }
            catch (Exception ex)
            {
                CommonSetting.Log.Logger.Error(ex.Message);
                return new List<Model.Product>();
            }
        }

        /// <summary>
        /// Save product data into table
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>
        /// The <see cref="int"/> 1 = success, 3 = fail
        /// </returns>
        public async Task<int> SaveProduct(Model.Product obj)
        {
            try
            {
                using (var _context = new DBContext())
                {
                    Product product = new()
                    {
                        Name = obj.Name,
                        Description = obj.Description,
                        IsActive = obj.IsActive,
                        IsDeleted = false,
                        CreatedDate = DateTime.Now,
                        CreatedUserId = obj.CreatedUserId
                    };
                    _context.Products.Add(product);
                    _context.SaveChanges();
                    return iConstance.RESULT_SUCCESS;
                }

            }
            catch (Exception ex)
            {
                CommonSetting.Log.Logger.Error(ex.Message);
                return iConstance.RESULT_FAILURE;
            }
        }

        /// <summary>
        /// Get product by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// The <see cref="Task{Model.Post}"/>
        /// </returns>
        public async Task<Model.Product> GetProducttById(int id)
        {
            try
            {
                var query = await _dataContext.Select(x => x.Id == id);
                _dataContext.Dispose();
                if (query != null)
                {
                    Model.Product product = new()
                    {
                        Id = query.Id,
                        Name = query.Name,
                        Description = query.Description,
                        IsActive = query.IsActive,
                    };
                    return product;
                }
                return new Model.Product();
            }
            catch (Exception ex)
            {
                CommonSetting.Log.Logger.Error(ex.Message);
                return new Model.Product();
            }
        }

        /// <summary>
        /// Update product data into table
        /// </summary>
        /// <param name="post"></param>
        /// <returns>
        /// The <see cref="int"/> 1 = success, 3 = fail
        /// </returns>
        public async Task<int> UpdateProduct(Model.Product product)
        {
            try
            {
                using (var _context = new DBContext())
                {
                    var currentProduct = await _dataContext.Select(x => x.Id == product.Id);
                    _dataContext.Dispose();
                    if (currentProduct != null)
                    {
                        currentProduct.Name = product.Name;
                        currentProduct.Description = product.Description;
                        currentProduct.IsActive = product.IsActive;
                        currentProduct.UpdatedDate = DateTime.Now;
                        currentProduct.UpdatedUserId = product.UpdatedUserId;
                        _context.Products.Update(currentProduct);
                        _context.SaveChanges();
                        return iConstance.RESULT_SUCCESS;
                    }

                }
                return iConstance.RESULT_FAILURE;
            }
            catch (Exception ex)
            {
                CommonSetting.Log.Logger.Error(ex.Message);
                return iConstance.RESULT_FAILURE;
            }
        }


        /// <summary>
        /// Delete product by id
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>
        /// The <see cref="int"/> 1 = success, 3 = fail
        /// </returns>
        public async Task<int> DeleteProduct(Model.Product obj)
        {
            try
            {
                using (var _context = new DBContext())
                {
                    var deleteProduct = await _dataContext.Select(x => x.Id == obj.Id);
                    if (deleteProduct != null)
                    {
                        deleteProduct.IsDeleted = true;
                        deleteProduct.DeletedDate = DateTime.Now;
                        deleteProduct.DeletedUserId = obj.DeletedUserId;
                        _context.Products.Update(deleteProduct);
                        _context.SaveChanges();
                        return iConstance.RESULT_SUCCESS;
                    }
                    return iConstance.RESULT_FAILURE;
                }
            }
            catch (Exception ex)
            {
                CommonSetting.Log.Logger.Error(ex.Message);
                return iConstance.RESULT_FAILURE;
            }
        }
    }
}
