using MySql.Data.MySqlClient;

namespace WebApplication1.DbContext
{
    public class BaseDB
    {
        public const string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=interactiveTechnologiesDatabase";
        public MySqlConnection databaseConnection = new MySqlConnection(connectionString);


        public const string connectionString2 = "datasource=relational.fit.cvut.cz;port=3306;username=guest;password=relational;database=NBA";
        public MySqlConnection nbaDatabaseConnection = new MySqlConnection(connectionString2);
    }
}