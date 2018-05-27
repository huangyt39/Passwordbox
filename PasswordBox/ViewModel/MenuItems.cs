using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using PasswordBox;
using PasswordBox.Model;

namespace PasswordBox.ViewModel
{
    public class MenuItems
    {
        public static List<NavMenuItem> navMenuPrimaryItem = new List<NavMenuItem>(
            new[]
            {
                new NavMenuItem()
                {
                    FontFamily = new FontFamily("Segoe MDL2 Assets"),
                    Icon = "\xE10F",
                    Label = "主页",
                    Selected = Visibility.Visible,
                    DestPage = typeof(Home)
                },

                new NavMenuItem()
                {
                    FontFamily = new FontFamily("Segoe MDL2 Assets"),
                    Icon = "\xE109",
                    Label = "添加",
                    Selected = Visibility.Collapsed,
                    DestPage = typeof(NewOrUpdate)
                },

                new NavMenuItem()
                {
                    FontFamily = new FontFamily("Segoe MDL2 Assets"),
                    Icon = "\xE13D",
                    Label = "个人信息",
                    Selected = Visibility.Collapsed,
                    DestPage = typeof(PersonalMessage)
                }

            });

        public static List<NavMenuItem> navMenuSecondaryItem = new List<NavMenuItem>(
            new[]
            {
                new NavMenuItem()
                {
                    FontFamily = new FontFamily("Segoe MDL2 Assets"),
                    Icon = "\xE192",
                    Label = "修改密码",
                    Selected = Visibility.Collapsed,
                    DestPage = typeof(ChangePassword)
                }
            });
    }
}
