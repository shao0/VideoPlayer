using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.DryIoc;
using Prism.Mvvm;
using SelfVideoPlayer.Views;
using UI.Library.MessageTools;
using VideoPlayerLibrary.Dictionary;
using VideoPlayerLibrary.Models;
using VideoPlayerLibrary.Tools;

namespace SelfVideoPlayer.ViewModels
{
    public class DramaLibraryViewModel : BindableBase
    {
        private FrameworkElement View;
        private readonly PrismApplication _app;

        public TvPlayDictionary PlayDictionary { get; }

        /// <summary>
        /// 影片信息集合
        /// </summary>
        public ObservableCollection<FilmInfo> FilmInfoList { get; set; } = new ObservableCollection<FilmInfo>();
        /// <summary>
        /// 每次请求的数据条数
        /// </summary>
        private readonly int PageSize = 3 * 9;

        public DramaLibraryViewModel(PrismApplication app, TvPlayDictionary tvPlayDictionary)
        {
            _app = app;
            PlayDictionary = tvPlayDictionary;
            InitialFilterValue();
        }

        private void InitialFilterValue()
        {
            _SortValue = PlayDictionary.SortDictionary.First().Key;
            _FeatureValue = PlayDictionary.FeatureDictionary.First().Key;
            _AreaValue = PlayDictionary.AreaDictionary.First().Key; 
            _YearValue = PlayDictionary.YearDictionary.First().Key;
            _CartoonSortValue = PlayDictionary.CartoonSortDictionary.First().Key; 
            _CartoonTypeValue = PlayDictionary.CartoonTypeDictionary.First().Key;
            _cartoonAreaValue = PlayDictionary.CartoonAreaDictionary.First().Key;
            _CartoonYearValue = PlayDictionary.CartoonYearDictionary.First().Key;
            _cartoonStatusValue = PlayDictionary.CartoonStatusDictionary.First().Key;
            _CartoonItemValue = PlayDictionary.CartoonItemDictionary.First().Key;
        }


        #region 电视剧筛选属性

        #region int SortValue 排序值
        /// <summary>
        /// 排序值 字段
        /// </summary>
        private int _SortValue;
        /// <summary>
        /// 排序值 属性
        /// </summary>
        public int SortValue
        {
            get => _SortValue;
            set
            {
                if (_SortValue != value)
                {
                    _SortValue = value;
                    RaisePropertyChanged();
                    PageCurrent = 1;
                    LoadFilmInfo();
                }
            }
        }
        #endregion

        #region int FeatureValue 类型值
        /// <summary>
        /// 类型值 字段
        /// </summary>
        private int _FeatureValue;
        /// <summary>
        /// 类型值 属性
        /// </summary>
        public int FeatureValue
        {
            get => _FeatureValue;
            set
            {
                if (_FeatureValue != value)
                {
                    _FeatureValue = value;
                    RaisePropertyChanged();
                    PageCurrent = 1;
                    LoadFilmInfo();
                }
            }
        }
        #endregion

        #region int AreaValue 区域值
        /// <summary>
        /// 区域值 字段
        /// </summary>
        private int _AreaValue;
        /// <summary>
        /// 区域值 属性
        /// </summary>
        public int AreaValue
        {
            get => _AreaValue;
            set
            {
                if (_AreaValue != value)
                {
                    _AreaValue = value;
                    RaisePropertyChanged();
                    PageCurrent = 1;
                    LoadFilmInfo();
                }
            }
        }
        #endregion

        #region int YearValue 年份值
        /// <summary>
        /// 年份值 字段
        /// </summary>
        private int _YearValue;
        /// <summary>
        /// 年份值 属性
        /// </summary>
        public int YearValue
        {
            get => _YearValue;
            set
            {
                if (_YearValue != value)
                {
                    _YearValue = value;
                    RaisePropertyChanged();
                    PageCurrent = 1;
                    LoadFilmInfo();
                }
            }
        }
        #endregion

        #endregion

        #region 动漫筛选属性

        #region int CartoonSortValue 动漫排序
        /// <summary>
        /// 动漫排序 字段
        /// </summary>
        private int _CartoonSortValue;
        /// <summary>
        /// 动漫排序 属性
        /// </summary>
        public int CartoonSortValue
        {
            get => _CartoonSortValue;
            set
            {
                if (_CartoonSortValue != value)
                {
                    _CartoonSortValue = value;
                    RaisePropertyChanged();
                    PageCurrent = 1;
                    LoadFilmInfo();
                }
            }
        }
        #endregion


        #region int CartoonTypeValue 类型
        /// <summary>
        /// 类型 字段
        /// </summary>
        private int _CartoonTypeValue;
        /// <summary>
        /// 类型 属性
        /// </summary>
        public int CartoonTypeValue
        {
            get => _CartoonTypeValue;
            set
            {
                if (_CartoonTypeValue != value)
                {
                    _CartoonTypeValue = value;
                    RaisePropertyChanged();
                    PageCurrent = 1;
                    LoadFilmInfo();
                }
            }
        }
        #endregion

        #region int CartoonAreaValue 地区
        /// <summary>
        /// 地区 字段
        /// </summary>
        private int _cartoonAreaValue;
        /// <summary>
        /// 地区 属性
        /// </summary>
        public int CartoonAreaValue
        {
            get => _cartoonAreaValue;
            set
            {
                if (_cartoonAreaValue != value)
                {
                    _cartoonAreaValue = value;
                    RaisePropertyChanged();
                    PageCurrent = 1;
                    LoadFilmInfo();
                }
            }
        }
        #endregion

        #region int CartoonYearValue 时间
        /// <summary>
        /// 时间 字段
        /// </summary>
        private int _CartoonYearValue;
        /// <summary>
        /// 时间 属性
        /// </summary>
        public int CartoonYearValue
        {
            get => _CartoonYearValue;
            set
            {
                if (_CartoonYearValue != value)
                {
                    _CartoonYearValue = value;
                    RaisePropertyChanged();
                    PageCurrent = 1;
                    LoadFilmInfo();
                }
            }
        }
        #endregion

        #region int CartoonStatusValue 状态
        /// <summary>
        /// 状态 字段
        /// </summary>
        private int _cartoonStatusValue;
        /// <summary>
        /// 状态 属性
        /// </summary>
        public int CartoonStatusValue
        {
            get => _cartoonStatusValue;
            set
            {
                if (_cartoonStatusValue != value)
                {
                    _cartoonStatusValue = value;
                    RaisePropertyChanged();
                    PageCurrent = 1;
                    LoadFilmInfo();
                }
            }
        }
        #endregion

        #region int CartoonItemValue 分类

        /// <summary>
        /// 分类 字段
        /// </summary>
        private int _CartoonItemValue;
        /// <summary>
        /// 分类 属性
        /// </summary>
        public int CartoonItemValue
        {
            get => _CartoonItemValue;
            set
            {
                if (_CartoonItemValue != value)
                {
                    _CartoonItemValue = value;
                    RaisePropertyChanged();
                    PageCurrent = 1;
                    LoadFilmInfo();
                }
            }
        }
        #endregion

        #endregion

        #region int PageCurrent 当前页
        /// <summary>
        /// 当前页 字段
        /// </summary>
        private int _PageCurrent = 1;
        /// <summary>
        /// 当前页 属性
        /// </summary>
        public int PageCurrent
        {
            get => _PageCurrent;
            set
            {
                if (_PageCurrent != value)
                {
                    _PageCurrent = value;
                    RaisePropertyChanged();
                }
            }
        }
        #endregion

        #region int PageCount 总页数
        /// <summary>
        /// 总页数 字段
        /// </summary>
        private int _PageCount;
        /// <summary>
        /// 总页数 属性
        /// </summary>
        public int PageCount
        {
            get => _PageCount;
            set
            {
                if (_PageCount != value)
                {
                    _PageCount = value;
                    RaisePropertyChanged();
                }
            }
        }
        #endregion

        #region int SelectFilmType 选择的影片类型
        /// <summary>
        /// 选择的影片类型 字段
        /// </summary>
        private int _SelectFilmType;
        /// <summary>
        /// 选择的影片类型 属性
        /// </summary>
        public int SelectFilmType
        {
            get => _SelectFilmType;
            set
            {
                if (_SelectFilmType != value)
                {
                    _SelectFilmType = value;
                    RaisePropertyChanged();
                    PageCurrent = 1;
                    LoadFilmInfo();
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

        private async void Loaded(object obj)
        {
            if (obj is FrameworkElement frameworkElement)
            {
                View = frameworkElement;
                //   string url = "https://v.qq.com/x/bu/pagesheet/list?_all=1&append=1&channel=tv&feature=1&iarea=814&listpage=2&offset=30&pagesize=30&pay=-1&sort=18&year=2020";//电视剧

                // url = "https://v.qq.com/channel/cartoon?_all=1&anime_status=-1&channel=cartoon&iarea=-1&ipay=-1&item=1&itype=-1&iyear=-1&listpage=1&sort=18";//动漫
                //url ="https://v.qq.com/channel/variety?_all=1&channel=variety&exclusive=-1&iarea=-1&ipay=-1&itype=-1&iyear=-1&listpage=1&sort=5";//综艺
                await LoadFilmInfo();
            }
        }
        /// <summary>
        /// 加载影片信息
        /// </summary>
        /// <returns></returns>
        private async Task LoadFilmInfo()
        {
            try
            {
                string url = GetUrl();
                string _html = await WebHttpRequest.GetStringAsync(url);
                _html = _html.Replace("\t", string.Empty).Replace("\b", string.Empty).Replace("\0", string.Empty)
                    .Replace("\r", string.Empty).Replace("\n", string.Empty);
                string zz =
                    "<div class=\"list_item\" __wind><a href=\"([\\s\\S]*?)\" class=\"figure\" target=\"_blank\" tabindex=\"-1\" data-float=\"([\\s\\S]*?)\" title=\"([\\s\\S]*?)\"><img class=\"figure_pic\" src=\"([\\s\\S]*?)\"";
                MatchCollection match = new Regex(zz).Matches(_html);
                Hashtable hashtable = new Hashtable();
                hashtable.Add("Host", "puui.qpic.cn");
                FilmInfoList.Clear();
                foreach (Match m in match)
                {
                    var image = await WebHttpRequest.GetByteAsync($"http:{m.Result("$4")}", header: hashtable);
                    FilmInfoList.Add(new FilmInfo
                    {
                        ImageSource = image.ConvertBitmapImage(),
                        Title = m.Result("$3"),
                        Url = m.Result("$1"),
                    });
                }

                zz = "_stat2=\"([\\s\\S]*?)\">([\\d]*?)</button><button class=\"page_next \"";
                match = new Regex(zz).Matches(_html);
                PageCount = match.Count > 0 ? int.Parse(match[0].Result("$2")) : 0;
            }
            catch (Exception e)
            {
               View.Show($"解析失败:{e.Message}");
            }
        }

        string GetUrl()
        {
            string result = string.Empty;
            switch (SelectFilmType)
            {
                case 1:
                    result =
                        $"https://v.qq.com/x/bu/pagesheet/list?_all=1&anime_status={CartoonStatusValue}&append=1&channel=cartoon&iarea={CartoonAreaValue}&ipay=-1&item={CartoonItemValue}&itype={CartoonTypeValue}&iyear={CartoonYearValue}&listpage={PageCurrent}&offset={(PageCurrent - 1) * PageSize}&pagesize={PageSize}&sort={CartoonSortValue}";
                    break;

                case 0:
                default:
                    result = $"https://v.qq.com/x/bu/pagesheet/list?_all=1&append=1&channel=tv&feature={FeatureValue}&iarea={AreaValue}&listpage={PageCurrent}&offset={(PageCurrent - 1) * PageSize}&pagesize={PageSize}&pay=-1&sort={SortValue}&year={YearValue}";
                    break;
            }
            return result;
        }
        #endregion

        #region SwitchCommand 切换命令

        private DelegateCommand<object> _SwitchCommand;
        /// <summary>
        /// 切换命令
        /// </summary>
        public DelegateCommand<object> SwitchCommand => _SwitchCommand ?? (_SwitchCommand = new DelegateCommand<object>(Switch));

        private void Switch(object obj)
        {
            if (obj is string s)
            {

            }
        }

        #endregion

        #region NextPageCommand 下一页命令

        private DelegateCommand _NextPageCommand;
        /// <summary>
        /// 下一页命令
        /// </summary>
        public DelegateCommand NextPageCommand => _NextPageCommand ?? (_NextPageCommand = new DelegateCommand(NextPage));

        private async void NextPage()
        {
            if (PageCurrent < PageCount)
            {
                PageCurrent++;
                await LoadFilmInfo();
            }
        }

        #endregion

        #region PreviousPageCommand 上一页命令

        private DelegateCommand _PreviousPageCommand;
        /// <summary>
        /// 上一页命令
        /// </summary>
        public DelegateCommand PreviousPageCommand => _PreviousPageCommand ?? (_PreviousPageCommand = new DelegateCommand(PreviousPage));

        private async void PreviousPage()
        {
            if (PageCurrent > 1)
            {
                PageCurrent--;
                await LoadFilmInfo();
            }
        }

        #endregion

        #region GoCommand 跳转页命令

        private DelegateCommand<object> _GoCommand;
        /// <summary>
        /// 跳转页命令
        /// </summary>
        public DelegateCommand<object> GoCommand => _GoCommand ?? (_GoCommand = new DelegateCommand<object>(Go));

        private async void Go(object obj)
        {
            if (obj is string s)
            {
                var go = int.Parse(s);
                if (go > 0 && go < PageCount)
                {
                    PageCurrent = go;
                    await LoadFilmInfo();
                }
            }
        }

        #endregion

        #region PlayWindowCommand 播放窗口命令

        private DelegateCommand<object> _PlayWindowCommand;
        /// <summary>
        /// 播放窗口命令
        /// </summary>
        public DelegateCommand<object> PlayWindowCommand => _PlayWindowCommand ?? (_PlayWindowCommand = new DelegateCommand<object>(PlayWindow));

        private void PlayWindow(object obj)
        {
            if (obj is FilmInfo filmInfo)
            {
                PlayWindow playWindow = (PlayWindow)_app.Container.Resolve(typeof(PlayWindow));
                playWindow.Owner = Application.Current.MainWindow;
                playWindow.ShowDialog(filmInfo);
            }
        }

        #endregion



    }
}
