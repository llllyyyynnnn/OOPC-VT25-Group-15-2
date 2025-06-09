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

namespace PanelWindow
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

        private void LogIn_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(emailTextBox.Text);
            
        }

        private void NoAccount_MouseDown(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Label clicked!");
        }
        private void ForgotPassword_MouseDown(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Label clicked!");
        }
    }
}