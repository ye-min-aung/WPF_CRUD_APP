using CGMWPF.Common;
using CGMWPF.Front.AppControls;
using CGMWPF.Service;
using Client.Post;
using Client.ViewModels;
using DataAccess.Repositories;
using Service.LocalServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Client.Product
{
    public class ProductViewModel : iViewModel
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ProductViewModel()
        {
            Product= new ProductModel();
            ProductList = new ObservableCollection<ProductModel>();
            this.Initilized();
            GetProductList();
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id"></param>
        public ProductViewModel(int id)
        {
            Product = new ProductModel();
            Initilized();
            GetProduct(id);
        }
        /// <summary>
        /// Define product list
        /// </summary>
        public ObservableCollection<ProductModel> ProductList { get; set; }
        /// <summary>
        /// Define product
        /// </summary>
        public ProductModel Product { get; set; }
        /// <summary>
        /// Define search command
        /// </summary>
        public iDelegateCommand? Search { get; set; }

        /// <summary>
        /// Define delete command
        /// </summary>
        public iDelegateCommand? Delete { get; set; }

        /// <summary>
        /// Define create command
        /// </summary>
        public iDelegateCommand? Create { get; set; }

        /// <summary>
        /// Define cancel command
        /// </summary>
        public iDelegateCommand? Cancel { get; set; }

        /// <summary>
        /// Define save command
        /// </summary>
        public iDelegateCommand? Save { get; set; }

        /// <summary>
        /// Initialize
        /// </summary>
        private void Initilized()
        {
            this.Create = new iDelegateCommand(
                (object? arg) =>
                {
                    MainViewModel.Instance.PagePath = iNavigation.PRODUCT_CREATE;
                }, () => true);
            this.Search = new iDelegateCommand(
                (object? arg) =>
                {
                    this.GetProductList();
                }, () => true);
            this.Delete = new iDelegateCommand(
                (object? arg) =>
                {
                    this.DeleteProduct(iConvert.ToInt(arg));
                }, () => true);
            this.Save = new iDelegateCommand(
                (object? arg) =>
                {
                    this.UpdateAsync();
                }, () => true);
            this.Cancel = new iDelegateCommand(
                (object? arg) =>
                {
                    MainViewModel.Instance.PagePath = iNavigation.PRODUCT_LIST;
                },
                () => true);
        }
        /// <summary>
        /// Get all posts
        /// </summary>
        private async void GetProductList()
        {
            ProductList.Clear();
            var commonApi = new ProductService();

            var getProdcut = await commonApi.GetProductsAsync(Product.Keyword);
            if (getProdcut.Count > 0)
            {
                foreach (var data in getProdcut)
                {
                    ProductModel product = new();
                    iConvert.CopyClassProperty<Model.IProduct>(data, product);
                    if (iAppSettings.LoginUser.Role != 1)
                    {
                        if (product.CreatedUserId == iConvert.ToString(iAppSettings.LoginUser.Id))
                        {
                            ProductList.Add(product);
                        }
                    }
                    else
                    {
                        ProductList.Add(product);
                    }

                }
            }
        }
        /// <summary>
        /// Get product data by id
        /// </summary>
        /// <param name="id"></param>
        private async void GetProduct(int id)
        {
            var commonApi = new ProductService();
            var getData = await commonApi.GetProductById(id);
            iConvert.CopyClassProperty<Model.IProduct>(getData, Product);
        }
        /// <summary>
        /// Save,Update product data by checking id
        /// </summary>
        private async void UpdateAsync()
        {
            var commonApi = new ProductService();

            if (CheckInput())
            {
                Mouse.OverrideCursor = Cursors.Wait;
                Model.Product data = new();
                iConvert.CopyClassProperty<Model.IProduct>(Product, data);
                if (Product.Id > 0)
                {
                    data.UpdatedUserId = iConvert.ToString(iAppSettings.LoginUser.Id);
                    int result = await commonApi.UpdateAsync(data, "UpdateProduct");
                    Mouse.OverrideCursor = null;
                    if (result == Constant.APIRESULT_SUCCESS)
                    {
                        iMessage.ShowInfomation(iMessage.MT_0182, iMessage.IMSG_TRAN_0011);
                        MainViewModel.Instance.PagePath = iNavigation.PRODUCT_LIST;
                    }
                    else
                    {
                        iMessage.ShowError(iMessage.MT_0182, iMessage.EMSG_TRAN_0011);
                    }
                }
                else
                {
                    data.CreatedUserId = iConvert.ToString(iAppSettings.LoginUser.Id);
                    Model.Product p = new Model.Product();
                    p.Name= Product.Name;
                    p.Description= Product.Description;
                    p.CreatedDate = DateTime.Now;
                    p.IsActive = Product.IsActive;
                    p.IsDeleted = Product.IsDeleted;
                    p.CreatedUserId= iConvert.ToString(iAppSettings.LoginUser.Id);
                    int result = await commonApi.UpdateAsync(p, "SaveProduct");
                    Mouse.OverrideCursor = null;
                    if (result == Constant.APIRESULT_SUCCESS)
                    {
                        iMessage.ShowInfomation(iMessage.MT_0181, iMessage.IMSG_TRAN_0010);
                        MainViewModel.Instance.PagePath = iNavigation.PRODUCT_LIST;
                    }
                    else
                    {
                        iMessage.ShowError(iMessage.MT_0181, iMessage.EMSG_TRAN_0010);
                    }
                }
            }

        }
        /// <summary>
        /// Delete product data by id
        /// </summary>
        /// <param name="id"></param>
        private async void DeleteProduct(int id)
        {
            MessageBoxResult resultData = iMessage.ShowQuestion(iMessage.MT_0183, iMessage.QMSG_TRAN_PRODUCT_3010, MessageBoxResult.Cancel);
            if (resultData == MessageBoxResult.Yes)
            {
                var commonApi = new ProductService();

                Model.Product deleteProduct = new()
                {
                    Id = id,
                    IsDeleted = true,
                    DeletedUserId = iConvert.ToString(iAppSettings.LoginUser.Id)
                };
                int result = await commonApi.DeleteProduct(deleteProduct);
                if (result == Constant.APIRESULT_SUCCESS)
                {
                    iMessage.ShowInfomation(iMessage.MT_0183, iMessage.IMSG_TRAN_0012);
                    this.GetProductList();
                }
                else
                {
                    iMessage.ShowError(iMessage.MT_0183, iMessage.EMSG_TRAN_0012);
                }

            }
        }
        /// <summary>
        /// Validate input
        /// </summary>
        /// <returns></returns>
        private bool CheckInput()
        {
            bool checkResult = true;
            string messageTtl = Product.Id > 0 ? iMessage.MT_0182 : iMessage.MT_0181;
            if (string.IsNullOrEmpty(Product.Name))
            {
                iMessage.ShowInfomation(messageTtl, iMessage.WMSG_TRAN_P_2181);
                checkResult = true;
            }
            else if (string.IsNullOrEmpty(Product.Description))
            {
                iMessage.ShowInfomation(messageTtl, iMessage.WMSG_TRAN_P_2190);
                checkResult = false;
            }
            return checkResult;
        }
    }
}
