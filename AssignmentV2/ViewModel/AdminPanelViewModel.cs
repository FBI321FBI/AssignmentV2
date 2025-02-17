using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using AssignmentV2.Services;
using AssignmentV2.View.UserControls.AdminPanels;
using CommunityToolkit.Mvvm.Input;

namespace AssignmentV2.ViewModel
{
	public partial class AdminPanelViewModel : INotifyPropertyChanged
	{
		#region Properties
		private AdminPanelUserControl? _userControl => MainWindowService.GetUserControlInMainWindow<AdminPanelUserControl>();

		public double AdminPanelDefaultWidth;
		public bool IsOpen;
		#endregion

		#region Commands
		[RelayCommand]
		public void OpenAndCloseAdminPanel()
		{
			if(_userControl is not null)
			{
				if (!IsOpen)
				{
					var widthAnimation = GetWidthAnimation(_userControl.AdminPanelBorder.Width, AdminPanelDefaultWidth);
					_userControl.AdminPanelBorder.BeginAnimation(StackPanel.WidthProperty, widthAnimation);
					IsOpen = true;
				}
				else
				{
					var widthAnimation = GetWidthAnimation(_userControl.AdminPanelBorder.Width, 0);
					_userControl.AdminPanelBorder.BeginAnimation(StackPanel.WidthProperty, widthAnimation);
					IsOpen = false;
				}
				
			}
		}
		#endregion

		#region Private
		private DoubleAnimation GetWidthAnimation(double from = 0, double to = 0)
		{
			var widthAnimation = new DoubleAnimation
			{
				To = to,
				From = from,
				Duration = TimeSpan.FromMilliseconds(200),
				SpeedRatio = 0.95
			};
			return widthAnimation;
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
