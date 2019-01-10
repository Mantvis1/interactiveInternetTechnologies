namespace WebApplication1.Models
{
    public class PagingModel
    {
        public int FirstPage { get; set; }
        public int LastPage { get; set; }
        public int CurrentPage { get; set; }
        public int NextPage { get; set; }
        public int BeforePage { get; set; }

        public PagingModel(int last, int current)
        {
            FirstPage = 1;
            LastPage = last;
            CurrentPage = current;
            NextPage = CurrentPage + 1;
            BeforePage = CurrentPage - 1;
        }

        public PagingModel()
        {
        }
    }
}