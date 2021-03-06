using System.Windows.Media;

namespace VideoPlayerLibrary.Models
{
    public class FilmInfo
    {
        /// <summary>
        /// 图片
        /// </summary>
        public ImageSource ImageSource { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Url { get; set; }
    }
}
