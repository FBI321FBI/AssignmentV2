using AssignmentV2.ReadModels.Projects;
using AssignmentV2.Services.DataBase;
using AssignmentV2.View.UserControls.ProjectPanel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace AssignmentV2.ViewModel.ProjectPanel
{
	public partial class EnterNameProjectViewModel : INotifyPropertyChanged
	{
		#region Properties
		private MainWindow? _mainWindow => Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
		private MainProjectPanelUserControl? _mainProjectPanelUserControl => Application.Current.Windows.OfType<MainWindow>().FirstOrDefault()?.MainProjectPanelUserControl;
		private ProjectPanelViewModel? _projectPanelViewModel => Application.Current.Windows.OfType<MainWindow>().FirstOrDefault()?.MainProjectPanelUserControl?.ProjectPanelUserControl.DataContext as ProjectPanelViewModel;
		private EnterNameProjectUserControl? _userControl => Application.Current.Windows.OfType<MainWindow>().FirstOrDefault()?.MainProjectPanelUserControl?.EnterNameProjectUserControl;
		#endregion

		#region Commands
		[RelayCommand]
		public async void CreateProject()
		{
			if(_projectPanelViewModel is not null)
			{
				_projectPanelViewModel.AddProjectVisually(new ProjectReadModel
				{
					id = Guid.NewGuid(),
					name = _userControl?.ProjectNameTextBox.Text ?? "🤔"
				});
				_userControl.Visibility = Visibility.Collapsed;

				_mainWindow.LoadingUserControl.Start();
				await new ProjectDbService().CreateProject(new ProjectReadModel { name = _userControl?.ProjectNameTextBox.Text });
				_mainWindow.LoadingUserControl.Stop();

				_mainProjectPanelUserControl.EnterNameProjectUserControl.ProjectNameTextBox.Clear();
			}
		}

		[RelayCommand]
		public void CloseCreateProject()
		{
			if(_mainProjectPanelUserControl is not null)
			{
				_mainProjectPanelUserControl.EnterNameProjectUserControl.Visibility = Visibility.Collapsed;
				_mainProjectPanelUserControl.EnterNameProjectUserControl.ProjectNameTextBox.Clear();
			}
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
