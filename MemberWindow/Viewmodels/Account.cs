using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static DataManager.Entities;
using static DataManager.Interfaces;
using static DataManager.Logic;

namespace MemberWindow.Viewmodels
{
    public class Account : INotifyPropertyChanged
    {
        private readonly DataManager.Interfaces.IUnitOfWork _uow;
        private readonly DataManager.Logic.Controllers.Members _memberController;

        public ICommand cmdLogIn { get; private set; }
        public ICommand cmdSignUp { get; private set; }
        public Action actionLoggedIn { get; set; }
        public Action actionSignedUp { get; set; }

        private string _firstName = "First Name";
        private string _lastName = "Last Name";
        private string _mailAddress = "Email Address";
        private string _phoneNumber = "Phone Number";
        private DateTime _birthDate;
        private string _pinCode = "Pin code";

        public string firstName { get => _firstName; set { _firstName = value; OnPropertyChanged(); } }
        public string lastName { get => _lastName; set { _lastName = value; OnPropertyChanged(); } }
        public string mailAddress { get => _mailAddress; set { _mailAddress = value; OnPropertyChanged(); } }
        public string phoneNumber { get => _phoneNumber; set { _phoneNumber = value; OnPropertyChanged(); } }
        public DateTime birthDate { get => _birthDate; set { _birthDate = value; OnPropertyChanged(); } }
        public string pinCode { get => _pinCode; set { _pinCode = value; OnPropertyChanged(); } }

        public Account(DataManager.Interfaces.IUnitOfWork uow)
        {
            _uow = uow;
            _memberController = new DataManager.Logic.Controllers.Members(_uow);

            cmdLogIn = new RelayCommand(ValidateLogIn);
            cmdSignUp = new RelayCommand(CreateAccount);
        }

        private void ValidateLogIn(object obj)
        {
            try
            {
                Storage.signedInMember = _memberController.Login(_mailAddress, _pinCode);
                if (Storage.signedInMember != null)
                    actionLoggedIn.Invoke();
                else
                    throw new Exception("Failed to sign in");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CreateAccount(object obj)
        {
            try
            {
                _memberController.Register(new DataManager.Entities.Member { 
                    firstName = _firstName,
                    lastName = _lastName,
                    mailAddress = _mailAddress,
                    phoneNumber = _phoneNumber,
                    birthDate = _birthDate,
                    pinCode = _pinCode,
                    paymentStatus = false
                });
                _memberController.Complete();
                actionSignedUp.Invoke();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message} ({ex.InnerException})");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
