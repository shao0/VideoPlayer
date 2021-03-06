using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace UI.Library.Controls
{
    public class MimicryImage : ContentControl
    {
        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register(
            "Source", typeof(ImageSource), typeof(MimicryImage), new PropertyMetadata(default(ImageSource)));
        /// <summary>
        /// 图片
        /// </summary>
        public ImageSource Source
        {
            get { return (ImageSource) GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }
        static MimicryImage()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MimicryImage), new FrameworkPropertyMetadata(typeof(MimicryImage)));
            
        }
    }
}
