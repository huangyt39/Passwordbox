using PasswordBox.Common;
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
        private byte[] img;
        private int id;
        private string urlstr;
        private string account;
        private string password;

        /// <summary>
        /// password的存取和修改
        /// </summary>
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
                    this.password = value;
                    NotifyPropertyChanged("Password");
                }
            }
        }

        /// <summary>
        /// account的存取和修改
        /// </summary>
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
                    NotifyPropertyChanged("Account");
                }
            }
        }

        /// <summary>
        /// url的存取和修改
        /// </summary>
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
                    NotifyPropertyChanged("Urlstr");
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
                    NotifyPropertyChanged("Id");
                }
            }
        }

        /// <summary>
        /// title的存取和修改
        /// </summary>
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
                    NotifyPropertyChanged("Title");
                }
            }
        }

        /// <summary>
        /// img的存取和修改
        /// </summary>
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
                    NotifyPropertyChanged("Img");
                }
            }
        }

        /// <summary>
        /// 数据库会用到
        /// </summary>
        public PasswordItem() { }

        /// <summary>
        /// PasswordItem的构造函数
        /// </summary>
        /// <param name="_title"></param>
        /// <param name="_img"></param>
        /// <param name="_urlstr"></param>
        /// <param name="_account"></param>
        /// <param name="_password"></param>
        public PasswordItem(int _id, string _title, byte[] _img, string _urlstr, string _account, string _password)
        {
            this.Id = _id;
            this.Img = _img ?? ImageHelper.DefaultImg;
            this.Title = _title;
            this.urlstr = _urlstr;
            this.account = _account;
            this.password = _password;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 用于发出属性值修改的通知
        /// </summary>
        /// <param name="propertyName"></param>
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

    }
}
