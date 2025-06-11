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
using DataManager;

namespace PanelWindow
{
    public partial class MainWindow : Window
    {
        private readonly DataManager.Handlers.Controllers.Coaches _controller;

        public MainWindow()
        {
            InitializeComponent();
            _controller = new DataManager.Handlers.Controllers.Coaches(Storage.uow);
        }

        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
            if(WindowFunctions.LogInUI.LogIn(_controller, logInEmailTextBox.Text, logInPasswordTextBox.Password))
            {
                SignedIn windowSignedIn = new SignedIn();
                windowSignedIn.Show();
                this.Close();
            }
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            DataManager.Entities.Coach coach = new DataManager.Entities.Coach
            {
                firstName = regFirstNameTextBox.Text,
                lastName = regLastNameTextBox.Text,
                birthDate = regDateofBirth.SelectedDate.Value,
                phoneNumber = regPhoneNumberTextBox.Text,
                mailAddress = regEmailTextBox.Text,
                specialisation = regSpecialisation.Text,
                pinCode = regPasswordTextBox.Password
            };

            if (WindowFunctions.LogInUI.Register(_controller, coach))
            {
                SwitchPanels();
                TitleLabel.Content = "Coach - Successfully signed up!";
            }
        }

        private void SwitchPanels()
        {
            if (logInPanel.Visibility == Visibility.Visible)
            {
                logInPanel.Visibility = Visibility.Collapsed;
                registerPanel.Visibility = Visibility.Visible;
                TitleLabel.Content = "Coach - Register";
                noAccountLabel.Content = "Have an account already?";
            }
            else
            {
                logInPanel.Visibility = Visibility.Visible;
                registerPanel.Visibility = Visibility.Collapsed;
                TitleLabel.Content = "Coach - Log In";
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