using Client.Post;
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
    /// Interaction logic for Edit.xaml
    /// </summary>
    public partial class Edit : Page
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id"></param>
        public Edit(int id)
        {
            InitializeComponent();
            vm = new ProductViewModel(id);
            this.DataContext = vm;
        }
        /// <summary>
        /// Define vm
        /// </summary>
        private ProductViewModel vm;
    }
}
