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
	/// Логика взаимодействия для TasksUserControl.xaml
	/// </summary>
	public partial class TasksUserControl : UserControl
	{
		#region Properties
		#region Services
		private TasksDbService _taskDbService = new TasksDbService();
		private TasksUsersClaimDbService _taskUserClaimDbService = new TasksUsersClaimDbService();
		private UsersClaimsDbService _usersClaimsDbService = new UsersClaimsDbService();
		#endregion

		private List<StackPanel> _tasksStackPanels = new List<StackPanel>();

		private List<TaskDbReadModel> _tasks = new List<TaskDbReadModel>();
		#endregion

		public TasksUserControl()
		{
			InitializeComponent();
			_tasksStackPanels.Add(TasksStackPanel);
		}

		public async Task AddTaskOnPnael(Guid taskId)
		{
			var currentTask = await _taskDbService.GetTaskById(taskId);

			if (currentTask is null) return;

			Button button = new Button
			{
				Content = currentTask.name,
				Style = (Style)Application.Current.Resources["DefaultButtonStyle"],
				Width = 315,
				Height = 150,
				Margin = new Thickness(3),
				FontSize = 10
			};

			button.Click += async (s, e) =>
			{
				Repository.SelectTask = currentTask;
				MainWindowService.GetUserControlInMainWindow<TaskViewUserControl>().NameTextBox.Text = currentTask.name;
				MainWindowService.GetUserControlInMainWindow<TaskViewUserControl>().DescriptionTextBox.Text = currentTask.description;
				var users = (await _taskUserClaimDbService.GetUsersByTask(currentTask.id)).Where(x => x.id != Repository.User.id);
				MainWindowService.GetUserControlInMainWindow<TaskViewUserControl>().ClearParticipants();
				foreach (var user in users)
				{
					var userTask = (await _taskUserClaimDbService.GetTasksByUserId(user.id)).Where(x => x.task_id == currentTask.id).FirstOrDefault();
					if(userTask is not null)
					{
						var userFio = (await _usersClaimsDbService.GetUserParameters(user.id)).Where(x => x.parameter_id == Guid.Parse(FIO)).FirstOrDefault().value;
						MainWindowService.GetUserControlInMainWindow<TaskViewUserControl>().AddUserInParticipant(user, userFio);
					}
				}
				MainWindowService.GetUserControlInMainWindow<TaskViewUserControl>().Visibility = Visibility.Visible;
			};

			if (_tasksStackPanels.Last().Children.Count == 6)
			{
				StackPanel taskPanel = new StackPanel
				{
					Orientation = Orientation.Horizontal,
				};
				taskPanel.Children.Add(button);
				MainTasksStackPanel.Children.Add(taskPanel);
				_tasksStackPanels.Add(taskPanel);
			}
			else
			{
				_tasksStackPanels.Last().Children.Add(button);
			}
		}

		public void ClearTasks()
		{
			_tasksStackPanels.Clear();
			for(int i = 1; i < MainTasksStackPanel.Children.Count; i++)
			{
				MainTasksStackPanel.Children.RemoveAt(i);
			}
			for(int i = TasksStackPanel.Children.Count - 1; i != 0; i--)
			{
				TasksStackPanel.Children.RemoveAt(i);
			}
			_tasksStackPanels.Add(TasksStackPanel);
		}

		private void AddTaskButton_Click(object sender, RoutedEventArgs e)
		{
			MainWindowService.GetUserControlInMainWindow<CreateTaskUserControl>()!.Visibility = Visibility.Visible;
			MainWindowService.GetUserControlInMainWindow<MainProjectPanelUserControl>()!.Visibility = Visibility.Collapsed;
		}
	}
}
