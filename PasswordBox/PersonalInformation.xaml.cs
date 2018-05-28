using PasswordBox.Common;
using PasswordBox.Model;
using PasswordBox.Services;
using PasswordBox.ViewModel;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace PasswordBox
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class PersonalMessage : Page
    {
        public PersonalMessage()
        {
            this.InitializeComponent();
        }

        PersonalInfo Info = new PersonalInfo();

        /// <summary>
        /// 用户设置个人信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void SetInformation(object sender, RoutedEventArgs e)
        {
            ContentDialog dialog;
            dialog = new ContentDialog()
            {
                Title = "提示",
                PrimaryButtonText = "确认",
                SecondaryButtonText = "取消",
                Content = "确定要修改信息?",
                FullSizeDesired = false
            };
            dialog.PrimaryButtonClick += (_s, _e) => {
                UserInfo.SetInfo("UserName", Info.Name);
                UserInfo.SetInfo("Question", Info.Question);
                UserInfo.SetInfo("Answer", Info.Answer);
                UserInfo.SaveImage(Info.Avator, "Avator.jpg");
            };
            dialog.SecondaryButtonClick += (_s, _e) => {};
            await dialog.ShowAsync();
        }

        private async void SelectPicture(object sender, RoutedEventArgs e)
        {
            var p = await ImageHelper.Picker();
            if (p != null)
            {
                Info.Avator = p;
            }
        }
    }
}
