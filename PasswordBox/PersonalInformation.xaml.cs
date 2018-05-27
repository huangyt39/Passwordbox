using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class PersonalMessage : Page
    {
        public PersonalMessage()
        {
            this.InitializeComponent();
        }
        /// <summary>
        /// save the picture for the head portrait
        /// </summary>
        private byte[] picSelect;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            LoadUserInformation();
        }

        /// <summary>
        /// Load user's information
        /// </summary>
        private async void LoadUserInformation()
        {
            username.Text = Services.UserInfo.CheckIfExist("UserName") == true ? Services.UserInfo.GetInfo("UserName") : "";
            question.Text = Services.UserInfo.CheckIfExist("Question") == true ? Services.UserInfo.GetInfo("Question") : "";
            answer.Text = Services.UserInfo.CheckIfExist("Answer") == true ? Services.UserInfo.GetInfo("Answer") : "";
            if (Services.UserInfo.GetImage("Head") != null)
            {
                picSelect = await Services.UserInfo.GetImage("Head");
            }
            else
            {
                UserHead.Source = new BitmapImage { UriSource = new Uri("ms-appx:///Assets/cat.png") };
            }
        }

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
                Services.UserInfo.SaveImage(picSelect, "Head");
                Services.UserInfo.SetInfo("UserName", username.Text);
                Services.UserInfo.SetInfo("Question", question.Text);
                Services.UserInfo.SetInfo("Answer", answer.Text);
            };
            dialog.SecondaryButtonClick += async (_s, _e) =>
            {
                username.Text = Services.UserInfo.GetInfo("UserName");
                question.Text = Services.UserInfo.GetInfo("Question");
                answer.Text = Services.UserInfo.GetInfo("Answer");
                picSelect = await Services.UserInfo.GetImage("Head");
            };
            await dialog.ShowAsync();
        }

        private async void SelectPicture(object sender, RoutedEventArgs e)
        {
            var p = await Common.ImageHelper.Picker();
            if (p != null)
            {
                picSelect = p; //无效,待修改
            }
        }
    }
}
