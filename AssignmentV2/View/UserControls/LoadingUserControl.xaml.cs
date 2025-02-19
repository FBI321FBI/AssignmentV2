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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AssignmentV2.View.UserControls
{
    /// <summary>
    /// Логика взаимодействия для LoadingUserControl.xaml
    /// </summary>
    public partial class LoadingUserControl : UserControl
    {
		private bool _isDisableAnimation;

        public LoadingUserControl()
        {
            InitializeComponent();
        }

        public void Start()
        {
			_isDisableAnimation = false;
			CurrentUserControl.Visibility = Visibility.Visible;

			DoubleAnimation doubleAnimation = new DoubleAnimation
			{
				To = 150,
				From = 0,
				Duration = TimeSpan.FromMilliseconds(1000),
				SpeedRatio = 0.95
			};

			DoubleAnimation doubleAnimation2 = new DoubleAnimation
			{
				To = 0,
				From = 150,
				Duration = TimeSpan.FromMilliseconds(1000),
				SpeedRatio = 0.95
			};

			doubleAnimation.Completed += (sender, e) =>
			{
				if (_isDisableAnimation) 
				{
					return;
				}

				LoadingRectangle.HorizontalAlignment = HorizontalAlignment.Right;
				LoadingRectangle.BeginAnimation(Rectangle.WidthProperty, doubleAnimation2);
			};

			doubleAnimation2.Completed += (sender, e) =>
			{
				if (_isDisableAnimation)
				{
					return;
				}

				LoadingRectangle.HorizontalAlignment = HorizontalAlignment.Left;
				LoadingRectangle.BeginAnimation(Rectangle.WidthProperty, doubleAnimation);
			};
			LoadingRectangle.BeginAnimation(Rectangle.WidthProperty, doubleAnimation);
		}

        public void Stop()
        {
			_isDisableAnimation = true;
			CurrentUserControl.Visibility = Visibility.Collapsed;
			LoadingRectangle.HorizontalAlignment = HorizontalAlignment.Left;
			LoadingRectangle.Width = 0;
		}
    }
}
