using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AssignmentV2.Services
{
	public static class MainWindowService
	{
		#region Public
		public static T? GetUserControlInMainWindow<T>() where T : UserControl
		{
			var mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();

			if (mainWindow != null)
			{
				return FindUserControl<T>(mainWindow);
			}

			return null;
		}
		#endregion

		#region Private
		private static T? FindUserControl<T>(DependencyObject parent)
		{
			for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
			{
				var child = VisualTreeHelper.GetChild(parent, i);
				if (child is T userControl)
				{
					return userControl;
				}

				var result = FindUserControl<T>(child);
				if (result != null)
				{
					return result;
				}
			}
			return default;
		}
		#endregion
	}
}
