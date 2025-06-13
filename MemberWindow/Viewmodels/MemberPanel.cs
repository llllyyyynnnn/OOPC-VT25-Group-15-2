using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Net.Mail;
using static DataManager.Entities;
using static DataManager.Logic;
using DataManager;
using System.Collections.ObjectModel;

namespace MemberWindow.Viewmodels
{
    public class MemberPanel : INotifyPropertyChanged
    {
        private readonly DataManager.Interfaces.IUnitOfWork _uow;
        private readonly DataManager.Logic.Controllers.Members _memberController;

        public ICommand cmdSaveNewPinCode { get; private set; }





        private string _currentPinCode = "Current password";
        public string currentPinCode { get => _currentPinCode; set { _currentPinCode = value; OnPropertyChanged(); } }
        private string _newPinCode = "New password";
        public string newPinCode { get => _newPinCode; set { _newPinCode = value; OnPropertyChanged(); } }

        private Member _selectedMember;
        public Member selectedMember { get => _selectedMember; set { _selectedMember = value; OnPropertyChanged(); } }
        public string mailAddress { get; private set; }
        public string welcomeMember { get; private set; }


        private ObservableCollection<Member> _members;
        public ObservableCollection<Member> members { get => _members; set { _members = value; OnPropertyChanged(); } }

        public MemberPanel()
        {
            _uow = new DataManager.Logic.UnitOfWork(Storage.ctx);
            _memberController = new DataManager.Logic.Controllers.Members(_uow);

            cmdSaveNewPinCode = new RelayCommand(SaveNewPinCode);
            members = new ObservableCollection<Member>(_memberController.GetMembers());
            mailAddress = Storage.signedInMember.mailAddress;
            welcomeMember = $"Welcome back, {Storage.signedInMember.firstName} {Storage.signedInMember.lastName}";
        }

        private void SaveNewPinCode(object obj)
        {
            try
            {
                if (_currentPinCode == Storage.signedInMember.pinCode)
                {
                    _memberController.Update(Storage.signedInMember, entity => { entity.pinCode = newPinCode; });
                    _memberController.Complete();

                    currentPinCode = "Current password";
                    newPinCode = "New password";
                }
                else
                    throw new Exception("Current password is invalid");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
