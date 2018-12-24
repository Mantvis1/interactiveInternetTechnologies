using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class PagingModel
    {
        public int firstPage;
        public int lastPage;
        public int currentPage;
        public int nextPage;
        public int beforePage;

        public PagingModel(int first, int last, int current)
        {
            this.firstPage = first;
            this.lastPage = last;
            this.currentPage = current;
            this.nextPage = current++;
            this.beforePage = current--;
        }

        public PagingModel()
        {
        }
    }
}