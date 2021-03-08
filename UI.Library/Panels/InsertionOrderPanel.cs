using System;
using System.Windows;
using System.Windows.Controls;

namespace UI.Library.Panels
{
    /// <summary>
    /// 插入顺序
    /// </summary>
    public class InsertionOrderPanel : Panel
    {
        /// <summary>
        /// 插入顺序
        /// </summary>
        public static readonly DependencyProperty OrderProperty;
        /// <summary>
        /// 插入顺序
        /// </summary>
        public InsertionOrder Order
        {
            get => (InsertionOrder)GetValue(OrderProperty);
            set => SetValue(OrderProperty, value);
        }

        /// <summary>
        /// 插入方向
        /// </summary>
        public static readonly DependencyProperty OrientationProperty;
        /// <summary>
        /// 插入方向
        /// </summary>
        public InsertionOrientation Orientation
        {
            get => (InsertionOrientation)GetValue(OrientationProperty);
            set => SetValue(OrientationProperty, value);
        }

        static InsertionOrderPanel()
        {
            var metadata = new FrameworkPropertyMetadata
            {
                AffectsArrange = true,
                AffectsParentArrange = true,
                AffectsParentMeasure = true
            };
            OrderProperty = DependencyProperty.Register("Order", typeof(InsertionOrder), typeof(InsertionOrderPanel), metadata);
            metadata = new FrameworkPropertyMetadata
            {
                AffectsArrange = true,
                AffectsParentArrange = true,
                AffectsParentMeasure = true
            };
            OrientationProperty = DependencyProperty.Register("Orientation", typeof(InsertionOrientation), typeof(InsertionOrderPanel), metadata);
        }

        /// <summary>
        /// 面板所需大小
        /// </summary>
        /// <param name="availableSize"></param>
        /// <returns></returns>
        protected override Size MeasureOverride(Size availableSize)
        {
            //面板大小
            var panelSize = new Size(0, 0);
            foreach (UIElement child in Children)
            {
                //测量元素布局大小
                child.Measure(availableSize);
                switch (Orientation)
                {
                    //横向布局高度不变宽度累计
                    case InsertionOrientation.LeftToRight:
                    case InsertionOrientation.RightToLeft:
                        panelSize.Width += child.DesiredSize.Width;
                        panelSize.Height = Math.Max(child.DesiredSize.Height, panelSize.Height);
                        break;
                    //竖向布局宽度不变高度累计
                    case InsertionOrientation.TopToBottom:
                    case InsertionOrientation.BottomToTop:
                        panelSize.Height += child.DesiredSize.Height;
                        panelSize.Width = Math.Max(child.DesiredSize.Width, panelSize.Width);
                        break;
                }
            }
            return panelSize;
        }
        /// <summary>
        /// 元素所处面板位置
        /// </summary>
        /// <param name="finalSize"></param>
        /// <returns></returns>
        protected override Size ArrangeOverride(Size finalSize)
        {
            //记录元素已占用位置
            Point location = new Point(0, 0);
            if (Children.Count > 0)
            {
                UIElement lastChild;
                //反向布局元素初始位置
                switch (Orientation)
                {
                    case InsertionOrientation.RightToLeft:
                        lastChild = this.Children[this.Children.Count - 1];
                        lastChild.Arrange(new Rect(location, new Size(lastChild.DesiredSize.Width, finalSize.Height)));
                        location.X = finalSize.Width - lastChild.DesiredSize.Width;
                        break;
                    case InsertionOrientation.BottomToTop:
                        lastChild = this.Children[this.Children.Count - 1];
                        lastChild.Arrange(new Rect(location, new Size(finalSize.Width, lastChild.DesiredSize.Height)));
                        location.Y = finalSize.Height - lastChild.DesiredSize.Height;
                        break;
                }
            }
            switch (Order)
            {
                case InsertionOrder.ReverseOrder:
                    {
                        for (var i = 0; i < Children.Count; i++)
                        {
                            SetChildPoint(ref location, i);
                        }
                        break;
                    }
                case InsertionOrder.PositiveSequence:
                    {
                        for (var i = Children.Count - 1; i >= 0; i--)
                        {
                            SetChildPoint(ref location, i);
                        }
                        break;
                    }
            }
            return finalSize;
        }
        /// <summary>
        /// 设置元素布局位置
        /// </summary>
        /// <param name="location"></param>
        /// <param name="i"></param>
        private void SetChildPoint(ref Point location, int i)
        {
            if (Children[i] is UIElement child)
            {
                switch (Orientation)
                {
                    case InsertionOrientation.LeftToRight:
                        //设置元素矩形
                        child.Arrange(new Rect(location, new Size(child.DesiredSize.Width, child.DesiredSize.Height)));
                        //记录已布局位置
                        location.X += child.DesiredSize.Width;
                        break;
                    case InsertionOrientation.RightToLeft:
                        child.Arrange(new Rect(location, new Size(child.DesiredSize.Width, child.DesiredSize.Height)));
                        location.X -= child.DesiredSize.Width;
                        break;
                    case InsertionOrientation.TopToBottom:
                        child.Arrange(new Rect(location, new Size(child.DesiredSize.Width, child.DesiredSize.Height)));
                        location.Y += child.DesiredSize.Height;
                        break;
                    case InsertionOrientation.BottomToTop:
                        child.Arrange(new Rect(location, new Size(child.DesiredSize.Width, child.DesiredSize.Height)));
                        location.Y -= child.DesiredSize.Height;
                        break;
                }
            }
        }
    }

    /// <summary>
    /// 排序插入
    /// </summary>
    public enum InsertionOrder
    {
        /// <summary>
        /// 正序
        /// </summary>
        ReverseOrder,
        /// <summary>
        /// 倒序
        /// </summary>
        PositiveSequence,
    }
    /// <summary>
    /// 插入方向
    /// </summary>
    public enum InsertionOrientation
    {
        /// <summary>
        /// 左到右
        /// </summary>
        LeftToRight,
        /// <summary>
        /// 右到左
        /// </summary>
        RightToLeft,
        /// <summary>
        /// 上到下
        /// </summary>
        TopToBottom,
        /// <summary>
        /// 下到上
        /// </summary>
        BottomToTop,
    }
}
