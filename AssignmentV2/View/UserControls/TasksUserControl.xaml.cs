using System.Windows;
using System.Windows.Controls;

namespace AssignmentV2.View.UserControls
{
	/// <summary>
	/// Логика взаимодействия для TasksUserControl.xaml
	/// </summary>
	public partial class TasksUserControl : UserControl
	{
		private List<StackPanel> _tasksStackPanels = new List<StackPanel>();

		public TasksUserControl()
		{
			InitializeComponent();
			_tasksStackPanels.Add(TasksStackPanel);
		}

		private void AddTaskButton_Click(object sender, RoutedEventArgs e)
		{
			Button button = new Button
			{
				Content = "test",
				Style = (Style)Application.Current.Resources["DefaultButtonStyle"],
				Width = 80,
				Height = 50,
				Margin = new Thickness(3),
				FontSize = 10
			};

			if (_tasksStackPanels.Last().Children.Count == 8)
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
	}
}
