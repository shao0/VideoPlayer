using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CefSharp;
using CefSharp.Wpf;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using VideoPlayerLibrary.Events;
using VideoPlayerLibrary.Models;

namespace VideoPlayerCefModule.ViewModels
{
    public class CefPlayerViewModel : BindableBase
    {
        private FrameworkElement View;

        private readonly IEventAggregator _ea;

        public CefPlayerViewModel(IEventAggregator ea)
        {
            _ea = ea;
        }

        #region object Web 网页
        /// <summary>
        /// 网页 字段
        /// </summary>
        private ChromiumWebBrowser _Web;
        /// <summary>
        /// 网页 属性
        /// </summary>
        public ChromiumWebBrowser Web
        {
            get => _Web;
            set
            {
                if (_Web != value)

                {
                    _Web = value;
                    RaisePropertyChanged();
                }
            }
        }
        #endregion

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
                View = frameworkElement;
                Web = new ChromiumWebBrowser();
                _ea.GetEvent<UrlChangeSentEvent>().Subscribe(UrlChange);
            }
        }
        private void UrlChange(FilmPlayInfo obj)
        {
            Web.Title = obj.Title;
            Web.Address = obj.PlayUrl;
        }

        #endregion


    }
}
