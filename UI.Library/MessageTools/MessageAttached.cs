using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Animation;
using UI.Library.Controls;
using UI.Library.Panels;

namespace UI.Library.MessageTools
{
    public class MessageAttached : DependencyObject
    {
        /// <summary>
        /// 窗口宿主事件名称
        /// </summary>
        public static readonly DependencyProperty HostEventNameProperty = DependencyProperty.RegisterAttached(
            "HostEventName", typeof(string), typeof(MessageAttached), new PropertyMetadata(default(string), (s, e) =>
            {
                if (s is ButtonBase buttonBase)
                {
                    buttonBase.Click += (bs, be) =>
                    {
                        if (bs is ButtonBase sender && Window.GetWindow(sender) is Window window)
                        {
                            var hostEventName = GetHostEventName(sender);
                            switch (hostEventName)
                            {
                                case "Min":
                                    window.WindowState = WindowState.Minimized;
                                    break;
                                case "Normal":
                                    if (sender is ToggleButton toggleButton) window.WindowState = toggleButton.IsChecked != true
                                            ? WindowState.Normal
                                            : WindowState.Maximized;
                                    break;
                                case "Close":
                                    window.Close();
                                    break;
                            }
                        }
                    };
                }
            }));


        /// <summary>
        /// 获取窗口宿主事件名称
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string GetHostEventName(DependencyObject obj)
        {
            return (string)obj.GetValue(HostEventNameProperty);
        }

        /// <summary>
        /// 设置窗口宿主事件名称
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetHostEventName(DependencyObject obj, string value)
        {
            obj.SetValue(HostEventNameProperty, value);
        }



    }
}
