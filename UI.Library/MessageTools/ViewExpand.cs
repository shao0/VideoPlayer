using System.Windows;
using UI.Library.Controls;

namespace UI.Library.MessageTools
{
    public static class ViewExpand
    {
        public static void Show(this FrameworkElement element, string msg, ShowEnum showEnum = ShowEnum.ShowText, int outTime = 3)
        {
            if (showEnum == ShowEnum.ShowText) MimicryMessage.ShowMessage(element, msg, outTime);
        }

    }

    public enum ShowEnum
    {
        ShowText,
        ShowLoading
    }
}
