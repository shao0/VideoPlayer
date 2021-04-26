using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace UI.Library.Controls
{
   public class MimicrySlider:Slider
    {

        public override void OnApplyTemplate()
        {
            if (Template.FindName("PART_Border", this) is FrameworkElement mainBorder)
            {
                mainBorder.MouseLeftButtonUp += (s, e) =>
                {
                    if (!(s is FrameworkElement element)) return;
                    var position = e.GetPosition(element);
                    Value = position.X / element.ActualWidth * Maximum;
                };
            }
            base.OnApplyTemplate();
        }

        static MimicrySlider()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MimicrySlider), new FrameworkPropertyMetadata(typeof(MimicrySlider)));
        }
    }
}
