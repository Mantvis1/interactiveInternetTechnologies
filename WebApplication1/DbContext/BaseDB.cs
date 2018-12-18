using MySql.Data.MySqlClient;

namespace WebApplication1.DbContext
{
    public class BaseDB
    {
        //  public const string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=interactiveTechnologiesDatabase";
        //  public MySqlConnection databaseConnection = new MySqlConnection(connectionString);

        public const string connectionString = "Server=MYSQL6002.site4now.net;Database=db_a43b7f_deadeye;Uid=a43b7f_deadeye;Pwd=liutas111!";
        public MySqlConnection databaseConnection = new MySqlConnection(connectionString);

        public const string connectionString2 = "datasource=relational.fit.cvut.cz;port=3306;username=guest;password=relational;database=NBA";
        public MySqlConnection nbaDatabaseConnection = new MySqlConnection(connectionString2);
    }
}