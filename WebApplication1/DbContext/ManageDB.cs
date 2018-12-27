namespace WebApplication1.DbContext
{
    public class ManageDB : BaseDB
    {
        string query = "";

        public ManageDB()
        {

        }

        public bool UpdatePassword()
        {
            return true;
        }

        public bool UpdateUsername()
        {
            return true;
        }

        public bool UpdateEmail()
        {
            return true;
        }

        public bool DeleteAccount()
        {
            return false;
        }
    }
}