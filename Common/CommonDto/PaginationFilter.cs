using Microsoft.AspNetCore.Authentication;

namespace WebRexErpAPI.Common.CommonDto
{
    public class PaginationFilter
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int? IndustryId { get; set; }
        public int? CategoryId { get; set; }
        public string? RelatedCategory { get; set; }
        public string? RelatedIndustry { get; set; }
        public string? RelatedType { get; set; }
        public int? TypeId { get; set; }
        public string? TextSearch { get; set; }
        public bool IsGlobelSearch { get; set; }
        public bool IsleftFilterSearch { get; set; }

        public int? PriceStartRange { get; set; }
        public int? PriceEndRange { get; set; }
        public string[]? BrandNameList { get; set; }
        public string[]? ConditionList { get; set; }
        public string[]? Locations { get; set; }
        public bool Reset { get; set; }

        public string Sort { get; set; } = "date_desc";

        public PaginationFilter()
        {
            this.PageNumber = 1;
            this.PageSize = 10;
        }
        public PaginationFilter(int pageNumber, int pageSize)
        {
            this.PageNumber = pageNumber < 1 ? 1 : pageNumber;
            //this.PageSize = pageSize > 10 ? 10 : pageSize;
            this.PageSize = pageSize <= 0 ? 10 : pageSize;
        }
    }

    
  
}
