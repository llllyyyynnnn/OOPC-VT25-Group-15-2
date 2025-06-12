using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataManager.Logic;
using System.Windows;
using static DataManager.Entities;
using DataManager;

namespace PanelWindow
{
    public static class WindowFunctions
    {
        public static class LogInUI
        {
            public static bool LogIn(DataManager.Logic.Controllers.Coaches controller, string email, string password)
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

            public static bool Register(DataManager.Logic.Controllers.Coaches controller, DataManager.Entities.Coach coach)
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
            public static bool Modify(DataManager.Logic.Controllers.Members controller, DataManager.Entities.Member member, DataManager.Entities.Member memberModifiedData)
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
                        entity.paymentStatus = memberModifiedData.paymentStatus;
                    });

                    controller.Complete();
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return false;
                }
            }

            public static bool Register(DataManager.Logic.Controllers.Members controller, DataManager.Entities.Member memberData)
            {
                try
                {
                    controller.Register(memberData);
                    controller.Complete();
                    MessageBox.Show("Successfully created new member");
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return false;
                }
            }

            public static bool Delete(DataManager.Logic.Controllers.Members controller, DataManager.Entities.Member member)
            {
                try
                {
                    MessageBoxResult res = MessageBox.Show($"Your request to delete {member.mailAddress} with id {member.id} is irreversible. Are you sure you want to continue?", "Warning", MessageBoxButton.YesNo);
                    if (res == MessageBoxResult.Yes)
                    {
                        controller.Delete(member);
                        controller.Complete();

                        return true;
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return false;
                }
            }
        }

        public static class GearUI
        {
            public static bool Modify(DataManager.Logic.Controllers.Gears controller, DataManager.Entities.Gear gear, DataManager.Entities.Gear gearModifiedData)
            {
                try
                {
                    controller.Update(gear, entity =>
                    {
                        entity.name = gearModifiedData.name;
                        entity.condition = gearModifiedData.condition;
                        entity.available = gearModifiedData.available;
                        entity.condition = gearModifiedData.condition;
                    });

                    controller.Complete();
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return false;
                }
            }

            public static bool Register(DataManager.Logic.Controllers.Gears controller, DataManager.Entities.Gear gearData)
            {
                try
                {
                    controller.Register(gearData);
                    controller.Complete();
                    MessageBox.Show("Successfully created new gear");
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return false;
                }
            }

            public static bool Delete(DataManager.Logic.Controllers.Gears controller, DataManager.Entities.Gear gear)
            {
                try
                {
                    MessageBoxResult res = MessageBox.Show($"Your request to delete {gear.name} with id {gear.id} is irreversible. Are you sure you want to continue?", "Warning", MessageBoxButton.YesNo);
                    if (res == MessageBoxResult.Yes)
                    {
                        controller.Delete(gear);
                        controller.Complete();

                        return true;
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return false;
                }
            }
        }

        public static class GearLoansUI
        {
            public static bool Modify(DataManager.Logic.Controllers.GearLoans gearLoanController, DataManager.Entities.GearLoan gearLoan, DataManager.Entities.GearLoan gearLoanModifiedData)
            {
                try
                {
                    gearLoanController.Update(gearLoan, entity =>
                    {
                        entity.gear = gearLoanModifiedData.gear;
                        entity.returnDate = gearLoanModifiedData.returnDate;
                        entity.loanDate = gearLoanModifiedData.loanDate;
                        entity.loanOwner = gearLoanModifiedData.loanOwner;
                    });

                    gearLoanController.Complete();
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return false;
                }
            }

            public static bool Register(DataManager.Logic.Controllers.GearLoans gearLoanController, DataManager.Logic.Controllers.Gears gearController, DataManager.Entities.GearLoan gearData)
            {
                try
                {
                    if (gearData.gear.available <= 0)
                        throw new Exception("Gear is not available.");

                    gearController.Update(gearData.gear, entity =>
                    {
                        entity.name = entity.name;
                        entity.condition = entity.condition;
                        entity.available -= 1;
                        entity.condition = entity.condition;
                    });

                    gearLoanController.Register(gearData);
                    gearLoanController.Complete();
                    gearController.Complete();
                    MessageBox.Show("Successfully created new gear loan");
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return false;
                }
            }

            public static bool Delete(DataManager.Logic.Controllers.GearLoans gearLoanController, DataManager.Logic.Controllers.Gears gearController, DataManager.Entities.GearLoan gearLoan)
            {
                try
                {
                    if (gearLoan == null)
                        return false;

                    MessageBoxResult res = MessageBox.Show($"Your request to delete {gearLoan.gear.name} for {gearLoan.loanOwner.firstName} {gearLoan.loanOwner.lastName} with id {gearLoan.id} is irreversible. Are you sure you want to continue?", "Warning", MessageBoxButton.YesNo);
                    if (res == MessageBoxResult.Yes)
                    {
                        gearController.Update(gearLoan.gear, entity =>
                        {
                            entity.name = entity.name;
                            entity.condition = entity.condition;
                            entity.available += 1;
                            entity.condition = entity.condition;
                        });
                        gearLoanController.Delete(gearLoan);
                        gearLoanController.Complete();

                        return true;
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return false;
                }
            }
        }

        public static class SessionUI
        {
            public static bool Modify(DataManager.Logic.Controllers.Sessions controller, DataManager.Entities.Session session, DataManager.Entities.Session sessionModifiedData)
            {
                try
                {
                    if (sessionModifiedData.members.Count > sessionModifiedData.participants)
                        throw new Exception("Too many members");

                    controller.Update(session, entity =>
                    {
                        entity.activity = sessionModifiedData.activity;
                        entity.caloriesBurnt = sessionModifiedData.caloriesBurnt;
                        entity.date = sessionModifiedData.date;
                        entity.description = sessionModifiedData.description;
                        entity.coach = sessionModifiedData.coach;
                        entity.time = sessionModifiedData.time;
                        entity.location = sessionModifiedData.location;
                        entity.participants = sessionModifiedData.participants;
                        entity.members = sessionModifiedData.members;
                    });

                    controller.Complete();
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex.Message}, {ex.InnerException}");
                    return false;
                }
            }

            public static bool Register(DataManager.Logic.Controllers.Sessions controller, DataManager.Entities.Session sessionData)
            {
                try
                {
                    if (sessionData.members.Count > sessionData.participants)
                        throw new Exception("Too many members");

                    controller.Register(sessionData);
                    controller.Complete();
                    MessageBox.Show("Successfully created new session");
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex.Message}, {ex.InnerException}");
                    return false;
                }
            }

            public static bool Delete(DataManager.Logic.Controllers.Sessions controller, DataManager.Entities.Session session)
            {
                try
                {
                    MessageBoxResult res = MessageBox.Show($"Your request to delete {session.activity} with id {session.id} is irreversible. Are you sure you want to continue?", "Warning", MessageBoxButton.YesNo);
                    if (res == MessageBoxResult.Yes)
                    {
                        controller.Delete(session);
                        controller.Complete();

                        return true;
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return false;
                }
            }
        }
    }
}
