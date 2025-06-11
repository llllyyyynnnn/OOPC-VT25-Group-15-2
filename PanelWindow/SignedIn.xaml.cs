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
        private readonly DataManager.Handlers.Controllers.Members _memberController;
        private readonly DataManager.Handlers.Controllers.Gears _gearController;
        private readonly DataManager.Handlers.Controllers.Sessions _sessionsController;
        private readonly DataManager.Handlers.Controllers.Coaches _coachesController;


        public SignedIn()
        {
            InitializeComponent();
            signedInLabel.Content = $"Currently signed in as: {Storage.signedInCoach.mailAddress}";

            _memberController = new DataManager.Handlers.Controllers.Members(Storage.uow);
            _gearController = new DataManager.Handlers.Controllers.Gears(Storage.uow);
            _sessionsController = new DataManager.Handlers.Controllers.Sessions(Storage.uow);
            _coachesController = new DataManager.Handlers.Controllers.Coaches(Storage.uow);

            RefreshMembersList();
            RefreshGearList();
            RefreshSessionList();
        }

        private void RefreshMembersList() => membersList.ItemsSource = _memberController.GetMembers();
        private void RefreshGearList() => gearList.ItemsSource = _gearController.GetGear();
        private void RefreshSessionList() => sessionsList.ItemsSource = _sessionsController.GetSessions();

        private void refreshSessionsButton_Click(object sender, RoutedEventArgs e) => RefreshSessionList();
        private void refreshGearButton_Click(object sender, RoutedEventArgs e) => RefreshGearList();
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

        private void refreshGearLoansButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void loanGearButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void removeGearLoanButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
