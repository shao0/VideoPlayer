using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace UI.Library.Controls
{
   public class MimicryComboBox:ComboBox
    {
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
            "Title", typeof(string), typeof(MimicryComboBox), new PropertyMetadata(default(string)));

        public string Title
        {
            get { return (string) GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }
        static MimicryComboBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MimicryComboBox), new FrameworkPropertyMetadata(typeof(MimicryComboBox)));
        }
    }
}
