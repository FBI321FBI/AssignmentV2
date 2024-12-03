using AssignmentV2.DataBase.Tables;
using AssignmentV2.ReadModels.Tables;
using AssignmentV2.Utilities;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using static AssignmentV2.ViewModel.AuthorizationViewModel;

namespace AssignmentV2.ViewModel
{
	public partial class AuthorizationViewModel : INotifyPropertyChanged
	{
		public delegate void LoggedIn(User user);
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
			Task.Run(async () =>
			{
				UsersTable usersTable = new();
				var user = (await usersTable.Select())?.Where(x => x.login == login && x.password == password).SingleOrDefault();
				if (user == null)
				{
					CustomMessageBox.Information("Пользователь не найден.");
				}
				else
				{
					OnLoggedIn?.Invoke(user);
                }
			});
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
