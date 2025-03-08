using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using AssignmentV2.Services;
using AssignmentV2.Utilities;

namespace AssignmentV2.View
{
	/// <summary>
	/// Логика взаимодействия для CreateUserWindow.xaml
	/// </summary>
	public partial class CreateUserWindow : Window
	{
		private UsersDbService _userDbService;
		private UserService _userService;

		public CreateUserWindow()
		{
			InitializeComponent();
			_userDbService = new UsersDbService();
			_userService = new UserService();
		}

		private async void CreteUserButton_Click(object sender, RoutedEventArgs e)
		{
			if(string.IsNullOrEmpty(FioTextBox.Text) && string.IsNullOrEmpty(LoginTextBox.Text) && string.IsNullOrEmpty(PasswordTextBox.Text))
			{
				CustomMessageBox.Information("Поля ФИО, Логин, Пароль не должны быть пустыми.");
				return;
			}

			var user = (await _userDbService.GetAllUsers()).Where(x => x.login == LoginTextBox.Text).FirstOrDefault();
			if(user is null)
			{
				await _userService.CreateUser(LoginTextBox.Text, PasswordTextBox.Text, FioTextBox.Text, CreateTasksCheckBox.IsChecked ?? false, CreateProjectsCheckBox.IsChecked ?? false, AdminCheckBox.IsChecked ?? false);
				this.Close();
			}
			else
			{
				CustomMessageBox.Information("пользователь с таким логином уже существует.");
				return;
			}
        }
    }
}
