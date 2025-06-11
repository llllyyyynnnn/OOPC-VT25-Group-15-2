using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataManager.Handlers;
using System.Windows;
using static DataManager.Entities;

namespace PanelWindow
{
    public static class ViewModels
    {
        public static class LogInUI
        {
            public static bool LogIn(DataManager.Handlers.Controllers.Coaches controller, string email, string password)
            {
                try
                {
                    Storage.signedInCoach = controller.Login(email, password);
                    if (Storage.signedInCoach != null)
                        return true;
                    else
                        throw new Exception("Failed to sign in");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return false;
                }
            }

            public static bool Register(DataManager.Handlers.Controllers.Coaches controller, DataManager.Entities.Coach coach)
            {
                try
                {
                    controller.Register(coach);
                    controller.Complete();
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex.Message} ({ex.InnerException})");
                    return false;
                }
            }
        }

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

        public static class DataUI
        {

        }
    }
}
