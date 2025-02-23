using System.Windows;
using System.Windows.Controls;
using AssignmentV2.Services;

namespace AssignmentV2.View.UserControls
{
	/// <summary>
	/// Логика взаимодействия для CreateTaskUserControl.xaml
	/// </summary>
	public partial class CreateTaskUserControl : UserControl
	{
		#region Properties
		private TaskService _taskService;
		#endregion

		public CreateTaskUserControl()
		{
			InitializeComponent();
			_taskService = new TaskService();
		}

		private void CancelButton_Click(object sender, RoutedEventArgs e)
		{
			UserControl.Visibility = Visibility.Collapsed;
		}

		private async void CreateButton_Click(object sender, RoutedEventArgs e)
		{
			await _taskService.CreateTask(Repository.User.id, Repository.SelectProject.id, NameTextBox.Text, DescriptionTextBox.Text);
		}
	}
}
