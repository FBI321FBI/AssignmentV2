using System.Windows.Controls;
using AssignmentV2.Services;

namespace AssignmentV2.View.UserControls.AdminPanels
{
	/// <summary>
	/// Логика взаимодействия для AdminPanelUserControl.xaml
	/// </summary>
	public partial class AdminPanelUserControl : UserControl
	{
		private UsersDbService _usersDbService = new UsersDbService();

		public AdminPanelUserControl()
		{
			InitializeComponent();
			AdminPanelViewModel.AdminPanelDefaultWidth = AdminPanelBorder.Width;
			AdminPanelBorder.Width = 0;
		}

		private void CreateUserButton_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			CreateUserWindow createUserWindow = new CreateUserWindow();
			createUserWindow.ShowDialog();
        }

		private async void DeleteUserButton_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			SelectUserWindow selectUserWindow = new SelectUserWindow();
			var users = (await _usersDbService.GetAllUsers()).Where(x => x.id != Repository.User.id);
			selectUserWindow.LoadUsers(users);
			selectUserWindow.ShowDialog();
			if(selectUserWindow.SelectedUser is not null)
			{
				_usersDbService.DeleteUser(selectUserWindow.SelectedUser.id);
			}
			selectUserWindow.Close();
		}
    }
}
