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
using PasswordBox.ViewModel;
using PasswordBox.Model;
using PasswordBox.Common;
using PasswordBox.Services;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Notifications;
using Windows.Data.Xml.Dom;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace PasswordBox
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>

    public sealed partial class Home : Page
    {
        public PWItemViewModel ViewModel = StaticModel.ViewModel;

        public Home()
        {
            this.InitializeComponent();
            //Test();
        }

        /// <summary>
        /// 点击已保存的密码item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemClick(object sender, ItemClickEventArgs e)
        {
            ViewModel.selectedItem = (PasswordItem)e.ClickedItem;
            Frame.Navigate(typeof(detail));
        }

        private void SearchItem(object sender, RoutedEventArgs e)
        {
            CleanItem();
            var statement = DB.Query("SELECT Id FROM PasswordItem WHERE Title LIKE ? OR urlstr LIKE ?");
            if (statement.Count == 0) return;
            foreach (PasswordItem i in statement)
            {
                ViewModel.AllItems.Add(i);
            }
        }

        private void CleanItem()
        {
            for (int i = ViewModel.AllItems.Count - 1; i >= 0; i--)
            {
                ViewModel.AllItems.Remove(ViewModel.AllItems[i]);
            }
            ViewModel.selectedItem = null;

        }

        private void SearchItem(object sender, TextChangedEventArgs e)
        {
            CleanItem();
            var statement = DB.Query(searchBox.Text);
            if (statement.Count == 0) return;
            foreach (PasswordItem i in statement)
            {
                ViewModel.AllItems.Add(i);
            }
        }

        private void IsEmpty(object sender, KeyRoutedEventArgs e)
        {
            if (searchBox.Text == string.Empty)
            {
                CleanItem();
                var list = DB.GetAllItems();
                foreach (PasswordItem i in list)
                {
                    ViewModel.AllItems.Add(i);
                }
            }
        }

        //public async void Test()
        //{
        //    //var items = DB.GetAllItems();
        //    //items[0].Title = "1";
        //    //DB.Delete(items[0]);
        //    //if (Crypto.Decrypt(Crypto.Encrypt("123")) != "123" || !Crypto.TestEqual(Crypto.Hash("123"), "123"))
        //    //{
        //    //}
        //    //byte[] res = await HttpAccess.GetIco("https://www.bilibili.com/");
        //    //if (res != null)
        //    //{
        //    //    await ImageHelper.AsStorageFile(res, "test.jpg");
        //    //    BitmapImage image = await ImageHelper.AsBitmapImage(res);
        //    //}

        //}
    }
}
