using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using UI.Library.Controls;
using UI.Library.Panels;

namespace UI.Library.MessageTools
{
    public class MessageAttached : DependencyObject
    {
        public static readonly DependencyProperty MessagePanelProperty = DependencyProperty.RegisterAttached(
            "MessagePanel", typeof(Panel), typeof(MessageAttached), new PropertyMetadata(default(Panel)));


        /// <summary>
        /// 获取消息窗口宿主
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Panel GetMessagePanel(DependencyObject obj)
        {
            return (Panel)obj.GetValue(MessagePanelProperty);
        }

        /// <summary>
        /// 设置消息窗口宿主
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetMessagePanel(DependencyObject obj, Panel value)
        {
            obj.SetValue(MessagePanelProperty, value);
        }
        /// <summary>
        /// 附加消息宿主
        /// </summary>
        public static readonly DependencyProperty MessageHostProperty = DependencyProperty.RegisterAttached(
            "MessageHost", typeof(FrameworkElement), typeof(MessageAttached), new PropertyMetadata(default(FrameworkElement), On_HostChanged));

        private static string MessagePanelName = "MessagePanel";



        private static void On_HostChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Panel panel && panel.FindName(MessagePanelName) == null)
            {
                var insertionOrderPanel = new InsertionOrderPanel();
                insertionOrderPanel.Order = InsertionOrder.PositiveSequence;
                insertionOrderPanel.Orientation = InsertionOrientation.TopToBottom;
                insertionOrderPanel.HorizontalAlignment = HorizontalAlignment.Center;
                panel.RegisterName(MessagePanelName, insertionOrderPanel);
                panel.Children.Add(insertionOrderPanel);
                Panel.SetZIndex(insertionOrderPanel, 1000);
                SetMessagePanel((DependencyObject)e.NewValue, insertionOrderPanel);
            }
        }


        /// <summary>
        /// 获取消息窗口宿主
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static FrameworkElement GetMessageHost(DependencyObject obj)
        {
            return (FrameworkElement)obj.GetValue(MessageHostProperty);
        }

        /// <summary>
        /// 设置消息窗口宿主
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetMessageHost(DependencyObject obj, FrameworkElement value)
        {
            obj.SetValue(MessageHostProperty, value);
        }
        /// <summary>
        /// 提示
        /// </summary>
        /// <param name="element"></param>
        /// <param name="msg"></param>
        /// <param name="outMillisecond"></param>
        public static void Show(FrameworkElement element, string msg, double outMillisecond)
        {
            element?.Dispatcher.Invoke(() =>
            {
                FrameworkElement host = GetMessageWindow(element);
                var messagePanel = GetMessagePanel(host);
                if (messagePanel != null)
                {
                    var mimicryControl = new MimicryControl();
                    mimicryControl.Padding = new Thickness(10,5,10,5);
                    mimicryControl.FontSize = 18;
                    var tips = new TextBlock();
                    tips.Text = msg;
                    tips.TextWrapping = TextWrapping.Wrap;
                    mimicryControl.Content = tips;
                    mimicryControl.MinWidth = 120;
                    mimicryControl.MaxWidth = 360;
                    mimicryControl.Loaded += (sen, r) =>
                    {
                        mimicryControl.Margin = new Thickness(0, -mimicryControl.ActualHeight - 10, 0, 0);
                        var moveTime = 500;
                        var marginAnimation = new ThicknessAnimationUsingKeyFrames();
                        marginAnimation.KeyFrames = new ThicknessKeyFrameCollection();
                        marginAnimation.KeyFrames.Add(new LinearThicknessKeyFrame {KeyTime = TimeSpan.FromMilliseconds(moveTime), Value = new Thickness(0)});
                        var opacityAnimation = new DoubleAnimationUsingKeyFrames();
                        opacityAnimation.KeyFrames = new DoubleKeyFrameCollection();
                        opacityAnimation.KeyFrames.Add(new LinearDoubleKeyFrame()
                            {KeyTime = TimeSpan.FromMilliseconds(moveTime), Value = 1});
                        opacityAnimation.KeyFrames.Add(new LinearDoubleKeyFrame()
                            {KeyTime = TimeSpan.FromMilliseconds(outMillisecond + moveTime - 500), Value = 0.7});
                        opacityAnimation.KeyFrames.Add(new LinearDoubleKeyFrame()
                            {KeyTime = TimeSpan.FromMilliseconds(outMillisecond + moveTime), Value = 0});
                        opacityAnimation.Completed += (s, e) =>
                        {
                            messagePanel.Children.Remove(mimicryControl);
                        };
                        mimicryControl.BeginAnimation(FrameworkElement.MarginProperty, marginAnimation);
                        mimicryControl.BeginAnimation(UIElement.OpacityProperty, opacityAnimation);
                    };
                    messagePanel.Children.Add(mimicryControl);
                }
            });

        }

        private static FrameworkElement GetMessageWindow(FrameworkElement element)
        {
            return element is Window ? element : Window.GetWindow(element);
        }
    }
}
