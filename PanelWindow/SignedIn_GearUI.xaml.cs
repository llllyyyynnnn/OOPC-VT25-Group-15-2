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
    public partial class SignedIn_GearUI : Window
    {
        private readonly DataManager.Handlers.Controllers.Gears _controller;
        private bool _modifyingGear = false;
        private readonly DataManager.Entities.Gear _gear;

        public SignedIn_GearUI(DataManager.Handlers.Controllers.Gears controller, DataManager.Entities.Gear gear = null)
        {
            InitializeComponent();
            _controller = controller;

            if (gear != null)
            {
                _gear = gear;
                TitleLabel.Content = $"Modifying gear ({gear.id})";
                gearName.Text = gear.name;
                gearCategory.Text = gear.category;
                gearCondition.Text = gear.condition;
                gearAvailable.Text = gear.available.ToString();

                _modifyingGear = true;
            }
            else
                TitleLabel.Content = $"Creating new gear";
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            if(_modifyingGear)
            {
                WindowFunctions.GearUI.Modify(_controller, _gear, new Entities.Gear
                {
                    name = gearName.Text,
                    category = gearCategory.Text,
                    condition = gearCondition.Text,
                    available = Int32.Parse(gearAvailable.Text),
                });
            }
            else
            {
                WindowFunctions.GearUI.Register(_controller, new Entities.Gear
                {
                    name = gearName.Text,
                    category = gearCategory.Text,
                    condition = gearCondition.Text,
                    available = Int32.Parse(gearAvailable.Text),
                });
            }

            this.Close();
        }
    }
}
