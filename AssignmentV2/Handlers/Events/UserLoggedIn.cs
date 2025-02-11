using System.Windows;
using AssignmentV2.ReadModels.Tables;

namespace AssignmentV2.Handlers.Events
{
	public static class UserLoggedIn
	{
		public static void Handler(UserModel user)
		{
			Application.Current.Dispatcher.Invoke(new Action(() =>
			{
				Reposetory.SetUser(user);
				var mainWindow = Application.Current.Windows.OfType<MainWindow>().SingleOrDefault(x => x.IsActive);
				if (mainWindow != null)
				{
					if (!user.isCanCreateProject)
					{
						mainWindow.MainProjectPanelUserControl.ProjectPanelUserControl.AddProjectButton.Visibility = Visibility.Collapsed;
					}
					mainWindow.authorizationUserControl.Visibility = Visibility.Hidden;
					mainWindow.MainProjectPanelUserControl.Visibility = Visibility.Visible;
				}
			}));
		}
	}
}
