using WebRexErpAPI.BusinessServices.ShipEngine.Dto;

namespace WebRexErpAPI.BusinessServices.ShipEngine
{
    public interface IShipEngineService
    {
        Task<string> GetRates(SERequest request);
        Task<List<SECarrier>> ListCarriersAsync();
        //Task<List<SEAddressVerificationResponse>> ValidateAddress(SERequestAddress input);
        Task<SECarrierInfo> ODFL();
        Task<SECarrierInfo> SAIA();
        Task<SECarrierInfo> XPOID();
        Task<string> GetRatesTLT(SELTLRequest input);

        Task<string> TLTCarrierSupported(string carrierId);

        Task<string> ValidateAddressAsync(SEAddressDTO address);
    }
}
