using PasswordBox.Common;
using PasswordBox.Model;
using PasswordBox.Services;
using PasswordBox.ViewModel;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
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

        private PasswordItem item = new PasswordItem("", null, "", "", "");

        /// <summary>
        /// save as a new password item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void SaveContent(object sender, RoutedEventArgs e)
        {
            if (item.Urlstr != string.Empty && CheckWebsite(item.Urlstr) == false)
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
            if (StaticModel.ViewModel.selectedItem == null)
            {
                StaticModel.ViewModel.AddPasswordItem(item.Title, item.Img, item.Urlstr, item.Account, item.Password);
            }
            else
            {
                StaticModel.ViewModel.UpdatePasswordItem(item.Title, item.Img, item.Urlstr, item.Account, item.Password);
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
            return HttpAccess.CheckURL(website);
        }

        /// <summary>
        /// auto generate a password
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GeneratePassword(object sender, RoutedEventArgs e)
        {
            password.Password = GetRandomString(10);
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
            else
            {
                StaticModel.ViewModel.selectedItem = null;
            }
        }

        private void LoadSelectedItem()
        {
            item.Title = StaticModel.ViewModel.selectedItem.Title;
            item.Urlstr = StaticModel.ViewModel.selectedItem.Urlstr;
            item.Account = StaticModel.ViewModel.selectedItem.Account;
            item.Password = StaticModel.ViewModel.selectedItem.Password;
            item.Img = StaticModel.ViewModel.selectedItem.Img;
        }

        private async void SelectPicture(object sender, RoutedEventArgs e)
        {
            var p = await ImageHelper.Picker();
            if (p != null)
            {
                item.Img = p;
            }
        }

        private async void Website_LostFocus(object sender, RoutedEventArgs e)
        {
            if (HttpAccess.CheckURL(website.Text))
            {
                item.Img = await HttpAccess.GetIco(website.Text) ?? ImageHelper.DefaultImg;
            }
        }
    }
}
