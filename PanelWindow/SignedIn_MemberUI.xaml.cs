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
                try
                {
                    _controller.Update(_member, entity =>
                    {
                        entity.firstName = regFirstNameTextBox.Text;
                        entity.lastName = regLastNameTextBox.Text;
                        entity.birthDate = regDateofBirth.SelectedDate.Value;
                        entity.phoneNumber = regPhoneNumberTextBox.Text;
                        entity.mailAddress = regEmailTextBox.Text;
                        entity.pinCode = regPasswordTextBox.Password;
                    });

                    _controller.Complete();
                }
                catch (Exception ex) {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                try
                {
                    DataManager.Entities.Member newMember = new DataManager.Entities.Member
                    {
                        firstName = regFirstNameTextBox.Text,
                        lastName = regLastNameTextBox.Text,
                        birthDate = regDateofBirth.SelectedDate.Value,
                        phoneNumber = regPhoneNumberTextBox.Text,
                        mailAddress = regEmailTextBox.Text,
                        pinCode = regPasswordTextBox.Password
                    };

                    _controller.Register(newMember);
                    _controller.Complete();
                    MessageBox.Show("Successfully created new member");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            this.Close();
        }
    }
}
