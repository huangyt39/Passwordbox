using PasswordBox.Common;
using SQLite.Net;
using SQLite.Net.Platform.WinRT;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace PasswordBox.Model
{
    class DB
    {
        private static SQLiteConnection Conn
        {
            get
            {
                string path = Path.Combine(ApplicationData.Current.LocalFolder.Path, "data.sqlite");
                var conn = new SQLiteConnection(new SQLitePlatformWinRT(), path);
                conn.CreateTable<PasswordItem>();
                return conn;
            }
        }

        /// <summary>
        /// 添加到数据库
        /// </summary>
        /// <param name="item"></param>
        public static void Add(PasswordItem item)
        {
            item.Password = Crypto.Encrypt(item.Password);
            using (var conn = Conn)
            {
                conn.Insert(item);
            }
            item.Password = Crypto.Decrypt(item.Password);
        }

        /// <summary>
        /// 删除某个item
        /// </summary>
        /// <param name="item"></param>
        public static void Delete(PasswordItem item)
        {
            using (var conn = Conn)
            {
                conn.Execute("DELETE FROM PasswordItem WHERE Id = ? ", item.Id);
            }
        }

        /// <summary>
        /// 更新某个item
        /// </summary>
        /// <param name="item"></param>
        public static void Update(PasswordItem item)
        {
            item.Password = Crypto.Encrypt(item.Password);
            using (var conn = Conn)
            {
                conn.Update(item);
            }
            item.Password = Crypto.Decrypt(item.Password);
        }

        /// <summary>
        /// 得到所有item，用于初始化ViewModel
        /// </summary>
        /// <returns></returns>
        public static List<PasswordItem> GetAllItems()
        {
            List<PasswordItem> items;
            using (var conn = Conn)
            {
                items = conn.Table<PasswordItem>().ToList();
            }
            return items;
        }
    }
}