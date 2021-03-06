using System.Windows;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Modularity;
using SelfVideoPlayer.ViewModels;
using SelfVideoPlayer.Views;
using VideoPlayerLibrary.Dictionary;
using VideoPlayerVlc;

namespace SelfVideoPlayer
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<PrismApplication>(() => this);
            containerRegistry.RegisterSingleton<TvPlayDictionary>(() => new TvPlayDictionary());
            containerRegistry.RegisterForNavigation<DramaLibraryView, DramaLibraryViewModel>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<VideoPlayerVlcModule>();
            base.ConfigureModuleCatalog(moduleCatalog);
        }
    }
}
