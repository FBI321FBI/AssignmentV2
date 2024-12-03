using System.Windows;

namespace AssignmentV2.Utilities
{
    public static class CustomMessageBox
    {
        public static void Information(string message, string caption = "Информация")
        {
            MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
