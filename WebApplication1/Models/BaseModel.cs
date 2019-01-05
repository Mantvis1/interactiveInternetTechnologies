namespace WebApplication1.Models
{
    public class BaseModel
    {
        public int ID { get; set; }

        public BaseModel(int id)
        {
            ID = id;
        }

        public BaseModel()
        {

        }
    }
}