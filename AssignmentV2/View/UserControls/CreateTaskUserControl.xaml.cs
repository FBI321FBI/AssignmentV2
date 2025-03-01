using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using AssignmentV2.ReadModels;
using AssignmentV2.Services;
using AssignmentV2.Services.DataBase;
using AssignmentV2.View.UserControls.ProjectPanel;
using static AssignmentV2.Constants.Parameters;

namespace AssignmentV2.View.UserControls
{
	/// <summary>
	/// Логика взаимодействия для CreateTaskUserControl.xaml
	/// </summary>
	public partial class CreateTaskUserControl : UserControl
	{
		#region Properties
		private TaskService _taskService;
		private UsersDbService _usersDbService;
		private UsersClaimsDbService _usersClaimsDbService;
		private MainWindow _mainWindow => Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();

		private List<UserReadModel> _usersInTask;
		#endregion

		public CreateTaskUserControl()
		{
			InitializeComponent();
			_taskService = new TaskService();
			_usersDbService = new UsersDbService();
			_usersInTask = new List<UserReadModel>();
			_usersClaimsDbService = new UsersClaimsDbService();
		}

		private void CancelButton_Click(object sender, RoutedEventArgs e)
		{
			UserControl.Visibility = Visibility.Collapsed;
			MainWindowService.GetUserControlInMainWindow<MainProjectPanelUserControl>()!.Visibility = Visibility.Visible;
		}

		private async void CreateButton_Click(object sender, RoutedEventArgs e)
		{
			var taskId = Guid.NewGuid();
			await _taskService.CreateTask(Repository.User.id, _usersInTask, taskId, Repository.SelectProject.id, NameTextBox.Text, DescriptionTextBox.Text);
			NameTextBox.Text = "";
			DescriptionTextBox.Text = "";
			_mainWindow.TasksUserControl.AddTaskOnPnael(taskId);
			UserControl.Visibility = Visibility.Collapsed;
			MainWindowService.GetUserControlInMainWindow<MainProjectPanelUserControl>()!.Visibility = Visibility.Visible;
		}

		private async void AddUserButton_Click(object sender, RoutedEventArgs e)
		{
			SelectUserWindow selectUserWindow = new SelectUserWindow();
			var users = (await _usersDbService.GetAllUsers()).Where(x => x.id != Repository.User?.id).ToList();
			foreach(var userInTask in _usersInTask)
			{
				for(int i = 0; i<users.Count; i++)
				{
					if (users[i].id == userInTask.id)
					{
						users.RemoveAt(i);
					}
				}
			}
			await selectUserWindow.LoadUsers(users);
			selectUserWindow.ShowDialog();

			if(selectUserWindow.SelectedUser is not null)
			{
				_usersInTask.Add(selectUserWindow.SelectedUser);
				var userFioParameter = (await _usersClaimsDbService.GetUserParameters(selectUserWindow.SelectedUser.id)).Where(x => x.parameter_id == Guid.Parse(FIO)).FirstOrDefault();
				Button button = new Button
				{
					Style = (Style)Application.Current.Resources["DefaultButtonStyle"],
					FontSize = 25,
					Content = userFioParameter?.value
				};
				button.Click += (e, a) =>
				{
					UsersStackPanel.Children.Remove(button);
				};

				UsersStackPanel.Children.Add(button);
			}

			selectUserWindow.Close();
        }
    }
}
