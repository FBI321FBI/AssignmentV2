using AssignmentV2.Handlers.Events;
using AssignmentV2.ViewModel.ProjectPanel;
using System.Windows;

namespace AssignmentV2
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			RegisteredEvents();
			SetDefaultSettings();
		}

		private void SetDefaultSettings()
		{
			#region ProjectPanelViewModel
			var projectPanelViewModel = (ProjectPanelViewModel)MainProjectPanelUserControl.ProjectPanelUserControl.DataContext;
			projectPanelViewModel.SetDefaultSettings();
			projectPanelViewModel.CloseProjectPanel();
			#endregion
		}

		private void RegisteredEvents()
		{
			authorizationUserControl.AuthorizationViewModel.OnLoggedIn += UserLoggedIn.Handler;
		}

		private void MinimizedButton_Click(object sender, RoutedEventArgs e)
		{
			Application.Current.MainWindow.WindowState = WindowState.Minimized;
		}

		private void CloseButton_Click(object sender, RoutedEventArgs e)
		{
			Application.Current.MainWindow.Close();
		}
	}
}