using System.Collections.Generic;

namespace VideoPlayerLibrary.Dictionary
{
    /// <summary>
    /// 电视剧字典
    /// </summary>
    public class TvPlayDictionary
    {

        #region 电视剧字典
        /// <summary>
        /// 排序字典
        /// </summary>
        public  Dictionary<int, string> SortDictionary { get; set; } = new Dictionary<int, string>
        {
            {19,"最新"},
            {18,"最热"},
            {16,"好评"},
            {21,"口碑好剧"},
            {54,"高分好评"},
            {22,"知乎高分"},
        };

        /// <summary>
        /// 类型字典
        /// </summary>
        public  Dictionary<int, string> FeatureDictionary { get; set; } = new Dictionary<int, string>
        {
            {-1, "全部"},
            {1, "偶像爱情"},
            {2, "古装历史"},
            {3, "玄幻史诗"},
            {4, "都市生活"},
            {14, "当代主旋律"},
            {5, "罪案谍战"},
            {6, "历险科幻"},
            {7, "军旅抗战"},
            {8, "喜剧"},
            {9, "武侠江湖"},
            {10, "青春校园"},
            {11, "时代传奇"},
            {12, "体育电竞"},
            {13, "真人动漫"},
            {10471, "网络剧"},
            {44, "独播"},
        };
        /// <summary>
        /// 区域字典
        /// </summary>
        public  Dictionary<int, string> AreaDictionary { get; set; } = new Dictionary<int, string>
        {
            {-1,"全部"},
            {814,"内地"},
            {815,"美国"},
            {816,"英国"},
            {818,"韩国"},
            {9,"泰国"},
            {10,"日本"},
            {14,"中国香港"},
            {817,"中国台湾"},
            {819,"其他"},
        };
        /// <summary>
        /// 年份字典
        /// </summary>
        public  Dictionary<int, string> YearDictionary { get; set; } = new Dictionary<int, string>
        {
            {-1,"全部"},
            {2020,"2020"},
            {4061,"2019"},
            {4060,"2018"},
            {2017,"2017"},
            {860,"2016"},
            {861,"2014"},
            {862,"2013"},
            {863,"2012"},
            {864,"2011"},
            {865,"2010"},
            {866,"其他"},
        };
        #endregion
        #region 动漫字典

        public  Dictionary<int, string> CartoonSortDictionary { get; set; } = new Dictionary<int, string>
        {
            {18,"最热"},
            {23,"最新"},
            {20,"好评"},
            {22,"知乎高分"},
        };

        public  Dictionary<int, string> CartoonTypeDictionary { get; set; } = new Dictionary<int, string>
        {
            {-1,"全部"},
            {2,"冒险"},
            {5,"战斗"},
            {1,"搞笑"},
            {3,"经典"},
            {4,"科幻"},
            {9,"玄幻"},
            {6,"魔幻"},
            {13,"武侠"},
            {10,"竞技"},
            {7,"恋爱"},
            {14,"推理"},
            {11,"腾讯出品"},
            {15,"日常"},
            {16,"校园"},
            {17,"悬疑"},
            {18,"真人"},
            {19,"历史"},
            {20,"竞技"},
            {12,"其他"},
        };

        public  Dictionary<int, string> CartoonAreaDictionary { get; set; } = new Dictionary<int, string>
        {
            {-1,"全部"},
            {1,"内地"},
            {2,"日本"},
            {3,"欧美"},
            {4,"其他"},
        };

        public  Dictionary<int, string> CartoonYearDictionary { get; set; } = new Dictionary<int, string>
        {
            {-1,"全部"},
            {2021,"2021"},
            {50,"2020"},
            {11,"2019"},
            {2018,"2018"},
            {2017,"2017"},
            {1,"2016"},
            {2,"2015"},
            {3,"2014"},
            {4,"2013"},
            {5,"2012"},
            {6,"2011"},
            {7,"00年代"},
            {8,"90年代"},
            {9,"80年代"},
            {10,"更早"},
        };
        public  Dictionary<int, string> CartoonStatusDictionary { get; set; } = new Dictionary<int, string>
        {
            {-1,"全部"},
            {46,"预告片"},
            {44,"连载"},
            {45,"完结"},
        };
        public  Dictionary<int, string> CartoonItemDictionary { get; set; } = new Dictionary<int, string>
        {
            {1,"全部"},
            {2,"3D动画"},
            {3,"2D动画"},
            {4,"特摄"},
            {5,"其他"},
        };

        #endregion

    }
}
