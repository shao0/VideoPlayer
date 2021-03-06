using System.Windows.Controls;
using System.Windows.Input;

namespace VideoPlayerVlc.Views
{
    /// <summary>
    /// VlcPlayerView.xaml 的交互逻辑
    /// </summary>
    public partial class VlcPlayerView : UserControl
    {
        public VlcPlayerView()
        {
            InitializeComponent();
        }
        private void Slider_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Slider slider)
            {
                var point = e.GetPosition(slider);
                slider.Value = point.X / slider.ActualWidth;

            }
        }
    }
}
