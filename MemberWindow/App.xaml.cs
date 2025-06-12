using System.Configuration;
using System.Data;
using System.Windows;
using Microsoft.Identity.Client.Extensions.Msal;

namespace MemberWindow
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            //Storage.Initialize();

            MainWindow main = new MainWindow();
            main.Show();
        }
    }
}
