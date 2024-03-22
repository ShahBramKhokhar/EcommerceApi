using WebRexErpAPI.Business.Industry.Dto;
using WebRexErpAPI.Common.CommonDto;

namespace WebRexErpAPI.BusinessServices.Item.Dto
{
    public class ItemsResult
    {
        public List<ItemDto>? items { get; set; }
        public FiltersResultDto? ResultsFilters { get; set; }

    }
}
