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
using static DataManager.Logic;

namespace PanelWindow
{
    public partial class SignedIn : Window
    {
        private readonly DataManager.Logic.Controllers.Members _memberController;
        private readonly DataManager.Logic.Controllers.Gears _gearController;
        private readonly DataManager.Logic.Controllers.GearLoans _gearLoansController;

        private readonly DataManager.Logic.Controllers.Sessions _sessionsController;
        private readonly DataManager.Logic.Controllers.Coaches _coachesController;

        public SignedIn()
        {
            InitializeComponent();
            signedInLabel.Content = $"Currently signed in as: {Storage.signedInCoach.mailAddress}";

            // We could use a global unit of work for all of these, but if one command is started on member and not finished, and we then start another command in gear and that finshed before, then we run Complete before member and risk inconsistent data.
            _memberController = new DataManager.Logic.Controllers.Members(new DataManager.Logic.UnitOfWork(Storage.ctx));
            _gearController = new DataManager.Logic.Controllers.Gears(new DataManager.Logic.UnitOfWork(Storage.ctx));
            _gearLoansController = new DataManager.Logic.Controllers.GearLoans(new DataManager.Logic.UnitOfWork(Storage.ctx));
            _sessionsController = new DataManager.Logic.Controllers.Sessions(new DataManager.Logic.UnitOfWork(Storage.ctx));
            _coachesController = new DataManager.Logic.Controllers.Coaches(new DataManager.Logic.UnitOfWork(Storage.ctx));

            RefreshMembersList();
            RefreshGearList();
            RefreshGearLoansList();
            RefreshSessionList();
        }

        private void RefreshMembersList() => membersList.ItemsSource = _memberController.GetMembers();
        private void RefreshGearList() => gearList.ItemsSource = _gearController.GetGear();
        private void RefreshGearLoansList() => gearLoansList.ItemsSource = _gearLoansController.GetGearLoans();
        private void RefreshSessionList() => sessionsList.ItemsSource = _sessionsController.GetSessions();

        private void refreshSessionsButton_Click(object sender, RoutedEventArgs e) => RefreshSessionList();
        private void refreshGearButton_Click(object sender, RoutedEventArgs e) => RefreshGearList();
        private void refreshGearLoansButton_Click(object sender, RoutedEventArgs e) => RefreshGearLoansList();
        private void refreshMembersButton_Click(object sender, RoutedEventArgs e) => RefreshMembersList();

        private void addMemberButton_Click(object sender, RoutedEventArgs e)
        {
            SignedIn_MemberUI memberUI = new SignedIn_MemberUI(_memberController);
            memberUI.ShowDialog();
            RefreshMembersList();
        }

        private void deleteMemberButton_Click(object sender, RoutedEventArgs e)
        {
            if(membersList.SelectedItem != null && membersList.SelectedItem is Entities.Member member)
                if(WindowFunctions.MemberUI.Delete(_memberController, member))
                    RefreshMembersList();
        }

        private void editMemberButton_Click(object sender, RoutedEventArgs e)
        {
            if (membersList.SelectedItem != null && membersList.SelectedItem is Entities.Member member) {
                SignedIn_MemberUI memberUI = new SignedIn_MemberUI(_memberController, member);
                memberUI.ShowDialog();
                RefreshMembersList();
            }
            else
            {
                MessageBox.Show("Invalid item selected");
            }
        }

        private void logOutButton_Click(object sender, RoutedEventArgs e)
        {
            Storage.signedInCoach = null;
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void addGearButton_Click(object sender, RoutedEventArgs e)
        {
            SignedIn_GearUI gearUI = new SignedIn_GearUI(_gearController);
            gearUI.ShowDialog();
            RefreshGearList();
        }
        private void modifyGearButton_Click(object sender, RoutedEventArgs e)
        {
            if (gearList.SelectedItem != null && gearList.SelectedItem is Entities.Gear gear)
            {
                SignedIn_GearUI gearUI = new SignedIn_GearUI(_gearController, gear);
                gearUI.ShowDialog();
                RefreshGearList();
            }
        }
        private void removeGearButton_Click(object sender, RoutedEventArgs e)
        {
            if (gearList.SelectedItem != null && gearList.SelectedItem is Entities.Gear gear)
                if (WindowFunctions.GearUI.Delete(_gearController, gear))
                    RefreshGearList();
        }

        private void addSessionButton_Click(object sender, RoutedEventArgs e)
        {
            SignedIn_SessionUI sessionUI = new SignedIn_SessionUI(_sessionsController, _coachesController, _memberController);
            sessionUI.ShowDialog();
            RefreshSessionList();
        }
        private void modifySessionButton_Click(object sender, RoutedEventArgs e)
        {
            if (sessionsList.SelectedItem != null && sessionsList.SelectedItem is Entities.Session session)
            {
                SignedIn_SessionUI sessionUI = new SignedIn_SessionUI(_sessionsController, _coachesController, _memberController, session);
                sessionUI.ShowDialog();
                RefreshSessionList();
            }
        }
        private void removeSessionButton_Click(object sender, RoutedEventArgs e)
        {
            if (sessionsList.SelectedItem != null && sessionsList.SelectedItem is Entities.Session session)
                if (WindowFunctions.SessionUI.Delete(_sessionsController, session))
                    RefreshSessionList();
        }

        private void loanGearButton_Click(object sender, RoutedEventArgs e)
        {
            if (gearList.SelectedItem != null && gearList.SelectedItem is Entities.Gear gear)
            {
                SignedIn_GearLoanUI gearLoanUI = new SignedIn_GearLoanUI(_gearLoansController, _memberController, _gearController, gear);
                gearLoanUI.ShowDialog();
                RefreshGearLoansList();
                RefreshGearList();
            }
        }

        private void removeGearLoanButton_Click(object sender, RoutedEventArgs e)
        {
            if (gearLoansList.SelectedItem != null && gearLoansList.SelectedItem is Entities.GearLoan gearLoan)
                if (WindowFunctions.GearLoansUI.Delete(_gearLoansController, _gearController, gearLoan))
                {
                    RefreshGearList();
                    RefreshGearLoansList();
                }
        }
    }
}
