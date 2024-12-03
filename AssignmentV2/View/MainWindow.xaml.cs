using System.Windows;
using AssignmentV2.Handlers.Events;

namespace AssignmentV2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            RegisteredEvents();
        }

        private void RegisteredEvents()
        {
            authorizationUserControl.AuthorizationViewModel.OnLoggedIn += UserLoggedIn.Handler;
        }

        private void MinimizedButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Close();
        }
    }
}