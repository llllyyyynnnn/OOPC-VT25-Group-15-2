using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataManager.Handlers;
using System.Windows;

namespace PanelWindow
{
    public static class ViewModels
    {
        public static class MemberUI
        {
            public static void Modify(DataManager.Handlers.Controllers.Members controller, DataManager.Entities.Member member, DataManager.Entities.Member memberModifiedData)
            {
                try
                {
                    controller.Update(member, entity =>
                    {
                        entity.firstName = memberModifiedData.firstName;
                        entity.lastName = memberModifiedData.lastName;
                        entity.birthDate = memberModifiedData.birthDate;
                        entity.phoneNumber = memberModifiedData.phoneNumber;
                        entity.mailAddress = memberModifiedData.mailAddress;
                        entity.pinCode = memberModifiedData.pinCode;
                    });

                    controller.Complete();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            public static void Register(DataManager.Handlers.Controllers.Members controller, DataManager.Entities.Member memberData)
            {
                try
                {
                    controller.Register(memberData);
                    controller.Complete();
                    MessageBox.Show("Successfully created new member");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
