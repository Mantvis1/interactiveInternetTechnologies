using MySql.Data.MySqlClient;

namespace WebApplication1.DbContext
{
    public class BaseDB
    {
        public const string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=interactiveTechnologiesDatabase";
       public MySqlConnection databaseConnection = new MySqlConnection(connectionString);
        //
    }
}