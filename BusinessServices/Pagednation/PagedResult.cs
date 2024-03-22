using System.Security.Principal;

namespace WebRexErpAPI.Services.Pagednation
{
    

    public class PagedResult
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int Skip { get; set; }
        public int TotalResult { get; set; }
        public int Totalpages { get; set; }
       

    }

   


}


