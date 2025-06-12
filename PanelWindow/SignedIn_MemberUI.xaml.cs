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
        private readonly DataManager.Logic.Controllers.Members _controller;
        private bool _modifyingMember = false;
        private readonly DataManager.Entities.Member _member;

        public SignedIn_MemberUI(DataManager.Logic.Controllers.Members controller, DataManager.Entities.Member member = null)
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
                regPaymentStatus.IsChecked = member.paymentStatus;

                _modifyingMember = true;
            }
            else
                TitleLabel.Content = $"Creating a new member";
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_modifyingMember)
                {
                    WindowFunctions.MemberUI.Modify(_controller, _member, new Entities.Member
                    {
                        firstName = regFirstNameTextBox.Text,
                        lastName = regLastNameTextBox.Text,
                        birthDate = regDateofBirth.SelectedDate.Value,
                        phoneNumber = regPhoneNumberTextBox.Text,
                        mailAddress = regEmailTextBox.Text,
                        pinCode = regPasswordTextBox.Password,
                        paymentStatus = regPaymentStatus.IsChecked.Value
                    });
                }
                else
                {
                    WindowFunctions.MemberUI.Register(_controller, new Entities.Member
                    {
                        firstName = regFirstNameTextBox.Text,
                        lastName = regLastNameTextBox.Text,
                        birthDate = regDateofBirth.SelectedDate.Value,
                        phoneNumber = regPhoneNumberTextBox.Text,
                        mailAddress = regEmailTextBox.Text,
                        pinCode = regPasswordTextBox.Password,
                        paymentStatus = regPaymentStatus.IsChecked.Value
                    });
                }

                this.Close();
            }
            catch (Exception ex){ MessageBox.Show(ex.Message); }


        }
    }
}
