using System.Windows;

namespace UI.Library.MessageTools
{
    public static class ViewExpand
    {
        public static void Show(this FrameworkElement element, string msg, ShowEnum showEnum = ShowEnum.ShowText, uint outTime = 3000)
        {
            if (showEnum == ShowEnum.ShowText) MessageAttached.Show(element, msg, outTime);
        }

    }

    public enum ShowEnum
    {
        ShowText,
        ShowLoading
    }
}
