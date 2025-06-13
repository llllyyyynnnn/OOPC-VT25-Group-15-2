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
        private readonly DataManager.Logic.Controllers.GearLoans _gearLoansController;

        public ICommand cmdSaveNewPinCode { get; private set; }
        public ICommand cmdJoinSession { get; private set; }
        public ICommand cmdLeaveSession { get; private set; }
        public ICommand cmdLogOut { get; private set; }

        public Action actionLogOut { get; set; }


        private string _currentPinCode = "Current password";
        public string currentPinCode { get => _currentPinCode; set { _currentPinCode = value; OnPropertyChanged(); } }
        private string _newPinCode = "New password";
        public string newPinCode { get => _newPinCode; set { _newPinCode = value; OnPropertyChanged(); } }


        public string mailAddress { get; private set; }
        public string welcomeMember { get; private set; }

        private Member _selectedMember;
        public Member selectedMember { get => _selectedMember; set { _selectedMember = value; OnPropertyChanged(); } }
        private ObservableCollection<Member> _members;
        public ObservableCollection<Member> members { get => _members; set { _members = value; OnPropertyChanged(); } }

        private ObservableCollection<Session> _sessions;
        public ObservableCollection<Session> sessions { get => _sessions; set { _sessions = value; OnPropertyChanged(); } }

        private ObservableCollection<GearLoan> _loans;
        public ObservableCollection<GearLoan> loans { get => _loans; set { _loans = value; OnPropertyChanged(); } }

        private string _sessionDetails = "none";
        public string sessionDetails { get => _sessionDetails; set { _sessionDetails = value; OnPropertyChanged(); } }

        private string _sessionJoinStatus = "none";
        public string sessionJoinStatus { get => _sessionJoinStatus; set { _sessionJoinStatus = value; OnPropertyChanged(); } }


        private Session _selectedSession;
        public Session selectedSession
        {
            get => _selectedSession;
            set
            {
                if (_selectedSession != value)
                {
                    _selectedSession = value;
                    OnPropertyChanged();

                    if (_selectedSession != null)
                    {
                        int memberCount = 0;
                        bool inSession = false;

                        if (_selectedSession.members != null)
                        {
                            memberCount = _selectedSession.members.Count;
                            inSession = selectedSession.members.Contains(Storage.signedInMember);
                        }

                        sessionDetails = $"There are {memberCount}/{_selectedSession.participants} in this session.";
                        sessionJoinStatus = inSession ? "You have joined this session" : "You are not part of this session";
                    }
                    else
                    {
                        sessionDetails = "No session selected";
                        sessionJoinStatus = string.Empty;
                    }
                }
            }
        }


        private ObservableCollection<Session> _upcomingSessions;
        public ObservableCollection<Session> upcomingSessions { get => _upcomingSessions; set { _upcomingSessions = value; OnPropertyChanged(); } }
        private ObservableCollection<Session> _previousSessions;
        public ObservableCollection<Session> previousSessions { get => _previousSessions; set { _previousSessions = value; OnPropertyChanged(); } }

        private void RefreshLists()
        {
            members = new ObservableCollection<Member>(_memberController.GetMembers());
            sessions = new ObservableCollection<Session>(_sessionController.GetSessions());
            loans = new ObservableCollection<GearLoan>(_gearLoansController.GetGearLoans());

            var upcomingSessionsFiltered = sessions
                .Where(session => session.date > DateTime.Today
                                  && session.members != null
                                  && session.members.Count > 0
                                  && session.members.Contains(Storage.signedInMember));
            upcomingSessions = new ObservableCollection<Session>(upcomingSessionsFiltered);

            var previousSessionsFiltered = sessions
    .Where(session => session.date < DateTime.Today
                      && session.members != null
                      && session.members.Count > 0
                      && session.members.Contains(Storage.signedInMember));
            previousSessions = new ObservableCollection<Session>(previousSessionsFiltered);
        }

        public MemberPanel()
        {
            _uow = new DataManager.Logic.UnitOfWork(Storage.ctx);
            _memberController = new DataManager.Logic.Controllers.Members(_uow);
            _sessionController = new DataManager.Logic.Controllers.Sessions(_uow);
            _gearLoansController = new DataManager.Logic.Controllers.GearLoans(_uow);

            cmdSaveNewPinCode = new RelayCommand(SaveNewPinCode);
            cmdJoinSession = new RelayCommand(JoinSession);
            cmdLeaveSession = new RelayCommand(LeaveSession);
            cmdLogOut = new RelayCommand(LogOut);

            mailAddress = Storage.signedInMember.mailAddress;
            welcomeMember = $"Welcome back, {Storage.signedInMember.firstName} {Storage.signedInMember.lastName}";

            RefreshLists();
        }

        private void LogOut(object obj)
        {
            Storage.signedInMember = null;
            actionLogOut.Invoke();
        }

        private void JoinSession(object obj)
        {
            try
            {
                _sessionController.JoinSession(Storage.signedInMember, selectedSession);
                _sessionController.Complete();

                RefreshLists();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " " + ex.InnerException);
            }
        }

        private void LeaveSession(object obj)
        {
            try
            {
                _sessionController.LeaveSession(Storage.signedInMember, selectedSession);
                _sessionController.Complete();

                RefreshLists();
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
                _memberController.ChangePassword(Storage.signedInMember, currentPinCode, newPinCode);
                _memberController.Complete();

                currentPinCode = "Current password";
                newPinCode = "New password";
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
