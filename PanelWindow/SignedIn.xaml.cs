using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DataManager;
using static DataManager.Handlers;

namespace PanelWindow
{
    /// <summary>
    /// Interaction logic for SignedIn.xaml
    /// </summary>
    public partial class SignedIn : Window
    {
        private readonly DataManager.Handlers.Controllers.Members _controller;

        public SignedIn()
        {
            InitializeComponent();
            _controller = new DataManager.Handlers.Controllers.Members(Storage.uow);
            RefreshMembersList();
        }

        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RefreshMembersList() => membersList.ItemsSource = _controller.GetMembers();
        private void refreshMembersButton_Click(object sender, RoutedEventArgs e) => RefreshMembersList();

        private void addMemberButton_Click(object sender, RoutedEventArgs e)
        {
            SignedIn_MemberUI memberUI = new SignedIn_MemberUI(_controller);
            memberUI.ShowDialog();
            RefreshMembersList();
        }

        private void deleteMemberButton_Click(object sender, RoutedEventArgs e)
        {
            if(membersList.SelectedItem != null && membersList.SelectedItem is Entities.Member member)
            {
                try
                {
                    MessageBoxResult res = MessageBox.Show($"Your request to delete {member.mailAddress} with id {member.id} is irreversible. Are you sure you want to continue?", "Warning", MessageBoxButton.YesNo);
                    if (res == MessageBoxResult.Yes)
                    {
                        _controller.Delete(member);
                        _controller.Complete();
                        RefreshMembersList();
                    }
                }
                catch (Exception ex) { 
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void editMemberButton_Click(object sender, RoutedEventArgs e)
        {
            if (membersList.SelectedItem != null && membersList.SelectedItem is Entities.Member member) {
                SignedIn_MemberUI memberUI = new SignedIn_MemberUI(_controller, member);
                memberUI.ShowDialog();
                RefreshMembersList();
            }
            else
            {
                MessageBox.Show("Invalid item selected");
            }
        }
    }
}
