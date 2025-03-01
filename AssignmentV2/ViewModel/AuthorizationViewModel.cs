using System.ComponentModel;
using System.Runtime.CompilerServices;
using AssignmentV2.ReadModels;
using AssignmentV2.Services;
using AssignmentV2.Utilities;
using CommunityToolkit.Mvvm.Input;

namespace AssignmentV2.ViewModel
{
	public partial class AuthorizationViewModel : INotifyPropertyChanged
	{
		public delegate void LoggedIn(UserReadModel user);
		public event LoggedIn? OnLoggedIn;

		#region Properties
		private string login = string.Empty;
		public string Login
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

		private string password = string.Empty;
		public string Password
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
			if (Login == string.Empty)
			{
				CustomMessageBox.Information("Логин не должен быть пустым.");
				return;
			}

			Task.Run(async () =>
			{
				UsersDbService usersTable = new();
				UserService userService = new UserService();
				var result = await usersTable.IsUserCorrect(Login, Password);
				if (!result)
				{
					CustomMessageBox.Information("Пользователь не найден.");
				}
				else
				{
					var user = await usersTable.GetUserByLoginAsync(Login);
					Repository.SetUser(new UserReadModel { id = user!.id, login = user.login });
					await userService.FillUserClaims(Repository.User!);
					await userService.FillUserProjects(Repository.User!);
					OnLoggedIn?.Invoke(Repository.User!);
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
