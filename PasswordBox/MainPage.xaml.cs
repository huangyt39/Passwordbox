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
using PasswordBox;
using Windows.UI.Notifications;
using Windows.Data.Xml.Dom;
using Windows.UI.Xaml.Media.Imaging;
using Windows.ApplicationModel.DataTransfer;
using PasswordBox.Model;
using Windows.Storage.Streams;
using Windows.Storage;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace PasswordBox
{

    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public static MainPage Current;
        public MainPage()
        {
            this.InitializeComponent();
            Current = this;
            NavMenuPrimaryListView.ItemsSource = MenuItems.navMenuPrimaryItem;
            NavMenuSecondaryListView.ItemsSource = MenuItems.navMenuSecondaryItem;
            // SplitView 开关
            PaneOpenButton.Click += (sender, args) =>
            {
                RootSplitView.IsPaneOpen = !RootSplitView.IsPaneOpen;
            };
            // 导航事件
            NavMenuPrimaryListView.ItemClick += NavMenuListView_ItemClick;
            NavMenuSecondaryListView.ItemClick += NavMenuListView_ItemClick;
            // 默认页
            if (Services.UserInfo.CheckIfExist("LoginPassword"))
            {
                RootFrame.Navigate(typeof(Login));
            }
            else
            {
                RootFrame.Navigate(typeof(ChangePassword));
            }
            //隐藏汉堡菜单,要显示则把length改为48并把button设为visible
            /*RootSplitView.CompactPaneLength = 0;
            PaneOpenButton.Visibility = Visibility.Collapsed;
            BottomButtons.Visibility = Visibility.Collapsed;*/
            HideMenu();
            // 动态磁贴
            AdaptiveTile();
        }

        public void HideMenu()
        {
            RootSplitView.CompactPaneLength = 0;
            PaneOpenButton.Visibility = Visibility.Collapsed;
            BottomButtons.Visibility = Visibility.Collapsed;
        }

        public void ShowMenu()
        {
            if (Window.Current.Bounds.Width <= 500)
            {
                BottomButtons.Visibility = Visibility.Visible;
            }
            else
            {
                RootSplitView.CompactPaneLength = 48;
                PaneOpenButton.Visibility = Visibility.Visible;
            }
        }

        private void NavMenuListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            // 遍历，将选中Rectangle隐藏
            foreach (var np in MenuItems.navMenuPrimaryItem)
            {
                np.Selected = Visibility.Collapsed;
            }
            foreach (var ns in MenuItems.navMenuSecondaryItem)
            {
                ns.Selected = Visibility.Collapsed;
            }

            NavMenuItem item = e.ClickedItem as NavMenuItem;
            // Rectangle显示并导航
            item.Selected = Visibility.Visible;
            if (item.DestPage != null)
            {
                RootFrame.Navigate(item.DestPage);
            }

            RootSplitView.IsPaneOpen = false;
        }

        /// <summary>
        /// 动态磁贴
        /// </summary>
        private async void AdaptiveTile()
        {
            TileUpdateManager.CreateTileUpdaterForApplication().Clear();
            XmlDocument document = new XmlDocument();
            document.LoadXml(File.ReadAllText("./Common/AdaptiveTile.xml"));
            XmlNodeList textElements = document.GetElementsByTagName("text");
            if (Services.UserInfo.CheckIfExist("UserName"))
            {
                textElements[0].InnerText = Services.UserInfo.GetInfo("UserName");
                textElements[1].InnerText = Services.UserInfo.GetInfo("UserName");
            }

            XmlNodeList imgElements = document.GetElementsByTagName("image");
            var imgElement = imgElements[0] as Windows.Data.Xml.Dom.XmlElement;
            for (int i = 0; i < imgElements.Count; i++)
            {
                imgElement = imgElements[i] as Windows.Data.Xml.Dom.XmlElement;
                if (Services.UserInfo.CheckIfExist("Head"))
                {
                    byte[] pic = await Services.UserInfo.GetImage("Head");
                    BitmapImage bitmap = await Common.ImageHelper.AsBitmapImage(pic);
                    imgElement.SetAttribute("Source", bitmap.UriSource.ToString());
                }
            }

            var tileNotification = new TileNotification(document);
            TileUpdateManager.CreateTileUpdaterForApplication().Update(tileNotification);
            TileUpdateManager.CreateTileUpdaterForApplication().EnableNotificationQueue(true);
        }

        private void NavigateToHome(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Home));
        }

        private void NavigateToAdd(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(NewOrUpdate));
        }

        private void NavigateToPersonalInfo(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(PersonalMessage));
        }

        private void NavigateToChangePassword(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ChangePassword));
        }
    }
}
