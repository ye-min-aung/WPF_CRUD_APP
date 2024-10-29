using CGMWPF.Common;
using CGMWPF.Service;
using Service.WebServices;
using System.Configuration;
using System.Windows;

namespace Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            this.Startup += App_Startup;
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            iAppSettings.ApiEndpoint = iConvert.ToString(ConfigurationManager.AppSettings.Get(Constant.HTTP_ENDPOINT));
            iAppSettings.ApiToken = iConvert.ToString(ConfigurationManager.AppSettings.Get(Constant.HTTP_HEADER_APITOKEN));
            iAppSettings.Log = new iLog(iConvert.ToInt(iConvert.ToString(ConfigurationManager.AppSettings.Get(Constant.LOG_MAXDAYCOUNT))));
        }
    }
    
}
