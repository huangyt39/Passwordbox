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

        /// <summary>
        /// 用于绑定页面上的显示内容
        /// </summary>
        private PasswordItem item = new PasswordItem("", null, "", "", "");

        /// <summary>
        /// save as a new password item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void SaveContent(object sender, RoutedEventArgs e)
        {
            ContentDialog tip;
            tip = new ContentDialog()
            {
                Title = "提示",
                PrimaryButtonText = "确认",
                FullSizeDesired = false
            };
            tip.Content = string.Empty;
            tip.PrimaryButtonClick += (S, E) => {};
            if (item.Title == string.Empty)
            {
                tip.Content = "名称不能为空";
            }
            else if (item.Account == string.Empty)
            {
                tip.Content = "账号不能为空";
            }
            else if (item.Password == string.Empty)
            {
                tip.Content = "密码不能为空";
            }
            else if (item.Urlstr != string.Empty && CheckWebsite(item.Urlstr) == false)
            {
                // 只有已填写网址时检查是否错误, 网址为空不提示错误
                tip.Content = "网址格式错误";
            }
            if (tip.Content.ToString() != string.Empty)
            {
                await tip.ShowAsync();
                return;
            }
            if (StaticModel.ViewModel.selectedItem == null)
            {
                // 创建item后,跳转到home页面
                StaticModel.ViewModel.AddPasswordItem(item.Title, item.Img, item.Urlstr, item.Account, item.Password);
            }
            else
            {
                // 修改item后,跳转到detail页面
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
            /// 指定生成10位随机密码
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

        /// <summary>
        /// 转入该页面时调用
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            /// 根据传递的参数可判断是新建item或是修改item
            if (e.Parameter != null)
            {
                /// 修改item时将selectitem各项数据显示到页面上
                LoadSelectedItem();
            }
        }

        /// <summary>
        /// 用于将选中的item的内容显示到页面的函数
        /// </summary>
        private void LoadSelectedItem()
        {
            item.Title = StaticModel.ViewModel.selectedItem.Title;
            item.Urlstr = StaticModel.ViewModel.selectedItem.Urlstr;
            item.Account = StaticModel.ViewModel.selectedItem.Account;
            item.Password = StaticModel.ViewModel.selectedItem.Password;
            item.Img = StaticModel.ViewModel.selectedItem.Img;
        }

        /// <summary>
        /// 选择item图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void SelectPicture(object sender, RoutedEventArgs e)
        {
            /// 调用图片选择接口
            var p = await ImageHelper.Picker();
            /// 判断是否选择了图片,不为空时再对显示内容进行修改
            /// 避免选择图片接口返回的值为空时(即未选择图片时),直接赋值给显示内容造成异常
            if (p != null)
            {
                item.Img = p;
            }
        }

        /// <summary>
        /// 点击网址框右侧按钮时调用
        /// 根据输入的网址自动添加对应网址图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void GetWebsitePicture(object sender, RoutedEventArgs e)
        {
            if (HttpAccess.CheckURL(website.Text))
            {
                item.Img = await HttpAccess.GetIco(website.Text) ?? ImageHelper.DefaultImg;
            }
        }

        /// <summary>
        /// 填写密码项之后按enter键保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Enter_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                SaveContent(null, null);
            }
        }
    }
}
