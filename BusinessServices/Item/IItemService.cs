    using WebRexErpAPI.Business.Industry.Dto;
using WebRexErpAPI.Common.CommonDto;
using WebRexErpAPI.Services.Item.Dto;

namespace WebRexErpAPI.Business.Industry
{
    public interface IItemService
    {
     
        Task<ItemDto> GetItemById(int id);
        Task<List<ItemDto>> GetRecentItemListAsync();
        Task<List<ItemDto>> getRandomItemByCategory(int categoryId);
        Task<List<ItemImageGalleryDto>> ItemGalleryByQBIdAsync(int QbRecordId);
        Task<PagedResponse<List<ItemDto>>> PagedItemsWithGlobalSearch(PaginationFilter filter);
        int GetAttempts(string ipAddress, int itemId);
        Task AddAttemptAsync(string ipAddress, int itemId);
        int TotalItemCount();
        Task<int> InsertItemAsync(ItemDto itemDto);
        Task<int> UpdateItemAsync(ItemDto itemDto);




    }
}
