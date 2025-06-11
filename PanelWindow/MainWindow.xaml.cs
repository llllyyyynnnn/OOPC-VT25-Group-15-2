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
            try
            {
                Storage.signedInCoach = _controller.Login(logInEmailTextBox.Text, logInPasswordTextBox.Password);
                if(Storage.signedInCoach != null)
                {

                }
                else
                {
                    throw new Exception("Failed to sign in");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            DataManager.Entities.Coach coach = new DataManager.Entities.Coach
            {
                firstName = firstNameTextBox.Text,
                lastName = lastNameTextBox.Text,
                birthDate = regDateofBirth.SelectedDate.Value,
                phoneNumber = regPhoneNumberTextBox.Text,
                mailAddress = regEmailTextBox.Text,
                specialisation = regSpecialisation.Text,
                pinCode = regPasswordTextBox.Password
            };

            try
            {
                _controller.Register(coach);
                _controller.Complete();
                SwitchPanels();
                TitleLabel.Content = "Coach - Successfully signed up!";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message} ({ex.InnerException})");
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