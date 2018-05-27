using PasswordBox.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.System;
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
    public sealed partial class detail : Page
    {
        public detail()
        {
            this.InitializeComponent();
        }

        private string str;

        /// <summary>
        /// load the detail of the clickitem
        /// </summary>
        private async void LoadDetail()
        {
            if (ViewModel.StaticModel.ViewModel.selectedItem == null)
            {
                ContentDialog tip;
                tip = new ContentDialog()
                {
                    Title = "提示",
                    PrimaryButtonText = "确认",
                    Content = "未选中事项",
                    FullSizeDesired = false
                };
                tip.PrimaryButtonClick += (S, E) => {
                    Frame.Navigate(typeof(Home));
                };
                await tip.ShowAsync();
                return;
            }
            title.Text = ViewModel.StaticModel.ViewModel.selectedItem.Title;
            account.Text = ViewModel.StaticModel.ViewModel.selectedItem.Account;
            website.Text = ViewModel.StaticModel.ViewModel.selectedItem.Urlstr;
            str = "";
            for (int i = 0; i < ViewModel.StaticModel.ViewModel.selectedItem.Password.Length; i++)
            {
                str += '*';
            }
            password.Text = str;
        }

        /// <summary>
        /// visit the website by the default browser
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void WebVisit(object sender, RoutedEventArgs e)
        {
            if (website.Text == string.Empty) return;

            var uri = new Uri(@"http://" + website.Text);
            var promptOptions = new LauncherOptions
            {
                TreatAsUntrusted = false
            };
            var success = await Launcher.LaunchUriAsync(uri, promptOptions);

            if (success)
            {
                await Launcher.LaunchUriAsync(uri, promptOptions);
            }
            else
            {
                ContentDialog tip;
                tip = new ContentDialog()
                {
                    Title = "提示",
                    PrimaryButtonText = "确认",
                    Content = "访问失败，请检查网址是否正确",
                    FullSizeDesired = false
                };
                tip.PrimaryButtonClick += (S, E) => { };
                await tip.ShowAsync();
            }
        }

        /// <summary>
        /// copy the account
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CopyAccount(object sender, RoutedEventArgs e)
        {
            DataPackage dp = new DataPackage();
            dp.SetText(account.Text);
            Clipboard.SetContent(dp);
        }

        /// <summary>
        /// copy the password
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CopyPassword(object sender, RoutedEventArgs e)
        {
            DataPackage dp = new DataPackage();
            dp.SetText(ViewModel.StaticModel.ViewModel.selectedItem.Password);
            Clipboard.SetContent(dp);
        }

        private void ShowPassword(object sender, RoutedEventArgs e)
        {
            if (ShowButton.Content.ToString() == "显示密码")
            {
                password.Text = ViewModel.StaticModel.ViewModel.selectedItem.Password;
                ShowButton.Content = "隐藏密码";
            }
            else
            {
                password.Text = str;
                ShowButton.Content = "显示密码";
            }
        }

        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            ViewModel.StaticModel.ViewModel.DeletePasswordItem();
            ViewModel.StaticModel.ViewModel.selectedItem = null;
            Frame.Navigate(typeof(Home));
        }

        private void ShareItem(object sender, RoutedEventArgs e)
        {
            DataTransferManager.ShowShareUI();
        }

        async void OnShareDataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            DataRequest request = args.Request;
            DataRequestDeferral deferal = request.GetDeferral();
            request.Data.Properties.Title = ViewModel.StaticModel.ViewModel.selectedItem.Title;
            request.Data.Properties.Description = ViewModel.StaticModel.ViewModel.selectedItem.Account;
            
            string content = "Website: " + ViewModel.StaticModel.ViewModel.selectedItem.Urlstr + '\n' +
                             "Account: " + ViewModel.StaticModel.ViewModel.selectedItem.Account + '\n' +
                             "Password: " + ViewModel.StaticModel.ViewModel.selectedItem.Password;
            request.Data.SetText(content);
            deferal.Complete();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            DataTransferManager.GetForCurrentView().DataRequested += OnShareDataRequested;
            LoadDetail();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            DataTransferManager.GetForCurrentView().DataRequested -= OnShareDataRequested;
        }

        private void TurnToChangePage(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(NewOrUpdate), 1);
        }
    }
}
