using System.Collections.Generic;

namespace WebApplication1.Models
{
    public class PagedViewModel
    {
        public IEnumerable<PlayerViewModel> players { get; set; }
        public PagingModel Page { get; set; }
    }
}