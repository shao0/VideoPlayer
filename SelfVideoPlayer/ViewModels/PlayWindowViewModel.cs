using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using VideoPlayerLibrary.Events;
using VideoPlayerLibrary.Models;
using VideoPlayerLibrary.Tools;

namespace SelfVideoPlayer.ViewModels
{
    public class PlayWindowViewModel : BindableBase
    {
        private FrameworkElement View;

        private readonly IEventAggregator _ea;
        /// <summary>
        /// 影片播放信息集
        /// </summary>
        public ObservableCollection<FilmPlayInfo> FilmPlayInfoList { get; set; } = new ObservableCollection<FilmPlayInfo>();

        public PlayWindowViewModel(IEventAggregator ea)
        {
            _ea = ea; 
        }

        #region LoadedCommand 加载命令

        private DelegateCommand<object> _LoadedCommand;
        /// <summary>
        /// 加载命令
        /// </summary>
        public DelegateCommand<object> LoadedCommand => _LoadedCommand ?? (_LoadedCommand = new DelegateCommand<object>(Loaded));

        private async void Loaded(object obj)
        {
            if (obj is FrameworkElement frameworkElement)
            {
                View = frameworkElement;
                if (frameworkElement.Tag is FilmInfo filmInfo)
                {
                    var html = await WebHttpRequest.GetStringAsync(filmInfo.Url);
                    html = html.Replace("\t", string.Empty).Replace("\b", string.Empty).Replace("\0", string.Empty)
                        .Replace("\r", string.Empty).Replace("\n", string.Empty); ;
                    string zz = "var COVER_INFO = ([\\s\\S]*?)var COLUMN_INFO = ";
                    MatchCollection match = new Regex(zz).Matches(html);
                    if (match.Count > 0)
                    {
                        JObject jObject = JObject.Parse(match[0].Result("$1"));
                        JToken arr = jObject["video_ids"];
                        FilmPlayInfoList.Clear();
                        FilmPlayInfo filmPlayInfo;
                        for (int i = 0; i < arr.Count(); i++)
                        {
                            filmPlayInfo = new FilmPlayInfo();
                            filmPlayInfo.Episode = i + 1;
                            filmPlayInfo.PlayUrl = filmInfo.Url.Replace(".html", $"/{arr[i]}.html");
                            filmPlayInfo.Title = filmInfo.Title;
                            FilmPlayInfoList.Add(filmPlayInfo);
                        }

                    }
                }
            }
        }

        #endregion


        #region PlayVideoCommand 播放视频命令

        private DelegateCommand<object> _PlayVideoCommand;
        /// <summary>
        /// 播放视频命令
        /// </summary>
        public DelegateCommand<object> PlayVideoCommand => _PlayVideoCommand ?? (_PlayVideoCommand = new DelegateCommand<object>(PlayVideo));

        private async void PlayVideo(object obj)
        {
            if (obj is FilmPlayInfo filmPlayInfo)
            {
                try
                {
                    var html = await WebHttpRequest.GetStringAsync($"https://api.kk06.top/?url=%20{filmPlayInfo.PlayUrl}");
                    html = html.Replace("\t", string.Empty).Replace("\b", string.Empty).Replace("\0", string.Empty)
                        .Replace("\r", string.Empty).Replace("\n", string.Empty);

                    string zz = "var urls = \"([\\s\\S]*?)\";";

                    MatchCollection match = new Regex(zz).Matches(html);
                    if (match.Count > 0)
                    {
                        filmPlayInfo.PlayUrl = match[0].Result("$1");
                        _ea.GetEvent<UrlChangeSentEvent>().Publish(filmPlayInfo);
                        return;
                    }

                    html = await WebHttpRequest.GetStringAsync(
                        $"https://z1.m1907.cn/api/v/?z=22423808d4caf1c00927b1f988f8e61b&jx=%20{filmPlayInfo.PlayUrl}&s1ig=11397");
                    JObject jObject = JObject.Parse(html);
                    if (jObject["type"].ToString() == "tv")
                    {
                        filmPlayInfo.PlayUrl = jObject["data"][0]["source"]["eps"][filmPlayInfo.Episode - 1]["url"].ToString();
                        _ea.GetEvent<UrlChangeSentEvent>().Publish(filmPlayInfo);
                        return;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                MessageBox.Show("解析失败");
            }
        }

        #endregion


    }
}
