using System.Windows;
using System.Windows.Controls;
using AssignmentV2.ReadModels;
using AssignmentV2.Services;
using AssignmentV2.Services.DataBase;
using static AssignmentV2.Constants.Parameters;

namespace AssignmentV2.View
{
	/// <summary>
	/// Логика взаимодействия для SelectUserWindow.xaml
	/// </summary>
	public partial class SelectUserWindow : Window
	{
		public UserReadModel SelectedUser;

		public SelectUserWindow()
		{
			InitializeComponent();
		}

		public async Task LoadUsers(IEnumerable<UserReadModel> users)
		{
			UsersClaimsDbService usersClaimsDbService = new UsersClaimsDbService();
			foreach (var user in users)
			{
				var userClaim = (await usersClaimsDbService.GetUserParameters(user.id)).Where(x => x.parameter_id == Guid.Parse(FIO)).FirstOrDefault();
				Button button = new Button
				{
					Content = userClaim?.value,
					Style = (Style)Application.Current.Resources["DefaultButtonStyle"],
					FontSize = 25,
					Margin = new Thickness(5)
				};
				button.Click += (e, a) =>
				{
					SelectedUser = user;
					this.Hide();
				};
				UsersStackPanel.Children.Add(button);
			}
			
		}
	}
}
