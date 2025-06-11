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
using static DataManager.Entities;

namespace PanelWindow
{
    public partial class SignedIn_GearLoanUI : Window
    {
        private readonly DataManager.Handlers.Controllers.GearLoans _gearLoanController;
        private readonly DataManager.Handlers.Controllers.Members _membersController;
        private readonly DataManager.Handlers.Controllers.Gears _gearController;

        private readonly DataManager.Entities.Gear _gear;


        private bool _modifyingLoan = false;
        private readonly DataManager.Entities.GearLoan _gearLoan;
        private List<Member> membersList = new List<Member>();

        public SignedIn_GearLoanUI(DataManager.Handlers.Controllers.GearLoans gearLoanController, DataManager.Handlers.Controllers.Members memberController, DataManager.Handlers.Controllers.Gears gearController, DataManager.Entities.Gear gear,DataManager.Entities.GearLoan gearLoan = null)
        {
            InitializeComponent();

            _gearLoanController = gearLoanController;
            _membersController = memberController;
            _gearController = gearController;
            _gear = gear;

            membersList = (List<Member>)_membersController.GetMembers();
            gearLoansMembersList.ItemsSource = membersList.Select(member => new { memberId = member.id, memberInformation = $"{member.firstName} {member.lastName}: Not in this session" }).ToList();

            if (_gearLoan != null)
            {
                _gearLoan = gearLoan;
                TitleLabel.Content = $"Modifying gear loan ({gearLoan.id})";

                gearLoansMembersList.SelectedValue = _gearLoan.gear.id;
                gearLoanStartDate.SelectedDate = _gearLoan.loanDate;
                gearLoanReturnDate.SelectedDate = _gearLoan.returnDate;

                _modifyingLoan = true;
            }
            else
                TitleLabel.Content = $"Creating new gear loan";
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            if(_modifyingLoan)
            {
                WindowFunctions.GearLoansUI.Modify(_gearLoanController, _gearLoan, new Entities.GearLoan
                {
                    gear = _gear,
                    loanOwner = _membersController.GetMemberById((int)gearLoansMembersList.SelectedValue),
                    loanDate = gearLoanStartDate.SelectedDate.Value,
                    returnDate = gearLoanReturnDate.SelectedDate.Value
                });
            }
            else
            {
                WindowFunctions.GearLoansUI.Register(_gearLoanController, _gearController, new Entities.GearLoan
                {
                    gear = _gear,
                    loanOwner = _membersController.GetMemberById((int)gearLoansMembersList.SelectedValue),
                    loanDate = gearLoanStartDate.SelectedDate.Value,
                    returnDate = gearLoanReturnDate.SelectedDate.Value
                });
            }

            this.Close();
        }
    }
}
