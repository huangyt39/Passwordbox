using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace PasswordBox.Common
{
    class ImageHelper
    {
        private static bool IsOpened = false;

        #region 当找不到favicon.ico时的默认图片

        public static string DefaultImageUri
        {
            get;
        } = "ms-appx:///Assets/passwordIcon.png";

        static public byte[] DefaultImg { get; private set; }

        /// <summary>
        /// 获得默认图片
        /// </summary>
        /// <returns></returns>
        public static async Task GetDefaultPixels()
        {
            StorageFile imgFile;
            imgFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri(DefaultImageUri));
            DefaultImg = await AsByteArray(imgFile);
        }

        #endregion

        /// <summary>
        /// 用户选图片
        /// </summary>
        /// <returns>当成功时放回图片的bytes，否则为null</returns>
        public static async Task<byte[]> Picker()
        {
            if (IsOpened == true)
                return null;
            else
                IsOpened = true;

            Windows.Storage.Pickers.FileOpenPicker picker
                = new Windows.Storage.Pickers.FileOpenPicker
                {
                    ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail,
                    SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary
                };

            picker.FileTypeFilter.Clear();
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".png");
            picker.FileTypeFilter.Add(".jpeg");

            StorageFile imgFile = await picker.PickSingleFileAsync();
            byte[] pixels = null;
            if (imgFile != null)
            {
                pixels = await AsByteArray(imgFile);
            }
            IsOpened = false;
            return pixels;
        }

        public static async Task<byte[]> AsByteArray(StorageFile file)
        {
            var stream = await file.OpenStreamForReadAsync();
            var bytes = new byte[(int)stream.Length];
            stream.Read(bytes, 0, (int)stream.Length);
            return bytes;
        }

    }
}
