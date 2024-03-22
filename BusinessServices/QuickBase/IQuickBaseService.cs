using WebRexErpAPI.Services.Account.Dto;
using WebRexErpAPI.Services.Common;
using WebRexErpAPI.Services.Pagednation;

namespace WebRexErpAPI.Services.QuickBase
{
    public interface IQuickBaseService
    {
        Task getAllIndustries();
        Task getAllTypes();
        Task getAllCategories();
        Task getAllItemIamageGallery(int ItemId);
        Task GetItems(PagedResult pagedResult);
        Task<bool> GetQBToItem(int qbId);
        Task<CustomerQBDto> FindCustomerQBBusiness(string businessName);
        Task<List<UserContactDto>> FindCustomerContacts(int customerNumber);
        Task<List<UserContactDto>> FindCustomerSaleQB(int recordId);
        Task<CustomerQBDto> CreateQbCustomer(CustomerQBDto input);
        Task<ContactQBDto> CreateQBContact(ContactQBDto input);
        Task<LocationQBDto> CreateQBLocationCustomer(LocationQBDto input);
        Task<QBOrderDto> CreateQBOrder(QBOrderDto input);
        Task<SO_ItemQBDto> CreateQBSOItem(SO_ItemQBDto input);
        Task<QBChargeDto> CreateQBCharge(QBChargeDto input);
        Task<QBChargeDto> CreateQBPayment(QBChargeDto input);
        Task<string> GetCustomerOrders(int CustomerQBID);
        Task<ContactQBDto> FindCustomerContacts(ContactQBDto input);
        Task<LocationQBDto> FindQBCustomerLocation(LocationQBDto input);
        Task<bool> GetQBItemCheckAppraiseChatGPT(int qbId);
        Task UpdateQBItemChatGPTInput(string message, int QBId);

    }
}
