using WebRexErpAPI.Business.Industry.Dto;
using WebRexErpAPI.Services.VisitorMessage.Dto;

namespace WebRexErpAPI.Services.VisitorMessage
{
    public interface IVisitorMessageService
    {
        Task<bool> CreateAsync(VisitorMessageDto input);
    }
}
