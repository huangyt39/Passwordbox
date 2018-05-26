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

        //public async void Test()
        //{
        //    var items = DB.GetAllItems();
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
