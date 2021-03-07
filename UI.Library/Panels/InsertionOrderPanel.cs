using System;
using System.Windows;
using System.Windows.Controls;

namespace UI.Library.Panels
{
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


        protected override Size MeasureOverride(Size availableSize)
        {
            var panelSize = new Size(0, 0);
            foreach (UIElement child in Children)
            {
                child.Measure(availableSize);
                switch (Orientation)
                {
                    case InsertionOrientation.LeftToRight:
                    case InsertionOrientation.RightToLeft:
                        panelSize.Width += child.DesiredSize.Width;
                        panelSize.Height = Math.Max(child.DesiredSize.Height, panelSize.Height);
                        break;
                    case InsertionOrientation.TopToBottom:
                    case InsertionOrientation.BottomToTop:
                        panelSize.Height += child.DesiredSize.Height;
                        panelSize.Width = Math.Max(child.DesiredSize.Width, panelSize.Width);
                        break;
                }
            }
            return panelSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            Point childPos = new Point(0, 0);
            if (Children.Count > 0)
            {
                UIElement lastChild;
                switch (Orientation)
                {
                    case InsertionOrientation.RightToLeft:
                        lastChild = this.Children[this.Children.Count - 1];
                        lastChild.Arrange(new Rect(childPos, new Size(lastChild.DesiredSize.Width, finalSize.Height)));
                        childPos.X = finalSize.Width - lastChild.DesiredSize.Width;
                        break;
                    case InsertionOrientation.BottomToTop:
                        lastChild = this.Children[this.Children.Count - 1];
                        lastChild.Arrange(new Rect(childPos, new Size(finalSize.Width, lastChild.DesiredSize.Height)));
                        childPos.Y = finalSize.Height - lastChild.DesiredSize.Height;
                        break;
                }
            }
            switch (Order)
            {
                case InsertionOrder.ReverseOrder:
                    {
                        for (var i = 0; i < Children.Count; i++)
                        {
                            SetChildPoint(ref finalSize, ref childPos, i);
                        }
                        break;
                    }
                case InsertionOrder.PositiveSequence:
                    {
                        for (var i = Children.Count - 1; i >= 0; i--)
                        {
                            SetChildPoint(ref finalSize, ref childPos, i);
                        }
                        break;
                    }
            }
            return finalSize;
        }

        private void SetChildPoint(ref Size finalSize, ref Point childPos, int i)
        {
            if (Children[i] is UIElement child)
            {
                switch (Orientation)
                {
                    case InsertionOrientation.LeftToRight:
                        child.Arrange(new Rect(childPos, new Size(child.DesiredSize.Width, child.DesiredSize.Height)));
                        childPos.X += child.DesiredSize.Width;
                        break;
                    case InsertionOrientation.RightToLeft:
                        child.Arrange(new Rect(childPos, new Size(child.DesiredSize.Width, child.DesiredSize.Height)));
                        childPos.X -= child.DesiredSize.Width;
                        break;
                    case InsertionOrientation.TopToBottom:
                        child.Arrange(new Rect(childPos, new Size(child.DesiredSize.Width, child.DesiredSize.Height)));
                        childPos.Y += child.DesiredSize.Height;
                        break;
                    case InsertionOrientation.BottomToTop:
                        child.Arrange(new Rect(childPos, new Size(child.DesiredSize.Width, child.DesiredSize.Height)));
                        childPos.Y -= child.DesiredSize.Height;
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
        PositiveSequence
    }
    /// <summary>
    /// 插入方向
    /// </summary>
    public enum InsertionOrientation
    {
        LeftToRight,
        RightToLeft,
        TopToBottom,
        BottomToTop
    }
}
