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
    public partial class MemberPanel : Window
    {
        public MemberPanel()
        {
            InitializeComponent();
            Viewmodels.MemberPanel viewModel = new Viewmodels.MemberPanel();
            DataContext = viewModel;
        }
    }
}