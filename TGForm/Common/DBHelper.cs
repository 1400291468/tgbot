using SqlSugar;

namespace TGForm.Common
{
    public static class DbHelper
    {
        private static readonly ConnectionConfig Config = new()
        {
            ConnectionString = "Data Source=mydata.db",
            DbType = DbType.Sqlite,
            IsAutoCloseConnection = true,
            InitKeyType = InitKeyType.Attribute
        };

        // SqlSugarScope 适合被多个 async/await 流程共享。
        public static SqlSugarScope Db { get; } = new(Config);
    }
}