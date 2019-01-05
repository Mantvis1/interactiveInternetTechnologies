namespace WebApplication1.Models
{
    public class PlayerModel : BaseModel
    {
        public string Name { get; set; }

        public PlayerModel(int id, string name) : base(id)
        {
            Name = name;
        }
    }
}