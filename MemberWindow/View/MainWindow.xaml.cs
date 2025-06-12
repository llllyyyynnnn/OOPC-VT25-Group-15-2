using System.Security.Cryptography.Xml;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Identity.Client.Extensions.Msal;

namespace MemberWindow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LogIn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void SwitchPanels()
        {
            if (logInPanel.Visibility == Visibility.Visible)
            {
                logInPanel.Visibility = Visibility.Collapsed;
                registerPanel.Visibility = Visibility.Visible;
                TitleLabel.Content = "Member - Register";
                noAccountLabel.Content = "Have an account already?";
            }
            else
            {
                logInPanel.Visibility = Visibility.Visible;
                registerPanel.Visibility = Visibility.Collapsed;
                TitleLabel.Content = "Member - Log In";
                noAccountLabel.Content = "Don't have an account?";
            }
        }

        private void NoAccount_MouseDown(object sender, RoutedEventArgs e)
        {
            SwitchPanels();
        }
        private void ForgotPassword_MouseDown(object sender, RoutedEventArgs e)
        {

        }
    }
}