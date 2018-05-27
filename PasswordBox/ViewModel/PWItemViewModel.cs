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

        public void AddPasswordItem(string title, Byte[] img, string urlstr, string account, string password)
        {
            PasswordItem newItem = new PasswordItem(title, img, urlstr, account, password);
            this.allItems.Add(newItem);
            DB.Add(newItem);
        }

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
