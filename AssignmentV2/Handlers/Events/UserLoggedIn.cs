using System.Windows;
using AssignmentV2.ReadModels.Tables;

namespace AssignmentV2.Handlers.Events
{
	public static class UserLoggedIn
	{
		public static void Handler(User user)
		{
			Application.Current.Dispatcher.Invoke(new Action(() =>
			{
				var mainWindow = Application.Current.Windows.OfType<MainWindow>().SingleOrDefault(x => x.IsActive);
				if (mainWindow != null)
				{
					mainWindow.authorizationUserControl.Visibility = Visibility.Hidden;
					mainWindow.MainProjectPanelUserControl.Visibility = Visibility.Visible;
				}
			}));
		}
	}
}
