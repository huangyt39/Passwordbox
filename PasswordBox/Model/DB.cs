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

        public static void Add(PasswordItem item)
        {
            using (var conn = Conn)
            {
                conn.Insert(item);
            }
        }

        public static void Delete(PasswordItem item)
        {
            using (var conn = Conn)
            {
                conn.Execute("DELETE FROM PasswordItem WHERE Id = ? ", item.Id);
            }
        }

        public static void Update(PasswordItem item)
        {
            using (var conn = Conn)
            {
                conn.Update(item);
            }
        }

        public static List<PasswordItem> Query(string qs)
        {
            List<PasswordItem> items;
            using (var conn = Conn)
            {
                items = conn.Table<PasswordItem>().Where(v => (v.Title.Contains(qs) || v.Urlstr.Contains(qs))).ToList();
            }
            return items;
        }

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
