using System.Linq;
using System.Windows;
using System.Windows.Controls;
using AssignmentV2.ReadModels;
using AssignmentV2.Services;
using AssignmentV2.Services.DataBase;
using AssignmentV2.Utilities;
using static AssignmentV2.Constants.Claims;
using static AssignmentV2.Constants.Parameters;

namespace AssignmentV2.View.UserControls
{
	/// <summary>
	/// Логика взаимодействия для TaskViewUserControl.xaml
	/// </summary>
	public partial class TaskViewUserControl : UserControl
	{
		private List<UserReadModel> usersInParticipant = new List<UserReadModel>();
		private TasksDbService taskDbService = new TasksDbService();
		private TaskService taskService = new TaskService();
		private TasksUsersClaimDbService tasksUsersCalimDbService = new TasksUsersClaimDbService();
		private UsersDbService usersDbService = new UsersDbService();
		private UsersClaimsDbService usersClaimsDbService = new UsersClaimsDbService();
		private TasksUserControl tasksUserControl => MainWindowService.GetUserControlInMainWindow<TasksUserControl>();
		private MainWindow? mainWindow => Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();

		public TaskViewUserControl()
		{
			InitializeComponent();
		}

		public void ClearParticipants()
		{
			usersInParticipant.Clear();
			UsersStackPanel.Children.Clear();
			UsersStackPanel.Children.Add(new Label
			{
				Content = "Участники",
				Margin = new Thickness(0,0,0,3),
				FontSize = 35,
				Style = (Style)Application.Current.Resources["LabelDefaultStyle"]
			});
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

		private async void SaveTaskButton_Click(object sender, RoutedEventArgs e)
		{
			var newEndDate = EndDateDatePicker.SelectedDate;
			if(newEndDate == null)
			{
				CustomMessageBox.Information("Укажите дату окончания задачи.");
				return;
			}
			await taskDbService.UpdateTaskById(Repository.SelectTask.id, NameTextBox.Text, DescriptionTextBox.Text, newEndDate ?? DateTime.Now, 0);
			var tasks = (await taskService.GetTasksByUserId(Repository.User.id)).Where(x => x.isDeleted == false && x.project_id == Repository.SelectProject.id);
			if (tasks is null) return;
			mainWindow.TasksUserControl.ClearTasks();
			foreach (var task in tasks)
			{
				mainWindow.TasksUserControl.AddTaskOnPnael(task.id);
			}

			var usersInTask = (await tasksUsersCalimDbService.GetUsersByTask(Repository.SelectTask.id)).Where(x => x.id == Repository.User.id);
			foreach (var userParticipant in usersInParticipant)
			{
				if(usersInTask.Where(x => x.id == userParticipant.id).FirstOrDefault() is not null)
				{
					await tasksUsersCalimDbService.CreateTaskUserClaim(new TasksUsersClaimDbReadModel
					{
						id = Guid.NewGuid(),
						user_id = userParticipant.id,
						task_id = Repository.SelectTask.id,
						claim_id = Guid.Parse(TASK_PARTICIPANT)
					});
				}
			}

			var users = (await usersDbService.GetAllUsers()).Where(x => x.id != Repository.User?.id).ToList();
			List<UserReadModel> usersForDelete = new List<UserReadModel>();
			var notInParcipiants = users.Where(x => !usersInParticipant.Any(y => y.id == x.id));
			foreach (var notInParcicpiant in notInParcipiants)
			{
				usersForDelete.Add(notInParcicpiant);
			}

			foreach (var item in notInParcipiants)
			{
				await tasksUsersCalimDbService.DeleteTaskByUserId(item.id, Repository.SelectTask.id);
			}
		}

		private async void AddUserButton_Click(object sender, RoutedEventArgs e)
		{
			SelectUserWindow selectUserWindow = new SelectUserWindow();
			var users = (await usersDbService.GetAllUsers()).Where(x => x.id != Repository.User?.id).ToList();
			var usersInTasks = (await tasksUsersCalimDbService.GetUsersByTask(Repository.SelectTask.id)).Where(x => x.id != Repository.User.id);
			List<UserReadModel> usersForAdd = new List<UserReadModel>();

			var notInParcipiants = users.Where(x => !usersInTasks.Any(y => y.id == x.id));
			foreach(var notInParcicpiant in notInParcipiants)
			{
				usersForAdd.Add(notInParcicpiant);
			}

			await selectUserWindow.LoadUsers(usersForAdd);
			selectUserWindow.ShowDialog();

			if (selectUserWindow.SelectedUser is not null)
			{
				var userFioParameter = (await usersClaimsDbService.GetUserParameters(selectUserWindow.SelectedUser.id)).Where(x => x.parameter_id == Guid.Parse(FIO)).FirstOrDefault();
				AddUserInParticipant(selectUserWindow.SelectedUser, userFioParameter.value);
				await tasksUsersCalimDbService.CreateTaskUserClaim(new TasksUsersClaimDbReadModel
				{
					id = Guid.NewGuid(),
					claim_id = Guid.Parse(TASK_PARTICIPANT),
					task_id = Repository.SelectTask.id,
					user_id = selectUserWindow.SelectedUser.id,
				});
			}
		}

		private async void DeleteButton_Click(object sender, RoutedEventArgs e)
		{
			var currentTask = Repository.SelectTask;
			await taskDbService.UpdateTaskById(currentTask.id, currentTask.name, currentTask.description, currentTask.end_date, 1);

			var tasks = (await taskService.GetTasksByUserId(Repository.User.id)).Where(x => x.isDeleted == false);
			if (tasks is null) return;
			mainWindow.TasksUserControl.ClearTasks();
			foreach (var task in tasks)
			{
				mainWindow.TasksUserControl.AddTaskOnPnael(task.id);
			}
			this.Visibility = Visibility.Collapsed;
		}
    }
}
