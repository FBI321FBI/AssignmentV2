using System.Windows;
using System.Windows.Controls;

namespace AssignmentV2.View.UserControls
{
	/// <summary>
	/// Логика взаимодействия для Authorization.xaml
	/// </summary>
	public partial class AuthorizationUserControl : UserControl
	{
		public AuthorizationUserControl()
		{
			InitializeComponent();
		}

		private void PasswordBox_PasswordChanged(object sender, System.Windows.RoutedEventArgs e)
		{
			AuthorizationViewModel.Password = PasswordBox.Password;
		}

		private void LookButton_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			PasswordTextBox.Visibility = Visibility.Visible;
			PasswordBox.Visibility = Visibility.Collapsed;
			PasswordTextBox.Text = PasswordBox.Password;
		}

		private void LookButton_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			PasswordTextBox.Visibility = Visibility.Collapsed;
			PasswordBox.Visibility = Visibility.Visible;
			PasswordTextBox.Text = PasswordBox.Password;
		}
	}
}
