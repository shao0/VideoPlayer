using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using UI.Library.Helpers;
using UI.Library.MessageTools;

namespace UI.Library.Controls
{
    public class MimicryMessage : ContentControl
    {
        private int OutTime = 3;
        private DispatcherTimer OutTimer;
        private int TickCount = 0;
        public MimicryMessage()
        {
            StartTimer();
        }

        public MimicryMessage(string msg) : this()
        {
            Message = msg;
        }

        public MimicryMessage(string msg, int outTime) : this(msg)
        {
            OutTime = outTime;
        }

        void StartTimer()
        {
            OutTimer = new DispatcherTimer
            { Interval = TimeSpan.FromSeconds(1) };
            OutTimer.Tick += OutTimer_Tick;
            OutTimer.Start();
        }

        private void OutTimer_Tick(object sender, EventArgs e)
        {
            if (IsMouseOver)
            {
                TickCount = 0;
                return;
            }
            TickCount++;
            if (TickCount >= OutTime)
            {
                Close();
            }
        }

        void Close()
        {
            OutTimer?.Stop();
            var width = FlowDirection == FlowDirection.LeftToRight ? Width : -Width;
            RenderTransform.BindingDoubleAnimation(TranslateTransform.XProperty, width, completed: () =>
             {
                 var window = GetMessageWindow(this);
                 if (GetMessagePanel(window) is Panel panel)
                 {
                     panel.Children.Remove(this);
                 }
             });

        }
        static MimicryMessage()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MimicryMessage), new FrameworkPropertyMetadata(typeof(MimicryMessage)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            var x = FlowDirection == FlowDirection.LeftToRight ? Width : -Width;
            var transform = new TranslateTransform
            {
                X = x
            };
            RenderTransform = transform;
            transform.BindingDoubleAnimation(TranslateTransform.XProperty, 0);
        }

        public static readonly DependencyProperty MessageProperty = DependencyProperty.Register(
            "Message", typeof(string), typeof(MimicryMessage), new PropertyMetadata(default(string)));

        public string Message
        {
            get => (string)GetValue(MessageProperty);
            set => SetValue(MessageProperty, value);
        }

        public static readonly DependencyProperty MessagePanelProperty = DependencyProperty.RegisterAttached(
            "MessagePanel", typeof(FrameworkElement), typeof(MimicryMessage), new PropertyMetadata(default(FrameworkElement),
                (s, e) =>
                {
                    var window = GetMessageWindow((FrameworkElement)s);
                    SetMessagePanel(window, (Panel)e.NewValue);
                }));

        /// <summary>
        /// 获取消息容器面板
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Panel GetMessagePanel(DependencyObject obj) => (Panel)obj.GetValue(MessagePanelProperty);

        /// <summary>
        /// 设置消息容器面板
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetMessagePanel(DependencyObject obj, Panel value)
        {
            obj.SetValue(MessagePanelProperty, value);
        }

        public static void ShowMessage(FrameworkElement element, string msg, int outTime = 3)
        {
            element.Dispatcher.Invoke(() =>
            {
                if (GetMessagePanel(GetMessageWindow(element)) is Panel panel)
                {
                    panel.Children.Add(new MimicryMessage(msg, outTime));
                }
            });
        }
        /// <summary>
        /// 获取消息窗口
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static FrameworkElement GetMessageWindow(FrameworkElement element) =>
            element is Window ? element : Window.GetWindow(element);
    }
}
