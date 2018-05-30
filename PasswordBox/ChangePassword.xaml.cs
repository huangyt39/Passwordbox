using PasswordBox.Common;
using PasswordBox.Model;
using PasswordBox.ViewModel;
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
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace PasswordBox
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class ChangePassword : Page
    {
        public ChangePassword()
        {
            this.InitializeComponent();
            SetOrChange();
        }

        PersonalInfo Info = new PersonalInfo();

        public void SetOrChange()
        {
            // Change Password
            // have logined 
            if (Info.Password != "" && App.loginFlag)
            {
                RememberPW.Visibility = Visibility.Visible;
                ForgetPW.Visibility = Visibility.Collapsed;
            }
            // Set Password
            // forget password
            else if (Services.UserInfo.CheckIfExist("LoginPassword") && !App.loginFlag)
            {
                RememberPW.Visibility = Visibility.Collapsed;
                ForgetPW.Visibility = Visibility.Visible;
                question.IsEnabled = false;
                question.Text = Services.UserInfo.GetInfo("Question");
            }
            // Set Password
            // first use
            else if (!Services.UserInfo.CheckIfExist("LoginPassword"))
            {
                RememberPW.Visibility = Visibility.Collapsed;
                ForgetPW.Visibility = Visibility.Visible;
                backButton.Visibility = Visibility.Collapsed;
                question.IsEnabled = true;
            }
        }

        // check the question and change the password
        private async void SetPassword(object sender, RoutedEventArgs e)
        {
            ContentDialog dialog;
            dialog = new ContentDialog()
            {
                Title = "提示",
                PrimaryButtonText = "确认",
                FullSizeDesired = false,
            };
            if (!Services.UserInfo.CheckIfExist("Question") && question.Text == string.Empty)
            {
                dialog.Content = "安全问题不能为空";
            }
            else if (answer.Text == string.Empty)
            {
                dialog.Content = "答案不能为空";
            }
            else if (Services.UserInfo.CheckIfExist("Question") && Services.UserInfo.GetInfo("Answer") != answer.Text)
            {
                dialog.Content = "答案错误";
            }
            else if (new_password.Password == string.Empty)
            {
                dialog.Content = "新密码不能为空";
            }
            else if (confirm_password.Password == string.Empty)
            {
                dialog.Content = "请再次输入新密码确认";
            }
            else if (new_password.Password != confirm_password.Password)
            {
                dialog.Content = "两次密码不一致";
            }
            else
            {
                Services.UserInfo.SetInfo("Question", question.Text);
                Services.UserInfo.SetInfo("Answer", answer.Text);
                Services.UserInfo.SetInfo("LoginPassword", new_password.Password);
                dialog.Content = "修改成功";
                dialog.PrimaryButtonClick += (_s, _e) =>
                {
                    Frame.Navigate(typeof(Login));
                };
                await dialog.ShowAsync();
                return;
            }
            dialog.PrimaryButtonClick += (_s, _e) => {};
            await dialog.ShowAsync();
        }

        // check the old password and set the new password
        private async void SwitchPassword(object sender, RoutedEventArgs e)
        {
            ContentDialog dialog;
            dialog = new ContentDialog()
            {
                Title = "提示",
                PrimaryButtonText = "确认",
                FullSizeDesired = false,
            };
            if (oldPassword.Password == string.Empty)
            {
                dialog.Content = "原密码不能为空";
            }
            else if (newPassword.Password == string.Empty)
            {
                dialog.Content = "新密码不能为空";
            }
            else if (confirmPassword.Password == string.Empty)
            {
                dialog.Content = "请再次输入新密码确认";
            }
            // check the old password
            else if (!Crypto.TestEqual(Info.Password, oldPassword.Password))
            {
                dialog.Content = "原密码错误";
            }
            // check the confirm password
            else if (newPassword.Password != confirmPassword.Password)
            {
                dialog.Content = "两次密码不一致";
            }
            else
            {
                Services.UserInfo.SetInfo("LoginPassword", newPassword.Password);
                dialog.Content = "修改成功";
            }
            dialog.PrimaryButtonClick += (_s, _e) => { };
            await dialog.ShowAsync();
        }

        /// <summary>
        /// 填写最后一项之后按下enter键登录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Enter_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                // 忘记密码时
                if (App.loginFlag == false)
                {
                    SetPassword(null, null);
                }
                // 主动修改时
                else if (App.loginFlag == true)
                {
                    SwitchPassword(null, null);
                }
            }
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Login));
        }
    }
}
