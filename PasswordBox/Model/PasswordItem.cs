using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace PasswordBox.Model
{
    public class PasswordItem : INotifyPropertyChanged
    {

        private string title;
        private ImageSource img;
        private string id;

        /// <summary>
        /// 数据库自动赋值
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public string Id
        {
            get
            {
                return this.id;
            }

            set
            {
                if (value != this.id)
                {
                    this.id = value;
                    NotifyPropertyChanged("id");
                }
            }
        }

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
                    NotifyPropertyChanged("title");
                }
            }
        }
        public ImageSource Img
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
                    NotifyPropertyChanged("img");
                }
            }
        }

        /// <summary>
        /// 数据库会用到
        /// </summary>
        public PasswordItem() { }

        public PasswordItem(string _title, ImageSource _img)
        {
            this.id = Guid.NewGuid().ToString();
            this.Img = _img;
            this.Title = _title;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

    }
}
