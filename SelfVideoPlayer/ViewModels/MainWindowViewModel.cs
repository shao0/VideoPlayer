using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CefSharp;
using CefSharp.Wpf;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace SelfVideoPlayer.ViewModels
{
    public class MainWindowViewModel:BindableBase
    {
        private readonly IRegionManager _regionManager;

        public MainWindowViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }


        #region LoadedCommand 加载命令

        private DelegateCommand<object> _LoadedCommand;
        /// <summary>
        /// 加载命令
        /// </summary>
        public DelegateCommand<object> LoadedCommand => _LoadedCommand ?? (_LoadedCommand = new DelegateCommand<object>(Loaded));

        private void Loaded(object obj)
        {
            if (obj is FrameworkElement frameworkElement)
            {
                _regionManager.Regions["ContentRegion"].RequestNavigate("DramaLibraryView");
                //var setting = new CefSettings();
                //Cef.Initialize(setting);
            }

        }

        #endregion


    }
}
