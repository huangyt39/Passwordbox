using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using PasswordManager;
namespace PasswordManager
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
                    DestPage = typeof(home)
                },

                new NavMenuItem()
                {
                    FontFamily = new FontFamily("Segoe MDL2 Assets"),
                    Icon = "\xE109",
                    Label = "添加新密码",
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
                },

                new NavMenuItem()
                {
                    FontFamily = new FontFamily("Segoe MDL2 Assets"),
                    Icon = "\xE715",
                    Label = "分享",
                    Selected = Visibility.Collapsed,
                    DestPage = typeof(detail)
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
