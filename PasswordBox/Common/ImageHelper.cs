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
        } = "ms-appx:///Assets/cat.jpg";

        public static byte[] DefaultPixels { get; private set; }

        /// <summary>
        /// 打开应用时调用，可避免以后使用async/await
        /// </summary>
        /// <returns></returns>
        public static async Task GetDefaultPixels()
        {
            StorageFile imgFile;
            imgFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri(DefaultImageUri));
            DefaultPixels = await AsByteArray(imgFile);
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

        /// <summary>
        /// 可能用不上，记得删掉
        /// </summary>
        /// <param name="pixels">图片bytes</param>
        /// <returns>BitmapImage</returns>
        public static async Task<BitmapImage> AsBitmapImage(byte[] pixels)
        {
            if (pixels == null) return null;
            BitmapImage bitmap = new BitmapImage();
            using (MemoryStream stream = new MemoryStream(pixels))
            {
                await bitmap.SetSourceAsync(stream.AsRandomAccessStream());
            }
            return bitmap;
        }

        /*
        /// <summary>
        /// item 存图片
        /// </summary>
        /// <param name="imageBuffer"></param>
        /// <returns></returns>
        public static async Task<ImageSource> SaveToImageSource(byte[] imageBuffer)
        {
            ImageSource imageSource = null;
            using (MemoryStream stream = new MemoryStream(imageBuffer))
            {
                var ras = stream.AsRandomAccessStream();
                BitmapDecoder decoder = await BitmapDecoder.CreateAsync(BitmapDecoder.JpegDecoderId, ras);
                var provider = await decoder.GetPixelDataAsync();
                byte[] buffer = provider.DetachPixelData();
                WriteableBitmap bitmap = new WriteableBitmap((int)decoder.PixelWidth, (int)decoder.PixelHeight);
                await bitmap.PixelBuffer.AsStream().WriteAsync(buffer, 0, buffer.Length);
                imageSource = bitmap;
            }
            return imageSource;
        }
        */
    }
}
