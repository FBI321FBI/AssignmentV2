using System.IO;
using System.Windows;
using System.Windows.Controls;
using AssignmentV2.ReadModels;

namespace AssignmentV2.View.UserControls
{
	/// <summary>
	/// Логика взаимодействия для TaskViewUserControl.xaml
	/// </summary>
	public partial class TaskViewUserControl : UserControl
	{
		private List<UserReadModel> usersInParticipant = new List<UserReadModel>();

		public TaskViewUserControl()
		{
			InitializeComponent();
		}

		public void AddUserInParticipant(UserReadModel user, string fio)
		{
			Button button = new Button
			{
				Style = (Style)Application.Current.Resources["DefaultButtonStyle"],
				FontSize = 25,
				Content = fio
			};
			button.Click += (e, a) =>
			{
				UsersStackPanel.Children.Remove(button);
				usersInParticipant.Remove(user);
			};
			UsersStackPanel.Children.Add(button);
			usersInParticipant.Add(user);
		}

		private void CloseViewButton_Click(object sender, RoutedEventArgs e)
		{
			Visibility = Visibility.Collapsed;
		}
	}
}
