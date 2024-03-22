using WebRexErpAPI.BusinessServices.Item.Dto;

namespace WebRexErpAPI.Common.CommonDto
{
    public class PagedResponse<T> : Response<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        //public Uri FirstPage { get; set; }
        //public Uri LastPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        //public Uri NextPage { get; set; }
        //public Uri PreviousPage { get; set; }
        public FiltersResultDto filtersResultDto { get; set; }
        public PagedResponse(T data, int pageNumber, int pageSize,int totalRecords,int totalPages)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
            this.Data = data;
            this.TotalRecords = totalRecords;
            this.TotalPages = totalPages;
            this.Message = null;
            this.Succeeded = true;
            this.Errors = null;
        }
    }
}
