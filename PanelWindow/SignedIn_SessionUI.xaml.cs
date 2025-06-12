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
using static DataManager.Handlers;

namespace PanelWindow
{
    public partial class SignedIn_SessionUI : Window
    {
        private readonly DataManager.Handlers.Controllers.Sessions _sessionsController;
        private readonly DataManager.Handlers.Controllers.Coaches _coachesController;
        private readonly DataManager.Handlers.Controllers.Members _membersController;

        private bool _modifyingSession = false;
        private readonly DataManager.Entities.Session _session;

        private List<Member> membersInSession = new List<Member>();
        private List<Member> membersNotInSession = new List<Member>();

        private void RefreshLists()
        {
            sessionCurrentMembersList.ItemsSource = membersInSession.Select(member => new { memberId = member.id, memberInformation = $"{member.firstName} {member.lastName}: In this session" }).ToList();
            sessionCoach.ItemsSource = _coachesController.GetCoaches().Select(coach => new { coachId = coach.id, coachInformation = $"{coach.firstName} {coach.lastName}: {coach.specialisation}" }).ToList();
            sessionMembersList.ItemsSource = membersNotInSession.Select(member => new { memberId = member.id, memberInformation = $"{member.firstName} {member.lastName}: Not in this session" }).ToList();
        }

        private void SyncMemberListsToSession(int id)
        {
            if (_sessionsController == null)
                return;

            try
            {
                membersInSession = _sessionsController.GetMembers(id).ToList();

                sessionCurrentMembersList.ItemsSource = membersInSession
                    .Select(member => new {
                        memberId = member.id,
                        memberInformation = $"{member.firstName} {member.lastName}: In this session"
                    }).ToList();

                HashSet<int> sessionMemberIds = membersInSession.Select(m => m.id).ToHashSet();
                membersNotInSession.RemoveAll(member => sessionMemberIds.Contains(member.id));
            }
            catch (Exception ex)
            {
                
            }

            RefreshLists();
        }

        public SignedIn_SessionUI(DataManager.Handlers.Controllers.Sessions sessionsController, DataManager.Handlers.Controllers.Coaches coachesController, DataManager.Handlers.Controllers.Members membersController, DataManager.Entities.Session session = null)
        {
            InitializeComponent();
            _sessionsController = sessionsController;
            _coachesController = coachesController;
            _membersController = membersController;

            membersNotInSession = (List<Member>)_membersController.GetMembers();
            RefreshLists();

            if (session != null)
            {
                _session = session;
                TitleLabel.Content = $"Modifying session ({session.id})";

                sessionActivity.Text = session.activity;
                sessionCalories.Text = session.caloriesBurnt.ToString();
                sessionDate.SelectedDate = session.date;
                sessionCoach.SelectedValue = session.coach.id;
                sessionDescription.Text = session.description;
                sessionTime.Text = session.time.ToString("HH:mm");
                sessionLocation.Text = session.location;
                sessionParticipants.Text = session.participants.ToString();

                SyncMemberListsToSession(session.id);

                _modifyingSession = true;
            }
            else
                TitleLabel.Content = $"Creating new session";
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Coach selectedCoach = _coachesController.GetCoachById((int)sessionCoach.SelectedValue);
                if (_modifyingSession)
                {

                    WindowFunctions.SessionUI.Modify(_sessionsController, _session, new Entities.Session
                    {
                        activity = sessionActivity.Text,
                        caloriesBurnt = Int32.Parse(sessionCalories.Text),
                        coach = selectedCoach,
                        description = sessionDescription.Text,
                        time = TimeOnly.ParseExact(sessionTime.Text, "HH:mm"),
                        location = sessionLocation.Text,
                        participants = Int32.Parse(sessionParticipants.Text),
                        date = sessionDate.SelectedDate.Value,
                        members = membersInSession
                    });
                }
                else
                {
                    WindowFunctions.SessionUI.Register(_sessionsController, new Entities.Session
                    {
                        activity = sessionActivity.Text,
                        caloriesBurnt = Int32.Parse(sessionCalories.Text),
                        coach = selectedCoach,
                        description = sessionDescription.Text,
                        time = TimeOnly.ParseExact(sessionTime.Text, "HH:mm"),
                        location = sessionLocation.Text,
                        participants = Int32.Parse(sessionParticipants.Text),
                        date = sessionDate.SelectedDate.Value,
                        members = membersInSession
                    });
                }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }

            this.Close();
        }

        private void addMember_Click(object sender, RoutedEventArgs e)
        {
            Member member = _membersController.GetMemberById((int)sessionMembersList.SelectedValue);

            membersInSession.Add(member);
            membersNotInSession.Remove(member);

            RefreshLists();
        }

        private void removeMember_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Member member = _membersController.GetMemberById((int)sessionCurrentMembersList.SelectedValue);

                membersInSession.Remove(member);
                membersNotInSession.Add(member);

                RefreshLists();
            }
            catch (Exception ex) { }
        }
    }
}
