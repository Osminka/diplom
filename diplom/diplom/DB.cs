using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace diplom
{
    public class DB
    { 
        private readonly SQLiteConnection conn;
        public DB (string path)
        {
            conn = new SQLiteConnection(path);
            conn.CreateTable<Users>();

        }
        public List<Users> GetItems()
        {
            return conn.Table<Users>().ToList();
        }
        public int SaveItem(Users users)
        {
            return conn.Insert(users);
        }
        public List<Users> Search()
        {
            //return conn.Get<Users>(id);
            return conn.Query<Users>("SELECT * FROM Users order by ID desc limit 1").ToList();
            
        }
        
    }
    
}
