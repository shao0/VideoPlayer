using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace UI.Library.Helpers
{
    public static class AnimationHelper
    {
        /// <summary>
        /// 绑定double类型动画
        /// </summary>
        /// <param name="bindingObject">绑定对象</param>
        /// <param name="dependencyProperty">绑定属性</param>
        /// <param name="toValue">绑定属性达到值</param>
        /// <param name="milliseconds">属性达到绑定值时间(毫秒)</param>
        /// <param name="completed">动画完成委托</param>
        public static void BindingDoubleAnimation(this Animatable bindingObject, DependencyProperty dependencyProperty, double toValue, double milliseconds = 500, Action completed = null)
        {
            var doubleAnimation = new DoubleAnimation(toValue, new Duration(TimeSpan.FromMilliseconds(milliseconds)));
            doubleAnimation.EasingFunction = new CubicEase{ EasingMode = EasingMode.EaseOut };
            if (completed != null)
            {
                doubleAnimation.Completed += (_, __) => completed.Invoke();
            }
            bindingObject.BeginAnimation(dependencyProperty, doubleAnimation);
        }
    }
}
