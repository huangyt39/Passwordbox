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
            this.selectedItem = null;
            //测试用例
            BitmapImage NewImage = new BitmapImage(new Uri("ms-appx:///Assets/cat.png", UriKind.Absolute));
            this.allItems.Add(new PasswordItem("Item1", NewImage));
            this.allItems.Add(new PasswordItem("Item2", NewImage));
            this.allItems.Add(new PasswordItem("Item3", NewImage));

        }

        public void AddPasswordItem(string title, ImageSource img)
        {
            this.allItems.Add(new PasswordItem(title, img));
        }

        public void UpdatePasswordItem(string title, ImageSource img)
        {
            if (this.selectedItem != null)
            {
                this.selectedItem.Title = title;
                this.selectedItem.Img = img;
                this.selectedItem = null;
            }
        }

        public void DeletePasswordItem()
        {
            if(this.selectedItem != null)
            {
                this.allItems.Remove(this.selectedItem);
            }
        }
    }
}
