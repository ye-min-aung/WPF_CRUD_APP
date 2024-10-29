using Client.Post;
using Client.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Client.Product
{
    /// <summary>
    /// Interaction logic for List.xaml
    /// </summary>
    public partial class List : Page
    {
        public List()
        {
            InitializeComponent();
            vm= new ProductViewModel();
            this.DataContext = vm;
        }
        /// <summary>
        /// Define vm
        /// </summary>
        private ProductViewModel vm;

        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            ProductModel? productModel = productDataGrid.CurrentItem as ProductModel;
            if (productModel != null)
            {
                this.NavigationService.Navigate(new Client.Product.Edit(productModel.Id));
            }
        }
    }
}
