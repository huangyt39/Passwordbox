using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PasswordBox;
using Windows.UI.Xaml.Media.Imaging;
using PasswordBox.Model;
using Windows.UI.Xaml.Media;
using PasswordBox.Common;

namespace PasswordBox.ViewModel
{
    public class PWItemViewModel
    {
        private ObservableCollection<PasswordItem> allItems = new ObservableCollection<PasswordItem>(); 
        public ObservableCollection<PasswordItem> AllItems { get { return this.allItems; } }

        public PasswordItem selectedItem;

        /// <summary>
        /// PWItemViewModel的构造函数
        /// 从数据库中加载数据
        /// </summary>
        public PWItemViewModel()
        {
            List<PasswordItem> itemList = DB.GetAllItems();
            foreach (var item in itemList)
            {
                item.Password = Crypto.Decrypt(item.Password);
                allItems.Add(item);
            }
            this.selectedItem = null;
        }

        /// <summary>
        /// 添加新的PasswordItem
        /// </summary>
        /// <param name="title"></param>
        /// <param name="img"></param>
        /// <param name="urlstr"></param>
        /// <param name="account"></param>
        /// <param name="password"></param>
        public void AddPasswordItem(int id, string title, Byte[] img, string urlstr, string account, string password)
        {
            PasswordItem newItem = new PasswordItem(id, title, img, urlstr, account, password);
            this.allItems.Add(newItem);
            DB.Add(newItem);
        }

        /// <summary>
        /// 更新PasswordItem
        /// </summary>
        /// <param name="title"></param>
        /// <param name="img"></param>
        /// <param name="urlstr"></param>
        /// <param name="account"></param>
        /// <param name="password"></param>
        public void UpdatePasswordItem(string title, Byte[] img, string urlstr, string account, string password)
        {
            if (this.selectedItem != null)
            {
                this.selectedItem.Title = title;
                this.selectedItem.Img = img;
                this.selectedItem.Urlstr = urlstr;
                this.selectedItem.Account = account;
                this.selectedItem.Password = password;
                DB.Update(this.selectedItem);
                this.selectedItem = null;
            }
        }

        /// <summary>
        /// 删除PasswordItem
        /// </summary>
        public void DeletePasswordItem()
        {
            if(this.selectedItem != null)
            {
                DB.Delete(this.selectedItem);
                this.allItems.Remove(this.selectedItem);
            }
        }
    }
}
