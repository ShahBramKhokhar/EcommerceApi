using WebRexErpAPI.Business.Category.Dto;
using WebRexErpAPI.Business.Industry.Dto;
using WebRexErpAPI.Business.Type.Dto;

namespace WebRexErpAPI.Common.CommonDto
{
    public class FiltersResultDto
    {
        public List<CategoryDto>? CategoryList { get; set; }
        public List<IndustryDto>? IndustryList { get; set; }
        public List<TypeDto>? TypeList { get; set; }
        public int? PriceStartRange { get; set; }
        public int? PriceEndRange { get; set; }
        public List<string>? BrandNameList { get; set; }
        public List<string>? ConditionList { get; set; }
        public int ConditionCount { get; set; }
        public List<string>? MachineType { get; set; }
        public List<string>? Locations { get; set; }
    }
}
