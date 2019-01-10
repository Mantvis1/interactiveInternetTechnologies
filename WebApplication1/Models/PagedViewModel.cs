using System.Collections.Generic;

namespace WebApplication1.Models
{
    public class PagedViewModel
    {
        public IEnumerable<PlayerViewModel> Players { get; set; }
        public PagingModel Page { get; set; }
    }
}