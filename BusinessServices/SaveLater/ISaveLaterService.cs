using WebRexErpAPI.Business.Industry.Dto;
using WebRexErpAPI.Business.SaveLater.Dto;

namespace WebRexErpAPI.Business.SaveLater
{
    public interface ISaveLaterService
    {
        Task SaveSaveLaterAsync(SaveLaterDto input);
        Task<bool> DeletSaveLaterAsync(int id);
        Task<List<ItemDto>> getUserSaveLater(int userId);

    }
}
