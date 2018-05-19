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
                return new SQLiteConnection(new SQLitePlatformWinRT(), path);
            }
        }

        public static void Add(PasswordItem item)
        {
            using (var conn = Conn)
            {
                var table = conn.CreateTable<PasswordItem>();
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

        public static List<PasswordItem> GetAllItems()
        {
            List<PasswordItem> items;
            using (var conn = Conn)
            {
                items = (from p in conn.Table<PasswordItem>()
                         select p).ToList();
            }
            return items;
        }
    }
}
