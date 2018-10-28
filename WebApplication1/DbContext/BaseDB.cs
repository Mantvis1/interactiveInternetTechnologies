using System.Data.SqlClient;

namespace WebApplication1.DbContext
{
    public class BaseDB
    {
        public const string connetionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Mantvydas\Desktop\KTU\5 semestras\Interaktyvios interneto technologijos\Project\WebApplication1\App_Data\basketballProjectDatabase.mdf;Integrated Security=True";
        public SqlConnection cnn = new SqlConnection(connetionString);
    }
}