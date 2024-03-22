using WebRexErpAPI.Business.Category.Dto;
using WebRexErpAPI.Business.Industry.Dto;

namespace WebRexErpAPI.Business.Category
{
    public interface ICategoryService
    {
      
        Task<List<CategoryDto>> GetListAsync();

        List<CategoryDto> GetListAsyncByIndustryId(string name);

        Task<bool> SaveAllCategories();

    }
}
