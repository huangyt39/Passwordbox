using SQLite.Net.Attributes;
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
        private int id;

        /// <summary>
        /// 数据库自动赋值
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int Id
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
                    NotifyPropertyChanged();
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

        /// <summary>
        /// 数据库会用到
        /// </summary>
        public PasswordItem() { }

        public PasswordItem(string _title, byte[] _img)
        {
            // this.Img = image;
            this.Title = _title;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

    }
}
