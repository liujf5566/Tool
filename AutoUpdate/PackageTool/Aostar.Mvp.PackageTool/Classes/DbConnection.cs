using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aostar.MVP.Update.Config.Classes;

namespace Aostar.MVP.Update.Config.Classes
{
    public class DbConnection
    {
        public EDbType DbType { get; set; }
        public string ServerAddress { get; set; }
        public string DbName { get; set; }
        public string Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public override bool Equals(object obj)
        {
            if(obj == null || (obj as DbConnection)==null)
            {
                return false;
            }
            DbConnection dbConn = obj as DbConnection;
            return DbType == dbConn.DbType &&
                ServerAddress.Equal(dbConn.ServerAddress) &&
                DbName.Equal(dbConn.DbName) &&
                Port.Equal(dbConn.Port) &&
                UserName.Equal(dbConn.UserName) &&
                Password.Equal(dbConn.Password);
        }
        public string ConnectionString
        {
            get
            {
                return DbType == EDbType.MySql ?
                    string.Format("Server={0};Port={1};Database={2};Uid={3};Pwd={4}", ServerAddress, Port, DbName, UserName, Password) :
                    string.Format(@"Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST={0})(PORT={1}))
(CONNECT_DATA=(SERVICE_NAME={2})));User Id={3};Password={4}", ServerAddress, Port, DbName, UserName, Password);
                    ;
            }
        }
    }
}
