using AssignmentV2.ReadModels.Projects;
using AssignmentV2.Services;
using AssignmentV2.View.UserControls;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace AssignmentV2.ViewModel
{
	public partial class ProjectManagmentToolViewModel : INotifyPropertyChanged
	{
		#region Properties
		private ProjectManagmentToolUserControl? _userControl => Application.Current.Windows.OfType<MainWindow>().FirstOrDefault()?.ProjectManagmentToolUserControl;

		private ProjectService _service => new ProjectService();

		/// <summary>
		/// Выбранный проект.
		/// </summary>
		public ProjectReadModel? SelectedProject { get; set; }
		#endregion

		#region Commands
		/// <summary>
		/// Удаление выбранного проекта.
		/// </summary>
		[RelayCommand]
		public void RemoveProject()
		{
			if (SelectedProject is not null)
			{
				_service.RemoveProjectById(SelectedProject.id);
				OpenOrCloseRenameStuckPanel();
			}
		}

		[RelayCommand]
		public void OpenOrCloseRenameStuckPanel()
		{
			if(_userControl is null) return;

			if (_userControl.RenameBorder.Visibility == Visibility.Collapsed)
			{
				_userControl.RenameBorder.Visibility = Visibility.Visible;
			}
			else
			{
				_userControl.RenameBorder.Visibility = Visibility.Collapsed;
			}
		}

		/// <summary>
		/// Переименовывает проект.
		/// </summary>
		[RelayCommand]
		public void RenameProject()
		{
			if (_userControl is null) return;

			if (SelectedProject is not null)
			{
				_service.RenameProjectById(SelectedProject.id, _userControl.RenameTextBox.Text);
				OpenOrCloseRenameStuckPanel();
			}
		}
		#endregion

		#region PropertyChanged
		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(prop));
		}
		#endregion
	}
}
