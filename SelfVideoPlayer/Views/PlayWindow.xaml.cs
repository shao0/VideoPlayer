
using VideoPlayerLibrary.Models;

namespace SelfVideoPlayer.Views
{
    /// <summary>
    /// PlayWindow.xaml 的交互逻辑
    /// </summary>
    public partial class PlayWindow
    {
        public PlayWindow()
        {
            InitializeComponent();
        }

        public  void ShowDialog(FilmInfo filmInfo)
        {
            Tag = filmInfo;
            base.ShowDialog();
        }
    }
}
