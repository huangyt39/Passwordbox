using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace PasswordBox.Model
{
    public class PasswordItem : INotifyPropertyChanged
    {
        private string title;
        private byte[] img;

        public string Title
        {
            get
            {
                return this.title;
            }

            set
            {
                if (value != this.title)
                {
                    this.title = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public byte[] Img
        {
            get
            {
                return this.img;
            }

            set
            {
                if (value != this.img)
                {
                    this.img = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }

        }
        public PasswordItem(string title_, BitmapImage image)
        {
            // this.Img = image;
            this.Title = title_;
        }
    }
}
