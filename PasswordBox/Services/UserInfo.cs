using PasswordBox.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace PasswordBox.Services
{
    class UserInfo
    {
        private static StorageFolder Folder => ApplicationData.Current.LocalFolder;
        private static ApplicationDataContainer Settings => ApplicationData.Current.LocalSettings;

        /// <summary>
        /// 将图片存起来
        /// </summary>
        /// <param name="pixels">图片bytes</param>
        /// <param name="name">文件名称</param>
        /// <returns></returns>
        public static async void SaveImage(byte[] pixels, string name)
        {
            StorageFile img = await Folder.CreateFileAsync(name, CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteBytesAsync(img, pixels);
        }

        /// <summary>
        /// 获取保存的图片
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static async Task<byte[]> GetImage(string name)
        {
            try
            {
                StorageFile img = await Folder.GetFileAsync(name);
                return await ImageHelper.AsByteArray(img);
            }
            catch (Exception)
            {
                // pass
            }
            return null;
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetInfo(string key)
        {
            return CheckIfExist(key) ? (string)Settings.Values[key] : null;
        }

        /// <summary>
        /// 更新或设置个人信息
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetInfo(string key, string value)
        {
            Settings.Values[key] = value;
        }

        /// <summary>
        /// 检查信息是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool CheckIfExist(string key)
        {
            return Settings.Values.ContainsKey(key);
        }
    }
}
