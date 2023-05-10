using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace diplom
{
    public class Users
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Email { get; set; }
        public bool trueORfalse { get; set; }
    }
}
