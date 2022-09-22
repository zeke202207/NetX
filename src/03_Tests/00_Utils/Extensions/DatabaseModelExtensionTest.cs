using NetX.Tenants;

namespace NetcoreX.UnitTests
{
    public class DatabaseModelExtensionTest
    {
        private static string _tenantId = "0001";

        [Fact]
        public void NullValueToConnStrIsEmpty()
        {
            DatabaseInfo info = null;
            var result = info.ToConnStr();
            Assert.Empty(result);
        }

        [Fact]
        public void EqualToConnStr()
        {
            DatabaseInfo info = new DatabaseInfo()
            {
                DatabaseHost = "localhost",
                DatabaseName = "zeke",
                DatabasePort = 3306,
                DatabaseType = DatabaseType.MySql,
                UserId = "root",
                Password = "root"
            };
            var result = info.ToConnStr();
            Assert.Equal(result, $"server={info.DatabaseHost};port={info.DatabasePort};database={info.DatabaseName};userid={info.UserId};pwd={info.Password};Charset=utf8; SslMode=none;Min pool size=1");
        }

        [Fact]
        public void NullValueMutilTenantToConnStrIsEmpty()
        {
            DatabaseInfo info = null;
            var result = info.ToConnStr(TenantType.Multi, _tenantId);
            Assert.Empty(result);
        }

        [Fact]
        public void EqualMutilTenantToConnStr()
        {
            DatabaseInfo info = new DatabaseInfo()
            {
                DatabaseHost = "localhost",
                DatabaseName = "zeke",
                DatabasePort = 3306,
                DatabaseType = DatabaseType.MySql,
                UserId = "root",
                Password = "root"
            };
            var result = info.ToConnStr(TenantType.Multi, _tenantId);
            Assert.Equal(result, $"server={info.DatabaseHost};port={info.DatabasePort};database={_tenantId}-{info.DatabaseName};userid={info.UserId};pwd={info.Password};Charset=utf8; SslMode=none;Min pool size=1");
        }

        [Fact]
        public void NullValeSingleToDatabaseNameIsEmpty()
        {
            DatabaseInfo info = null;
            var result = info.ToDatabaseName(TenantType.Single, _tenantId);
            Assert.Empty(result);
        }

        [Fact]
        public void NullValeMutilToDatabaseNameIsEmpty()
        {
            DatabaseInfo info = null;
            var result = info.ToDatabaseName(TenantType.Multi, _tenantId);
            Assert.Empty(result);
        }

        [Fact]
        public void EqualSingleToDatabaseNameIsEmpty()
        {
            DatabaseInfo info = new DatabaseInfo()
            {
                DatabaseHost = "localhost",
                DatabaseName = "zeke",
                DatabasePort = 3306,
                DatabaseType = DatabaseType.MySql,
                UserId = "root",
                Password = "root"
            };
            var result = info.ToDatabaseName(TenantType.Single, _tenantId);
            Assert.Equal(result, $"{info.DatabaseName}");
        }

        [Fact]
        public void EqualMutilToDatabaseNameIsEmpty()
        {
            DatabaseInfo info = new DatabaseInfo()
            {
                DatabaseHost = "localhost",
                DatabaseName = "zeke",
                DatabasePort = 3306,
                DatabaseType = DatabaseType.MySql,
                UserId = "root",
                Password = "root"
            };
            var result = info.ToDatabaseName(TenantType.Multi, _tenantId);
            Assert.Equal(result, $"{_tenantId}-{info.DatabaseName}");
        }

        public void EqualToCreateDatabaseConnStr()
        {
            DatabaseInfo info = new DatabaseInfo()
            {
                DatabaseHost = "localhost",
                DatabaseName = "zeke",
                DatabasePort = 3306,
                DatabaseType = DatabaseType.MySql,
                UserId = "root",
                Password = "root"
            };
            var result = info.ToCreateDatabaseConnStr();
            Assert.Equal(result, $"Data Source={info.DatabaseHost};port={info.DatabasePort};Persist Security Info=yes;UserId={info.UserId}; PWD={info.Password};");
        }
    }
}