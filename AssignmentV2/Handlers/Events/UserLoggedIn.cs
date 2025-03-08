using System.Windows;
using AssignmentV2.ReadModels;
using AssignmentV2.Services;
using AssignmentV2.Services.DataBase;
using AssignmentV2.ViewModel.ProjectPanel;

namespace AssignmentV2.Handlers.Events
{
	public static class UserLoggedIn
	{
		private static MainWindow _mainWindow = Application.Current.Windows.OfType<MainWindow>().SingleOrDefault(x => x.IsActive);

		public static void Handler(UserReadModel user)
		{
			Application.Current.Dispatcher.Invoke(new Action(() =>
			{
				Repository.SetUser(user);
				_mainWindow = Application.Current.Windows.OfType<MainWindow>().SingleOrDefault(x => x.IsActive);
				if (_mainWindow != null)
				{
					CheckRule(user);
					LoadProjects(user);
					_mainWindow.authorizationUserControl.Visibility = Visibility.Hidden;
					_mainWindow.MainProjectPanelUserControl.Visibility = Visibility.Visible;
				}
			}));
		}

		private static void CheckRule(UserReadModel user)
		{
			if (!user.isCanCreateProject)
			{
				_mainWindow.MainProjectPanelUserControl.ProjectPanelUserControl.AddProjectButton.Visibility = Visibility.Collapsed;
			}

			if (!user.isCanCreateTask)
			{
				_mainWindow.TasksUserControl.AddTaskButton.Visibility = Visibility.Collapsed;
			}

			if (user.isSa)
			{
				_mainWindow.AdminPanelUserControl.Visibility = Visibility.Visible;
			}
		}

		private static async Task LoadProjects(UserReadModel user)
		{
			ProjectService projectService = new ProjectService();
			ProjectDbService projectDbService = new ProjectDbService();
			ProjectPanelViewModel? projectPanelViewModel = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault()?.MainProjectPanelUserControl?.ProjectPanelUserControl.DataContext as ProjectPanelViewModel;
			var projects = await projectDbService.GetProjects();

			foreach (var project in projects)
			{
				projectPanelViewModel.AddProjectVisually(new ReadModels.Projects.ProjectReadModel
				{
					id = project.id,
					name = project.name
				});
			}
		}
	}
}
