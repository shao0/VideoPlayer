using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using VideoPlayerVlc.Views;

namespace VideoPlayerVlc
{
    public class VideoPlayerVlcModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("PlayerContent", typeof(VlcPlayerView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
        }
    }
}