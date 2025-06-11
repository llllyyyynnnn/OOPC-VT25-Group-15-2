using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
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

namespace PanelWindow
{
    public partial class SignedIn_MemberUI : Window
    {
        private readonly DataManager.Handlers.Controllers.Members _controller;
        private bool _modifyingMember = false;
        private readonly DataManager.Entities.Member _member;

        public SignedIn_MemberUI(DataManager.Handlers.Controllers.Members controller, DataManager.Entities.Member member = null)
        {
            InitializeComponent();
            _controller = controller;

            if (member != null)
            {
                _member = member;
                TitleLabel.Content = $"Modifying member ({member.id})";
                regFirstNameTextBox.Text = member.firstName;
                regLastNameTextBox.Text = member.lastName;
                regDateofBirth.Text = member.birthDate.ToString();
                regEmailTextBox.Text = member.mailAddress;
                regPasswordTextBox.Password = member.pinCode;

                _modifyingMember = true;
            }
            else
                TitleLabel.Content = $"Creating a new member";
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (_modifyingMember)
            {
                ViewModels.MemberUI.Modify(_controller, _member, new Entities.Member
                {
                    firstName = regFirstNameTextBox.Text,
                    lastName = regLastNameTextBox.Text,
                    birthDate = regDateofBirth.SelectedDate.Value,
                    phoneNumber = regPhoneNumberTextBox.Text,
                    mailAddress = regEmailTextBox.Text,
                    pinCode = regPasswordTextBox.Password
                });
            }
            else
            {
                ViewModels.MemberUI.Register(_controller, new Entities.Member
                {
                    firstName = regFirstNameTextBox.Text,
                    lastName = regLastNameTextBox.Text,
                    birthDate = regDateofBirth.SelectedDate.Value,
                    phoneNumber = regPhoneNumberTextBox.Text,
                    mailAddress = regEmailTextBox.Text,
                    pinCode = regPasswordTextBox.Password
                });
            }

            this.Close();
        }
    }
}
