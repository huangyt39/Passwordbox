using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace PasswordBox
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class NewOrUpdate : Page
    {
        public NewOrUpdate()
        {
            this.InitializeComponent();
        }

        private byte[] ItemPic;
        /// <summary>
        /// save as a new password item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void SaveContent(object sender, RoutedEventArgs e)
        {
            var _title = title.Text;
            var _website = website.Text;
            var _account = account.Text;
            var _password = password.Password;
            if (_website != string.Empty && CheckWebsite(_website) == false)
            {
                ContentDialog tip;
                tip = new ContentDialog()
                {
                    Title = "提示",
                    PrimaryButtonText = "确认",
                    Content = "网址格式错误",
                    FullSizeDesired = false
                };
                tip.PrimaryButtonClick += (S, E) => { };
                await tip.ShowAsync();
                return;
            }
            if (_website != string.Empty && Services.HttpAccess.GetIco(_website) != null)
            {
                ItemPic = await Services.HttpAccess.GetIco(_website);
            }
            else
            {
                BitmapImage bitmap = new BitmapImage { UriSource = new Uri("ms-appx:///Assets/cat.png") };
                img.Source = bitmap;
                var photoFile = await StorageFile.GetFileFromApplicationUriAsync(bitmap.UriSource);
                ItemPic = await Common.ImageHelper.AsByteArray(photoFile);
            }
            if (ViewModel.StaticModel.ViewModel.selectedItem == null)
            {
                ViewModel.StaticModel.ViewModel.AddPasswordItem(_title, ItemPic, _website, _account, _password);
            }
            else
            {
                ViewModel.StaticModel.ViewModel.UpdatePasswordItem(_title, ItemPic, _website, _account, _password);
            }
            Frame.Navigate(typeof(Home));
        }

        /// <summary>
        /// check if the website is valid
        /// </summary>
        /// <param name="website"></param>
        /// <returns></returns>
        private bool CheckWebsite(string website)
        {
            return Services.HttpAccess.CheckURL(website);
        }

        /// <summary>
        /// auto generate a password
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GeneratePassword(object sender, RoutedEventArgs e)
        {
            password.Password = GetRandomString(16);
        }

        /// <summary>
        /// 获取随机码
        /// </summary>
        /// <param name="length">长度</param>
        /// <param name="availableChars">指定随机字符，为空，默认系统指定</param>
        /// <returns></returns>
        private string GetRandomString(int length, string availableChars = null)
        {
            if (availableChars == null) availableChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var id = new char[length];
            Random random = new Random();
            for (var i = 0; i < length; i++)
            {
                id[i] = availableChars[random.Next(0, availableChars.Length)];
            }
            return new string(id);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter != null)
            {
                LoadSelectedItem();
            }
        }

        private void LoadSelectedItem()
        {
            title.Text = ViewModel.StaticModel.ViewModel.selectedItem.Title;
            website.Text = ViewModel.StaticModel.ViewModel.selectedItem.Urlstr;
            account.Text = ViewModel.StaticModel.ViewModel.selectedItem.Account;
            password.Password = ViewModel.StaticModel.ViewModel.selectedItem.Password;
            ItemPic = ViewModel.StaticModel.ViewModel.selectedItem.Img;
        }

        private async void SelectPicture(object sender, RoutedEventArgs e)
        {
            var p = await Common.ImageHelper.Picker();
            if (p != null)
            {
                ItemPic = p; // 无效，待修改
            }
        }
    }
}
