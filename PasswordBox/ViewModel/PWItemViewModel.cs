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
            for (int i = 0;i < itemList.Count;i ++)
            {
                this.allItems.Add(itemList.ElementAt(i));
            }
            this.selectedItem = null;
            //测试用例
            /*BitmapImage NewImage = new BitmapImage(new Uri("ms-appx:///Assets/cat.png", UriKind.Absolute));
            this.allItems.Add(new PasswordItem("Title1", NewImage, "Url1", "Account1", "Password1"));
            this.allItems.Add(new PasswordItem("Title2", NewImage, "Url2", "Account2", "Password2"));
            this.allItems.Add(new PasswordItem("Title3", NewImage, "Url3", "Account3", "Password3"));*/
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
                this.selectedItem = null;
            }
            DB.Update(this.selectedItem);
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
