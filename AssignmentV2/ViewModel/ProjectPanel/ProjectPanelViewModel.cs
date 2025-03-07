using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using AssignmentV2.ReadModels;
using AssignmentV2.ReadModels.Projects;
using AssignmentV2.Services;
using AssignmentV2.View.UserControls.ProjectPanel;
using CommunityToolkit.Mvvm.Input;

namespace AssignmentV2.ViewModel.ProjectPanel
{
	public partial class ProjectPanelViewModel : INotifyPropertyChanged
	{
		#region Properties
		private bool _isProjectPanelOpen = false;
		private double _defaultHeightProjectPanel;
		private double _defaultOpacityBlackOutProjectPanel;
		private ProjectPanelUserControl? _userControl => Application.Current.Windows.OfType<MainWindow>().FirstOrDefault()?.MainProjectPanelUserControl?.ProjectPanelUserControl;
		private MainProjectPanelUserControl? _mainProjectPanelUserControl => Application.Current.Windows.OfType<MainWindow>().FirstOrDefault()?.MainProjectPanelUserControl;
		private MainWindow? _mainWindow => Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
		private TaskService taskService => new TaskService();
		#endregion

		#region .ctor
		public ProjectPanelViewModel()
		{
			SetDefaultSettings();
		}
		#endregion

		#region Commands
		[RelayCommand]
		private void OpenOrCloseProjectPanelCommand()
		{
			if (_isProjectPanelOpen)
			{
				CloseProjectPanel();
			}
			else
			{
				OpenProjectPanel();
			}
		}

		[RelayCommand]
		private void AddNewProject()
		{
			if (_mainProjectPanelUserControl is not null)
			{
				_mainProjectPanelUserControl.EnterNameProjectUserControl.Visibility = Visibility.Visible;
			}
		}
		#endregion

		#region Public
		public void AddProjectVisually(ProjectReadModel project)
		{
			if (_userControl is not null)
			{
				var button = new Button
				{
					Content = project.name,
					Style = (Style)Application.Current.Resources["DefaultButtonStyle"],
					FontSize = 15,
					Height = 50,
					Width = 100,
					Margin = new Thickness(0, 0, 5, 0)
				};

				button.MouseRightButtonDown += (sender, e) =>
				{
					var mouseX = e.GetPosition(null).X;
					var mouseY = e.GetPosition(null).Y;

					Canvas.SetLeft(_mainWindow.ProjectManagmentToolUserControl, mouseX);
					Canvas.SetTop(_mainWindow.ProjectManagmentToolUserControl, mouseY);

					_mainWindow.ProjectManagmentToolUserControlCanvas.Visibility = Visibility.Visible;
					var projectManagmentToolViewModel = (ProjectManagmentToolViewModel)_mainWindow.ProjectManagmentToolUserControl.DataContext;
					projectManagmentToolViewModel.SelectedProject = project;
				};

				button.Click += async (sender, e) =>
				{
					Repository.SelectProject = project;
					var tasks = (await taskService.GetTasksByUserId(Repository.User.id)).Where(x => x.isDeleted == false);
					if (tasks is null) return;
					_mainWindow.TasksUserControl.ClearTasks();
					foreach (var task in tasks)
					{
						_mainWindow.TasksUserControl.AddTaskOnPnael(task.id);
					}
					_mainWindow.TasksUserControl.Visibility = Visibility.Visible;
				};

				new ProjectService().AddProjectInRepository(new ProjectInProjectPanelReadModel
				{
					Project = project,
					Button = button,
				});

				_userControl.ProjectsStackPanel.Children.Insert(0, button);
			}
		}

		public void SetDefaultSettings()
		{
			if (_userControl is not null)
			{
				_defaultHeightProjectPanel = _userControl?.SubtopicPanelScrollViewer.Height ?? 0;
				_defaultOpacityBlackOutProjectPanel = _userControl?.BlackoutMainUserControl.Opacity ?? 0;

				_userControl!.SubtopicPanelScrollViewer.Height = 0;
				_userControl.BlackoutMainUserControl.Opacity = 0;
			}
		}

		/// <summary>
		/// Открытие панели задач.
		/// </summary>
		public void OpenProjectPanel()
		{
			if (_userControl is not null)
			{
				var heightAnimation = GetHeightAnimation(_userControl.SubtopicPanelScrollViewer.Height, _defaultHeightProjectPanel);
				var opacityAnimation = GetOpacityAnimation(_userControl.BlackoutMainUserControl.Opacity, _defaultOpacityBlackOutProjectPanel);

				_userControl.BlackoutMainUserControl.Visibility = Visibility.Visible;

				_userControl.SubtopicPanelScrollViewer.BeginAnimation(StackPanel.HeightProperty, heightAnimation);
				_userControl.BlackoutMainUserControl.BeginAnimation(Rectangle.OpacityProperty, opacityAnimation);

				_userControl.SubtopicPanelButton.Content = "▲";

				_isProjectPanelOpen = true;
			}
		}

		/// <summary>
		/// Закрытие панели задач.
		/// </summary>
		public void CloseProjectPanel()
		{
			if (_userControl is not null)
			{
				var heightAnimation = GetHeightAnimation(_userControl.SubtopicPanelScrollViewer.Height, 0);
				var opacityAnimation = GetOpacityAnimation(_userControl.BlackoutMainUserControl.Opacity, 0);

				_userControl.SubtopicPanelScrollViewer.BeginAnimation(StackPanel.HeightProperty, heightAnimation);
				_userControl.BlackoutMainUserControl.BeginAnimation(Rectangle.OpacityProperty, opacityAnimation);

				_userControl.BlackoutMainUserControl.Visibility = Visibility.Collapsed;

				_userControl.SubtopicPanelButton.Content = "▼";

				_isProjectPanelOpen = false;
			}
		}
		#endregion

		#region Private
		private DoubleAnimation GetOpacityAnimation(double from = 0, double to = 0)
		{
			var opacityAnimation = new DoubleAnimation
			{
				To = to,
				From = from,
				Duration = TimeSpan.FromMilliseconds(200),
				SpeedRatio = 0.95
			};
			return opacityAnimation;
		}

		private DoubleAnimation GetHeightAnimation(double from = 0, double to = 0)
		{
			var heightAnimation = new DoubleAnimation
			{
				To = to,
				From = from,
				Duration = TimeSpan.FromMilliseconds(200),
				SpeedRatio = 0.95
			};
			return heightAnimation;
		}
		#endregion

		#region PropertyChanged
		public event PropertyChangedEventHandler? PropertyChanged;
		public void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(prop));
		}
		#endregion
	}
}
