using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PasswordManager;
using Windows.UI.Xaml.Media.Imaging;
using PasswordManager.Model;

namespace PasswordManager.ViewModel
{
    public class PWItemViewModel
    {
        private ObservableCollection<PasswordItem> allItems = new ObservableCollection<PasswordItem>();
        public PasswordItem selectedItem;
        public ObservableCollection<PasswordItem> AllItems { get { return this.allItems; } }
        public PWItemViewModel()
        {
            BitmapImage NewImage = new BitmapImage(new Uri("ms-appx:///Assets/cat.png", UriKind.Absolute));
            this.allItems.Add(new PasswordItem("Just have a try", NewImage));
            this.allItems.Add(new PasswordItem("Just have a try", NewImage));
            this.allItems.Add(new PasswordItem("Just have a try", NewImage));
            this.allItems.Add(new PasswordItem("Just have a try", NewImage));
            this.allItems.Add(new PasswordItem("Just have a try", NewImage));
            this.allItems.Add(new PasswordItem("Just have a try", NewImage));

        }
    }
}
