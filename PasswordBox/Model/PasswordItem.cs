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
        private Byte[] img;
        private int id;
        private string urlstr;
        private string account;
        private string password;

        public string Password
        {
            get
            {
                return this.password;
            }
            set
            {
                if (value != this.password)
                {
                    this.password = value; ;
                    NotifyPropertyChanged("password");
                }
            }
        }

        public string Account
        {
            get
            {
                return this.account;
            }
            set
            {
                if (value != this.account)
                {
                    this.account = value; ;
                    NotifyPropertyChanged("account");
                }
            }
        }

        public string Urlstr
        {
            get
            {
                return this.urlstr;
            }
            set
            {
                if(value != this.urlstr)
                {
                    this.urlstr = value;
                    NotifyPropertyChanged("urlstr");
                }
            }
        }


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
        public Byte[] Img
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

        public PasswordItem(string _id, string _title, Byte[] _img, string _urlstr, string _account, string _password)
        {
<<<<<<< HEAD
=======
            this.id = _id;
>>>>>>> 5fbdc3f5e8aaf7da89a158b45bd1fef15a6a375e
            this.Img = _img;
            this.Title = _title;
            this.urlstr = _urlstr;
            this.account = _account;
            this.password = _password;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

    }
}
