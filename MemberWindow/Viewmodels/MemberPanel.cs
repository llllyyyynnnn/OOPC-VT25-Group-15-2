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
        private readonly DataManager.Logic.Controllers.Sessions _sessionController;

        public ICommand cmdSaveNewPinCode { get; private set; }
        public ICommand cmdJoinSession { get; private set; }
        public ICommand cmdLeaveSession { get; private set; }


        private string _currentPinCode = "Current password";
        public string currentPinCode { get => _currentPinCode; set { _currentPinCode = value; OnPropertyChanged(); } }
        private string _newPinCode = "New password";
        public string newPinCode { get => _newPinCode; set { _newPinCode = value; OnPropertyChanged(); } }

        
        public string mailAddress { get; private set; }
        public string welcomeMember { get; private set; }
        private string _sessionCurrentParticipantCount;
        public string sessionCurrentParticipantCount
        {
            get => _sessionCurrentParticipantCount;
            set
            {
                _sessionCurrentParticipantCount = value;
                OnPropertyChanged();
            }
        }

        private string _sessionMaxParticipants;
        public string sessionMaxParticipants
        {
            get => _sessionMaxParticipants;
            set
            {
                _sessionMaxParticipants = value;
                OnPropertyChanged();
            }
        }

        private string _sessionCoach;
        public string sessionCoach
        {
            get => _sessionCoach;
            set
            {
                _sessionCoach = value;
                OnPropertyChanged();
            }
        }

        private string _sessionJoinedStatus;
        public string sessionJoinedStatus
        {
            get => _sessionJoinedStatus;
            set
            {
                _sessionJoinedStatus = value;
                OnPropertyChanged();
            }
        }


        private Member _selectedMember;
        public Member selectedMember { get => _selectedMember; set { _selectedMember = value; OnPropertyChanged(); } }
        private ObservableCollection<Member> _members;
        public ObservableCollection<Member> members { get => _members; set { _members = value; OnPropertyChanged(); } }

        private void UpdateSessionStatus()
        {
            sessionCurrentParticipantCount = $"Currently participating: {_selectedSession.members.Count()} members";
            sessionMaxParticipants = $"This session has a capacity of: {_selectedSession.participants} amount of people.";
            sessionCoach = $"Coach: {_selectedSession.coach}";
            sessionJoinedStatus = selectedSession.members.Contains(Storage.signedInMember)
                ? "You have joined this session"
                : "You are not part of this session";
        }

        private Session _selectedSession;
        public Session selectedSession { get => _selectedSession; set { 
                _selectedSession = value;
                UpdateSessionStatus();
                OnPropertyChanged(); 
            } }
        private ObservableCollection<Session> _sessions;
        public ObservableCollection<Session> sessions { get => _sessions; set { _sessions = value; OnPropertyChanged(); } }

        public MemberPanel()
        {
            _uow = new DataManager.Logic.UnitOfWork(Storage.ctx);
            _memberController = new DataManager.Logic.Controllers.Members(_uow);
            _sessionController = new DataManager.Logic.Controllers.Sessions(_uow);

            cmdSaveNewPinCode = new RelayCommand(SaveNewPinCode);
            cmdJoinSession = new RelayCommand(JoinSession);
            cmdLeaveSession = new RelayCommand(LeaveSession);

            members = new ObservableCollection<Member>(_memberController.GetMembers());
            sessions = new ObservableCollection<Session>(_sessionController.GetSessions());

            mailAddress = Storage.signedInMember.mailAddress;
            welcomeMember = $"Welcome back, {Storage.signedInMember.firstName} {Storage.signedInMember.lastName}";
        }

        private void JoinSession(object obj)
        {
            try
            {
                if (selectedSession.members == null || selectedSession.participants + 1 > selectedSession.members.Count)
                {
                    List<Member> newMembersList = _selectedSession.members;
                    members.Add(Storage.signedInMember);
                    _sessionController.Update(selectedSession, entity => { entity.members = newMembersList; });
                    _sessionController.Complete();
                }
                else
                {
                    MessageBox.Show("This session is full!");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LeaveSession(object obj)
        {
            try
            {
                if (selectedSession.members.Contains(Storage.signedInMember))
                {
                    List<Member> newMembersList = _selectedSession.members;
                    members.Remove(Storage.signedInMember);
                    _sessionController.Update(selectedSession, entity => { entity.members = newMembersList; });
                    _sessionController.Complete();
                }
                else
                {
                    MessageBox.Show("Not part of session!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
