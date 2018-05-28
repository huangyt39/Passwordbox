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
        // 安全问题
        private string question;
        // 答案
        private string answer;
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
        /// question的存取和修改
        /// </summary>
        public string Question
        {
            get => question; set
            {
                if (value != this.question)
                {
                    this.question = value;
                    NotifyPropertyChanged("Question");
                }
            }
        }
        /// <summary>
        /// answer的存取和修改
        /// </summary>
        public string Answer
        {
            get => answer; set
            {
                if (value != this.answer)
                {
                    this.answer = value;
                    NotifyPropertyChanged("Answer");
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
            question = UserInfo.CheckIfExist("Question") ? UserInfo.GetInfo("Question") : "";
            answer = UserInfo.CheckIfExist("Answer") ? UserInfo.GetInfo("Answer") : "";
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
