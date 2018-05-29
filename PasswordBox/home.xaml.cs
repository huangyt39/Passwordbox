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
using System.Collections.ObjectModel;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace PasswordBox
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>

    public sealed partial class Home : Page
    {
        public PWItemViewModel ViewModel => StaticModel.ViewModel;

        /// <summary>
        /// temp of allitems
        /// </summary>
        private ObservableCollection<PasswordItem> temp = new ObservableCollection<PasswordItem>(StaticModel.ViewModel.AllItems);

        public Home()
        {
            this.InitializeComponent();
            LiveTile.LoadTile();
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

        /// <summary>
        /// 搜索名称、账号或网址含有搜索字段的已存item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchItem(object sender, KeyRoutedEventArgs e)
        {
            // the text of the search is empty
            if (searchBox.Text == string.Empty)
            {
                // clear viewmodel
                ViewModel.AllItems.Clear();
                // show all items
                foreach (PasswordItem i in temp)
                {
                    ViewModel.AllItems.Add(i);
                }
                return;
            }
            if (e.Key == Windows.System.VirtualKey.Enter) {
                ViewModel.AllItems.Clear();
                string search_str = searchBox.Text;
                // search for the match items
                foreach (PasswordItem i in temp)
                {
                    if (i.Title.Contains(search_str) || i.Urlstr.Contains(search_str) || i.Account.Contains(search_str))
                    {
                        ViewModel.AllItems.Add(i);
                    }
                }
            }
        }
    }
}
