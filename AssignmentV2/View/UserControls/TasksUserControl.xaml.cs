using System.Windows;
using System.Windows.Controls;
using AssignmentV2.ReadModels;
using AssignmentV2.Services;
using AssignmentV2.Services.DataBase;
using AssignmentV2.View.UserControls.ProjectPanel;

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
			//StackPanel stackPanel = new StackPanel
			//{
			//	Orientation = Orientation.Horizontal,
			//};
			//Button button = new Button
			//{
			//	Name = "AddTaskButton",
			//	Width = 310,
			//	Height = 150,
			//	Style = (Style)Application.Current.Resources["DefaultButtonStyle"],
			//	Content = "+",
			//	FontSize = 25,
			//	HorizontalAlignment = HorizontalAlignment.Center,
			//	Margin = new Thickness(3),
			//};
			//button.Click += (s, e) =>
			//{
			//	MainWindowService.GetUserControlInMainWindow<CreateTaskUserControl>()!.Visibility = Visibility.Visible;
			//	MainWindowService.GetUserControlInMainWindow<MainProjectPanelUserControl>()!.Visibility = Visibility.Collapsed;
			//};
			//stackPanel.Children.Add(button);
			_tasksStackPanels.Clear();
			for(int i = 1; i < TasksStackPanel.Children.Count; i++)
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
