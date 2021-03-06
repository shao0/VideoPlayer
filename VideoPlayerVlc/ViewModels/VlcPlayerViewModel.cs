using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using VideoPlayerLibrary.Events;
using VideoPlayerLibrary.Models;
using Vlc.DotNet.Core;
using Vlc.DotNet.Wpf;

namespace VideoPlayerVlc.ViewModels
{
    public class VlcPlayerViewModel : BindableBase
    {
        private readonly IEventAggregator _ea;

        /// <summary>
        /// 视图控件
        /// </summary>
        private FrameworkElement View;

        private string Url;
        public VlcPlayerViewModel(IEventAggregator ea)
        {
            _ea = ea;
            LoadedCommand = new DelegateCommand<object>(Loaded);
            PlayCommand = new DelegateCommand(Play);
        }

        private VlcControl _Vlc;

        public VlcControl Vlc
        {
            get => _Vlc;
            set
            {
                _Vlc = value;
                RaisePropertyChanged();
            }
        }

        #region double CurrentValue 当前值
        /// <summary>
        /// 当前值 字段
        /// </summary>
        private long _CurrentValue;
        /// <summary>
        /// 当前值 属性
        /// </summary>
        public long CurrentValue
        {
            get => _CurrentValue;
            set
            {
                if (_CurrentValue != value)
                {
                    _CurrentValue = value;
                    Vlc.SourceProvider.MediaPlayer.Position = (float)(_CurrentValue*1d / CountValue);
                    RaisePropertyChanged();
                }
            }
        }
        public TimeSpan CurrentTime => TimeSpan.FromSeconds(_CurrentValue );
        #endregion


        #region double CountValue 总值
        /// <summary>
        /// 总值 字段
        /// </summary>
        private long _CountValue;
        /// <summary>
        /// 总值 属性
        /// </summary>
        public long CountValue
        {
            get => _CountValue;
            set
            {
                if (_CountValue != value)
                {
                    _CountValue = value;
                    RaisePropertyChanged();
                    RaisePropertyChanged(nameof(CountTime));
                }
            }
        }
        public TimeSpan CountTime => TimeSpan.FromSeconds(_CountValue);
        #endregion



        public DelegateCommand<object> LoadedCommand { get; }

        private void Loaded(object obj)
        {
            if (obj is FrameworkElement element)
            {
                View = element;
                Vlc = new VlcControl();
                //创建播放器
                string appPath = AppDomain.CurrentDomain.BaseDirectory; //获取输出目录
                //根据系统32/64选择文件夹，否则会报错 不是有效的 Win32 应用程序。
                //IntPtr.Size == 4 表示当前程序是32位 x86的 
                DirectoryInfo vlcLibDirectory = new DirectoryInfo(Path.Combine(appPath, "VLC"));//vlc文件的地址
                //配置项
                string[] options = {
                    //添加日志
                    //   "--file-logging", "-vvv", "--logfile=Logs.log"
                    "--network-caching=300",//尝试读取或写入受保护的内存。这通常指示其他内存已损坏。”
                };
                //创建播放器
                Vlc.SourceProvider.CreatePlayer(vlcLibDirectory, options);

                _ea.GetEvent<UrlChangeSentEvent>().Subscribe(UrlChange);
            }
        }
        private void UrlChange(FilmPlayInfo obj)
        {
            Url = obj.PlayUrl;
            Play();
        }
        public DelegateCommand PlayCommand { get; }

        private async void Play()
        {
            if (string.IsNullOrWhiteSpace(Url)) return;
            await Task.Run(() => Vlc.SourceProvider.MediaPlayer.Stop());

            var url = new Uri(Url);

            await Task.Run(() => Vlc.SourceProvider.MediaPlayer.Play(url));

            //添加播放事件
            Vlc.SourceProvider.MediaPlayer.EndReached += MediaPlayer_EndReached;//播放结束
            Vlc.SourceProvider.MediaPlayer.PositionChanged += MediaPlayer_PositionChanged;//播放位置改变事件-刷新播放进度
            Vlc.SourceProvider.MediaPlayer.LengthChanged += MediaPlayer_LengthChanged;//获取播放总时长
            Vlc.SourceProvider.MediaPlayer.TimeChanged += MediaPlayer_TimeChanged;//获取播放当前时间
            Vlc.SourceProvider.MediaPlayer.Rate = 1;
        }
        /// <summary>
        /// 播放结束
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MediaPlayer_EndReached(object sender, VlcMediaPlayerEndReachedEventArgs e)
        {

        }

        /// <summary>
        /// 获取播放当前时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MediaPlayer_TimeChanged(object sender, VlcMediaPlayerTimeChangedEventArgs e)
        {
            _CurrentValue = e.NewTime/1000;
            RaisePropertyChanged(nameof(CurrentValue));
            RaisePropertyChanged(nameof(CurrentTime));
        }
        /// <summary>
        /// 获取播放总时长
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MediaPlayer_LengthChanged(object sender, VlcMediaPlayerLengthChangedEventArgs e)
        {
            CountValue = e.NewLength / 1000;
        }
        /// <summary>
        /// 播放位置改变事件-刷新播放进度
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MediaPlayer_PositionChanged(object sender, VlcMediaPlayerPositionChangedEventArgs e)
        {
            //_ProgressValue = e.NewPosition;
            //RaisePropertyChanged(nameof(ProgressValue));
        }


        #region UnloadedCommand 卸载命令

        private DelegateCommand _UnloadedCommand;
        /// <summary>
        /// 卸载命令
        /// </summary>
        public DelegateCommand UnloadedCommand => _UnloadedCommand ?? (_UnloadedCommand = new DelegateCommand(Unloaded));

        private async void Unloaded()
        {
            await Task.Run(() => Vlc.SourceProvider.MediaPlayer.Stop());
            Vlc.SourceProvider.MediaPlayer.EndReached -= MediaPlayer_EndReached;//播放结束
            Vlc.SourceProvider.MediaPlayer.PositionChanged -= MediaPlayer_PositionChanged;//播放位置改变事件-刷新播放进度
            Vlc.SourceProvider.MediaPlayer.LengthChanged -= MediaPlayer_LengthChanged;//获取播放总时长
            Vlc.SourceProvider.MediaPlayer.TimeChanged -= MediaPlayer_TimeChanged;//获取播放当前时间
            _ea.GetEvent<UrlChangeSentEvent>().Unsubscribe(UrlChange);
        }

        #endregion

    }
}
