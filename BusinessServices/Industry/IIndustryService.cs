using WebRexErpAPI.Business.Industry.Dto;
using System.Threading.Tasks;

namespace WebRexErpAPI.Business.Industry
{
    public interface IIndustryService
    {
        Task<List<IndustryDto>> GetListAsync();
        Task<bool> SaveAllIndustries();
    }
}
