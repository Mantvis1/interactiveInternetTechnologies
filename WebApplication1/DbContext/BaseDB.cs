using MySql.Data.MySqlClient;

namespace WebApplication1.DbContext
{
    public class BaseDB
    {
        /* https://ourcodeworld.com/articles/read/218/how-to-connect-to-mysql-with-c-sharp-winforms-and-xampp */
        public const string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=interactiveTechnologiesDatabase";
       public MySqlConnection databaseConnection = new MySqlConnection(connectionString);
    }
}