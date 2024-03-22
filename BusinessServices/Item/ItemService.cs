using WebRexErpAPI.Business.Category.Dto;
using WebRexErpAPI.Business.Industry.Dto;
using WebRexErpAPI.Business.Type.Dto;
using WebRexErpAPI.BusinessServices.Item.Dto;
using WebRexErpAPI.Common.CommonDto;
using WebRexErpAPI.Data.UnitOfWork;
using WebRexErpAPI.DataAccess.Models;
using WebRexErpAPI.Helper;
using WebRexErpAPI.Models;
using WebRexErpAPI.Services.Item.Dto;


namespace WebRexErpAPI.Business.Industry
{
    public class ItemService : IItemService,IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;
        public ItemService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public  async Task<PagedResponse<List<ItemDto>>> PagedItemsWithGlobalSearch(PaginationFilter filter)
        {
            var result = new ItemsResult();
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            validFilter.Reset = filter.Reset;
            var queryResult = SearchWithAndFilters(filter);

            var resultsfilters = new FiltersResultDto();
            SearchFilters(queryResult, resultsfilters);

            validFilter.Sort = filter.Sort;
            var pagedData = ItemDtoPagedResult(validFilter, queryResult);
            var data = await ItemPagedResult(filter, validFilter, pagedData, queryResult.Count());

            data.filtersResultDto = resultsfilters;
            return data;
        }

        private void SearchFilters(IQueryable<Item> queryResult, FiltersResultDto resultsfilters)
        {

            resultsfilters.IndustryList = queryResult.Where(m => m.IndustryName != "" && m.IndustryName != null)
                                             .DistinctBy(a => a.IndustryName)
                                             .Select(
                                                a => 
                                                new IndustryDto
                                                {
                                                    Id= a.RelatedIndustrId,
                                                    Name = a.IndustryName,
                                                    ItemCount = queryResult.Where( q => q.IndustryName == a.IndustryName).Count(),
                                                }).OrderBy(a => a.Name).ToList();


            resultsfilters.CategoryList = queryResult.Where(m => m.CategoryName != "" && m.CategoryName != null)
                                            .DistinctBy(a => a.CategoryName)
                                             .Select(
                                                a =>
                                                new CategoryDto
                                                {
                                                    Id = a.RelatedCategoryId,
                                                    Name = a.CategoryName,
                                                    ItemCount = queryResult.Where(q => q.CategoryName == a.CategoryName).Count(),
                                                }).OrderBy(a => a.Name).ToList();


            resultsfilters.TypeList = queryResult.Where(m => m.TypeName != "" && m.TypeName != null)
                                            .DistinctBy(a => a.TypeName)
                                                   .Select(
                                                a =>
                                                new TypeDto
                                                {
                                                    Id = a.RelatedTypeId,
                                                    Name = a.TypeName,
                                                    ItemCount = queryResult.Where(q => q.TypeName == a.TypeName).Count(),
                                                }).OrderBy(a => a.Name).ToList();


            resultsfilters.BrandNameList = queryResult.Where(m => m.BrandName != "" && m.BrandName != null)
                   .DistinctBy(a => a.BrandName).Select(a => a.BrandName.Trim()).ToList();

            

            resultsfilters.ConditionList = queryResult.Where(m => m.Condition != "" && m.Condition != null)
                 .DistinctBy(a => a.Condition).Select(a => a.Condition.Trim()).ToList();


            resultsfilters.Locations = queryResult.Where(m => m.Location != "" && m.Location != null)
                .DistinctBy(a => a.Location).Select(a => a.Location.Trim()).ToList();

            resultsfilters.BrandNameList.Sort();
            resultsfilters.ConditionList.Sort();
            resultsfilters.Locations.Sort();
        }

        public async Task<ItemDto> GetItemById(int id)
        {
            try
            {
                var itmDto = new ItemDto();
                Item data = await _unitOfWork.item.GetByIdAsync(id);
                itmDto = data.MapSameProperties<ItemDto>();

                return itmDto;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public async Task<List<ItemDto>> GetRecentItemListAsync()
        {
            try
            {
                List<ItemDto> ItemDtoList = new List<ItemDto>();

                Random rand = new Random();
                var count = _unitOfWork.item.FindAll( a=>a.Quantity >0).Count();
                int toSkip = rand.Next(1, count);

                // var list = _unitOfWork.item.GetAll().Skip(toSkip).Take(12).ToList();
                var list = await _unitOfWork.item.GetAllAsync();
                list = list.Skip(toSkip).Take(12).ToList();

                foreach (var item in list)
                {
                    var ItemDto = new ItemDto();
                    ItemDto = item.MapSameProperties<ItemDto>();
                    ItemDtoList.Add(ItemDto);

                }

                return ItemDtoList;
            }
            catch (Exception )
            {

                throw;
            }

           
        }

        public async Task<List<ItemImageGalleryDto>> ItemGalleryByQBIdAsync(int QbRecordId)
        {
           var GalleryList = new List<ItemImageGalleryDto>();
            var list =  _unitOfWork.itemImageGallery.FindAll(a=>a.QbRecordId == QbRecordId).ToList();

            foreach (var item in list)
            {
                var ItemDto = new ItemImageGalleryDto();
                
                ItemDto.original = item.ImageUrl;
                ItemDto.thumbnail = item.ImageUrl;
                ItemDto.SortOrder = item.SortOrder;

                GalleryList.Add(ItemDto);
            }

            return GalleryList;
        }

        public  int TotalItemCount()
        {
            var items = _unitOfWork.item.GetAll();
            if (items != null && items.Any())
            {
                int totalQuantity = (int)items.Sum(a => a.Quantity);
                return (int)totalQuantity;
            }
            else
            {
                return 0; 
            }
        }
        public async Task<int> InsertItemAsync(ItemDto itemDto)
        {
            var itemEntity = itemDto.MapSameProperties<Item>();
            return await _unitOfWork.item.InsertItemAsync(itemEntity);
        }

        public async Task<int> UpdateItemAsync(ItemDto itemDto)
        {
            var itemEntity = itemDto.MapSameProperties<Item>();
            return await _unitOfWork.item.UpdateItemAsync(itemEntity);
        }

      
        #region private method region
        private IQueryable<Item> SearchWithAndFilters(PaginationFilter filter)
        {
            var query = _unitOfWork.item.GetAll().AsQueryable();
            query = GlobalSearch(filter, query);
            return query;

        }

        private static IQueryable<Item> GlobalSearch(PaginationFilter filter, IQueryable<Item> query)
        {
            Predicate<Item> IndustryPrd = i => i.RelatedIndustrId == filter.IndustryId;
            Predicate<Item> CategoryPrd = i => i.RelatedCategoryId == filter.CategoryId;
            Predicate<Item> TypePrd = i => i.RelatedTypeId == filter.TypeId;
            Predicate<Item> CombineIndusrtyAndCategory = c => (IndustryPrd(c) && CategoryPrd(c));
            Predicate<Item> CombineIndusrtyCategoryAndType = c => (IndustryPrd(c) && CategoryPrd(c) && TypePrd(c));
            Predicate<Item> SearchKeywordPrd = i => i.Description.ToLower().Trim()
                                                  .Contains(filter.TextSearch.ToLower().Trim())
                                                   || i.Location.ToLower().Trim().Contains(filter.TextSearch.ToLower().Trim())
                                                   || i.BrandName.ToLower().Trim().Contains(filter.TextSearch.ToLower().Trim());
            var data = query.ToList();
            if (filter.IndustryId > 0)
            {
                data = data.Where(c => IndustryPrd(c)).ToList();
            }


            if (!string.IsNullOrEmpty(filter.RelatedIndustry))
            {
                data = data.Where(c => c.IndustryName.Trim().ToLower() == filter.RelatedIndustry.Trim().ToLower()).ToList();
            }

            if (filter.CategoryId > 0 )
            {
                data = data.Where(c => CategoryPrd(c)).ToList();
            }


            if (!string.IsNullOrEmpty(filter.RelatedCategory))
            {
                data = data.Where(c => c.CategoryName?.ToLower().Trim() == filter.RelatedCategory.ToLower().Trim()).ToList();
            }

            if (filter.TypeId > 0)
            {
                data = data.Where(c => TypePrd(c)).ToList();
            }

            if (!string.IsNullOrEmpty(filter.RelatedType))
            {
                data = data.Where(c => c.TypeName?.ToLower().Trim() == filter.RelatedType.ToLower().Trim()).ToList();
            }

            if (!string.IsNullOrEmpty(filter.TextSearch))
            {
                data = data.Where(c => SearchKeywordPrd(c)).ToList();
              
            }
           

            if (filter.BrandNameList != null && filter.BrandNameList.Count() > 0)
            {
               data = data.Where(p => filter.BrandNameList.Any(s => p.BrandName.Contains(s))).ToList();
            }


            if (filter.PriceStartRange >= 0 && (filter.PriceEndRange < 1 || filter.PriceEndRange == null))
            {
                data = data.Where(p => p.SalePrice >= filter.PriceStartRange).ToList();
            }
            if (filter.PriceEndRange > 0 && (filter.PriceStartRange < 1 || filter.PriceStartRange == null))
            {
                data = data.Where(p => p.SalePrice <= filter.PriceEndRange).ToList();
            }

            if (filter.PriceEndRange >= 0 && filter.PriceEndRange > filter.PriceStartRange && filter.PriceStartRange > 0)
            {
                data = data.Where(p => p.SalePrice >= filter.PriceStartRange
                 && p.SalePrice <= filter.PriceEndRange
                ).ToList();
            }

            if (filter.ConditionList != null && filter.ConditionList.Count() > 0)
            {
                data = data.Where(p => filter.ConditionList.Any(s => p.Condition.Contains(s))).ToList();
            }

            if (filter.Locations != null && filter.Locations.Count() > 0)
            {
                data = data.Where(p => filter.Locations.Any(s => p.Location.Contains(s))).ToList();
            }
            var res = data.AsQueryable();
            return res;
        }

        private static List<ItemDto> ItemDtoPagedResult(PaginationFilter validFilter, IQueryable<Item> query)
        {
            List<ItemDto> ItemDtoList = new List<ItemDto>();

            if(validFilter.Reset == true)
            {
                validFilter.PageNumber = 1;
            }

            
            switch (validFilter.Sort)
            {
                case "price_asc":
                    query = query.OrderBy(p => p.SalePrice);
                    break;
                case "price_desc":
                    query  = query.OrderByDescending(p => p.SalePrice);
                    break;
                case "date_asc":
                    query = query.OrderBy(p => p.ModifiedDate);
                    break;
                case "date_desc":
                    query = query.OrderByDescending(p => p.ModifiedDate);
                    break;
                case "name_asc":
                    query = query.OrderBy(p => p.Description);
                    break;
                case "name_desc":
                    query = query.OrderByDescending(p => p.Description);
                    break;
                default:
                    break;
            }

            List<Item> itmList = query.Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                            .Take(validFilter.PageSize)
                            .ToList();

            foreach (var item in itmList)
            {
                var ItemDto = new ItemDto();
                ItemDto = item.MapSameProperties<ItemDto>();
                ItemDtoList.Add(ItemDto);

            }

            return ItemDtoList;
        }



        private async Task<PagedResponse<List<ItemDto>>> ItemPagedResult(PaginationFilter filter, PaginationFilter validFilter, List<ItemDto> pagedData,int totalRecords)
        {
            double toTalPages = 1;
            int totalRoundedPages = 1;
            if(totalRecords > filter.PageSize)
            {
                totalRoundedPages = (int)Math.Ceiling((double)totalRecords / filter.PageSize);
            }
            return new PagedResponse<List<ItemDto>>(pagedData, validFilter.PageNumber, validFilter.PageSize, totalRecords, totalRoundedPages);
        }
      
        public async Task<List<ItemDto>> getRandomItemByCategory(int categoryId)
        {
            List<ItemDto> ItemDtoList = new List<ItemDto>();

            Random rand = new Random();
            int toSkip = rand.Next(1, _unitOfWork.item.FindAll(a => a.RelatedCategoryId == categoryId && a.Quantity > 0).Count());
            var list =  _unitOfWork.item.FindAll(a=>a.RelatedCategoryId == categoryId && a.Quantity > 0).Skip(toSkip).Take(4).ToList();

            foreach (var item in list)
            {
                var ItemDto = new ItemDto();
                ItemDto = item.MapSameProperties<ItemDto>();
                ItemDtoList.Add(ItemDto);
            }

            return ItemDtoList;
        }


        public int GetAttempts(string ipAddress, int itemId)
        {
            var attempts = _unitOfWork.allowItemOfferRepository.FindAll(a => a.IPAddress == ipAddress && a.ItemId == itemId).FirstOrDefault();
            if (attempts == null)
            {
                return 0;
            }

            return attempts.Attempts;
        }

        public async Task AddAttemptAsync(string ipAddress, int itemId)
        {
            var attempts = _unitOfWork.allowItemOfferRepository.FindAll(a => a.IPAddress == ipAddress && a.ItemId == itemId).FirstOrDefault();

            if (attempts == null)
            {
                attempts = new AllowItemOffer { IPAddress = ipAddress, ItemId = itemId, Attempts = 1 };
                _unitOfWork.allowItemOfferRepository.Add(attempts);
            }
            else
            {
                attempts.Attempts++;
            }

            await _unitOfWork.CompleteAsync();
        }

        #endregion

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _unitOfWork?.Dispose();
            }
        }

    }
}
