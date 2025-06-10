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
        private readonly DataManager.Controllers.Members _controller;
        private readonly DataManager.Context _ctx;
        private readonly DataManager.UnitOfWork _uow;


        /*
         public MainWindow(DataManager.Controllers.Members controller)
        {
            InitializeComponent();
            _controller = controller;
        }
         */

        public MainWindow()
        {
            InitializeComponent();

            _ctx = new Context();
            _uow = new UnitOfWork(_ctx);
            _controller = new DataManager.Controllers.Members(_uow);
        }

        private void LogIn_Click_1(object sender, RoutedEventArgs e)
        {
            DataManager.Entities.Member member = new DataManager.Entities.Member
            {
                fullName = "",
                birthDate = DateOnly.FromDayNumber(100),
                phoneNumber = "123456789",
                mailAddress = emailTextBox.Text
            };

            _controller.Register(member);
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