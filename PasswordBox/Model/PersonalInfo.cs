using PasswordBox.Common;
using PasswordBox.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PasswordBox.Model
{
    public class PersonalInfo : INotifyPropertyChanged
    {
        // 头像
        private byte[] avator;
        // 昵称
        private string name;
        // 生日
        private string birth;
        // 邮箱
        private string email;
        // 密码
        private string password;

        /// <summary>
        /// avator的存取和修改
        /// </summary>
        public byte[] Avator
        {
            get => avator;
            set
            {
                if (value != this.avator)
                {
                    this.avator = value;
                    NotifyPropertyChanged("Avator");
                }
            }
        }
        /// <summary>
        /// name的存取和修改
        /// </summary>
        public string Name
        {
            get => name; set
            {
                if (value != this.name)
                {
                    this.name = value;
                    NotifyPropertyChanged("Name");
                }
            }
        }
        /// <summary>
        /// birth的存取和修改
        /// </summary>
        public string Birth
        {
            get => birth; set
            {
                if (value != this.birth)
                {
                    this.birth = value;
                    NotifyPropertyChanged("Birth");
                }
            }
        }
        /// <summary>
        /// email的存取和修改
        /// </summary>
        public string Email
        {
            get => email; set
            {
                if (value != this.email)
                {
                    this.email = value;
                    NotifyPropertyChanged("Email");
                }
            }
        }
        /// <summary>
        /// password的存取和修改
        /// </summary>
        public string Password
        {
            get => password; set
            {
                if (value != this.password)
                {
                    this.password = value;
                    NotifyPropertyChanged("Password");
                }
            }
        }
        /// <summary>
        /// PersonalInfo的构造函数
        /// </summary>
        public PersonalInfo()
        {
            name = UserInfo.CheckIfExist("UserName") ? UserInfo.GetInfo("UserName") : "";
            birth = UserInfo.CheckIfExist("Birth") ? UserInfo.GetInfo("Birth") : new DateTimeOffset(new DateTime(2000, 1, 1)).ToString();
            email = UserInfo.CheckIfExist("Email") ? UserInfo.GetInfo("Email") : "";
            password = UserInfo.CheckIfExist("LoginPassword") ? UserInfo.GetInfo("LoginPassword") : "";
            SetAvator();
        }

        /// <summary>
        /// 设置用户头像
        /// </summary>
        public async void SetAvator()
        {
            Avator = await UserInfo.GetImage("Avator.jpg") ?? ImageHelper.DefaultImg;
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
