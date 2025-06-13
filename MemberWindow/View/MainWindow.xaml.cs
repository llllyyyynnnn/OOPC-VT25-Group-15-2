using System.Runtime.CompilerServices;
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
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Viewmodels.Account viewModel = new Viewmodels.Account();
            DataContext = viewModel;
            viewModel.actionLoggedIn += OnLoggedIn;
            viewModel.actionSignedUp += OnSignedUp;
        }

        private void OnLoggedIn()
        {
            MemberPanel memberPanel = new MemberPanel();
            memberPanel.Show();
            this.Close();
        }

        private void OnSignedUp()
        {
            SwitchPanels();
            TitleLabel.Content = "Member - Successfully signed up!";
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