using System.Windows.Controls;

namespace AssignmentV2.View.UserControls.AdminPanels
{
	/// <summary>
	/// Логика взаимодействия для AdminPanelUserControl.xaml
	/// </summary>
	public partial class AdminPanelUserControl : UserControl
	{
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
    }
}
