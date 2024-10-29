using CGMWPF.Front.AppControls;
using Client.Account;

namespace Client.Account
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : iWindow
    {
        /// <summary>
        /// Define vm
        /// </summary>
        private LoginViewModel vm;

        /// <summary>
        /// Constructor
        /// </summary>
        public Login()
        {
            vm = new LoginViewModel();
            vm.Logined += Vm_Logined;
            this.DataContext = vm;
            vm.ParentForm = this;
            InitializeComponent();
        }

        /// <summary>
        /// Update password
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>
        /// </returns>
        private string Vm_Logined()
        {
            return TxtPass.Password;
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }
    }
}
