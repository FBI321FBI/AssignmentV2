using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace AssignmentV2.ViewModel
{
	public partial class AuthorizationViewModel : INotifyPropertyChanged
	{
		public delegate int LoggedIn();
		public event LoggedIn? OnLoggedIn;

		#region Properties
		private string? login;
		public string? Login
		{
			get
			{
				return login;
			}
			set
			{
				login = value;
				OnPropertyChanged("login");
			}
		}

		private string? password;
		public string? Password
		{
			get
			{
				return password;
			}
			set
			{
				password = value;
				OnPropertyChanged("password");
			}
		}
		#endregion

		#region Commands
		[RelayCommand]
		private void Authorization()
		{
			MessageBox.Show(Password);
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
