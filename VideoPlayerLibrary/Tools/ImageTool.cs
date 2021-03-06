using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace VideoPlayerLibrary.Tools
{
    /// <summary>
    /// 扩展方法
    /// </summary>
    public static class ImageTool
    {
        /// <summary>
        /// 根据图片路径转换为图片二进制byte
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static byte[] ConvertBytes(this string path)
        {
            try
            {
                byte[] array;
                using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    array = new byte[fileStream.Length];
                    fileStream.Read(array, 0, array.Length);
                }
                return array;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// 根据图片二进制byte数据转换为图片位图
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static BitmapImage ConvertBitmapImage(this byte[] bytes)
        {
            try
            {
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.CreateOptions = BitmapCreateOptions.DelayCreation;
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = new MemoryStream(bytes);
                bitmapImage.EndInit();
                bitmapImage.Freeze();
                return bitmapImage;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// 根据图片路径转换为图片位图
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static BitmapImage ConvertBitmapImage(this string path)
        {
            return path.ConvertBytes().ConvertBitmapImage();
        }


    }
}
