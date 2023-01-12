using EasyCaching.Core;
using EasyCaching.InMemory;
using EasyCaching.SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HypermindLib
{
    public static class HypermindCache
    {
        public static bool GlobalEnabled = true;
        public static DefaultSQLiteCachingProvider Cache;

        static HypermindCache()
        {
            Init();
        }

        public static TimeSpan CacheTime = new TimeSpan(10000, 0, 0);

        public static void Init()
        {
            var sqlOptions = new SQLiteOptions();
            var sqlDBoptions= new SQLiteDBOptions();
            sqlDBoptions.FileName = @"my.db";
            
            sqlOptions.DBConfig = sqlDBoptions;


            EasyCaching.SQLite.ISQLiteDatabaseProvider provider = new EasyCaching.SQLite.SQLiteDatabaseProvider("sqlite", sqlOptions);

            Cache = new DefaultSQLiteCachingProvider("sqlite", new ISQLiteDatabaseProvider[] { provider }, sqlOptions);
    
            
            
        }

    }
}
