﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SQLite;

namespace NeverSkipLegDay.Models.DAL
{
    public class SQLiteDB
    {
        public SQLiteConnection GetConnection()
        {
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var dbPath = Path.Combine(appData, "SQLiteNSLD.db3");
            return new SQLiteConnection(dbPath);
        }
    }
}
