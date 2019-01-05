namespace WebApplication1.Models
{
    public class PagingModel
    {
        public int firstPage;
        public int lastPage;
        public int currentPage;
        public int nextPage;
        public int beforePage;

        public PagingModel(int last, int current)
        {
            this.firstPage = 1;
            this.lastPage = last;
            this.currentPage = current;
            this.nextPage = currentPage + 1;
            this.beforePage = currentPage - 1;
        }

        public PagingModel()
        {
        }
    }
}