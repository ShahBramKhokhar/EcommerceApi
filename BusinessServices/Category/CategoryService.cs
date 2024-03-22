using WebRexErpAPI.Business.Category.Dto;
using WebRexErpAPI.Data.UnitOfWork;
using WebRexErpAPI.Helper;

namespace WebRexErpAPI.Business.Category
{
    public class CategoryService : ICategoryService ,IDisposable
    {

        private readonly IUnitOfWork  _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
         _unitOfWork = unitOfWork;

        }

        //public async Task<List<CategoryDto>> GetListAsyncByIndustryId(string name)
        //{
        //    // var list = _unitOfWork.item.GetMany(a => a.IndustryName == name).DistinctBy(d=>d.CategoryName).ToList();
        //    var list = await _unitOfWork.item.FindAllAsync(a => a.IndustryName == name);
        //    list = list.DistinctBy(d => d.CategoryName).ToList();

        //    var categoryDtoList = new List<CategoryDto>();
        //    foreach (var item in list)
        //    {
        //        var cate = new CategoryDto();
        //        if(item.RelatedCategoryId != null) { 
        //           cate.Id = (int)item.RelatedCategoryId;
        //        }
        //        cate.Name = item.CategoryName;
        //        categoryDtoList.Add(cate);
        //    }

        //    return categoryDtoList;


        //}

        //public List<CategoryDto> GetListAsyncByIndustryId(string name)
        //{
        //    var distinctCategories = ( _unitOfWork.item.GetMany(a => a.IndustryName == name))
        //        .Select(item => new
        //        {
        //            RelatedCategoryId = item.RelatedCategoryId,
        //            CategoryName = item.CategoryName,
        //        })
        //        .DistinctBy(d => d.CategoryName)
        //        .ToList();

        //    var categoryDtoList = distinctCategories
        //        .Select(item => new CategoryDto
        //        {
        //            Id = item.RelatedCategoryId ?? 0,
        //            Name = item.CategoryName ?? ""
        //        })
        //        .ToList();

        //    return categoryDtoList;
        //}

        public List<CategoryDto> GetListAsyncByIndustryId(string name)
        {
            var distinctCategories = (_unitOfWork.item.GetMany(a => a.IndustryName == name))
                .GroupBy(item => new
                {
                    RelatedCategoryId = item.RelatedCategoryId,
                    CategoryName = item.CategoryName,
                })
                .Select(group => new
                {
                    RelatedCategoryId = group.Key.RelatedCategoryId,
                    CategoryName = group.Key.CategoryName,
                    ItemCount = group.Count()
                })
                .ToList();

            var categoryDtoList = distinctCategories
                .Select(item => new CategoryDto
                {
                    Id = item.RelatedCategoryId ?? 0,
                    Name = item.CategoryName ?? "",
                    ItemCount = item.ItemCount
                })
                .ToList();

            return categoryDtoList;
        }

        public async Task<List<CategoryDto>> GetListAsync()
        {
            
            var list = await _unitOfWork.category.GetAllAsync();
            return MapTypListToTypeDto(list);
            
        }

        public async Task<bool> SaveAllCategories()
        {
            try
            {
                await DeleteAllCategories();
                var allItems = await _unitOfWork.item.GetAllAsync();

                var categories = allItems
                    .GroupBy(a => a.CategoryName)
                    .Select(g => new WebRexErpAPI.Models.Category
                    {
                        QbRecordId = g.Any(item => item.RelatedCategoryId != null) ? (int)g.First(item => item.RelatedCategoryId != null).RelatedCategoryId : 0,
                        Name = g.Key,
                        ItemCount = g.Count()
                    })
                    .ToList();

                foreach (var category in categories)
                {
                    var existingCategory = _unitOfWork.category.FindAll(a => a.Name == category.Name).FirstOrDefault();

                    if (existingCategory != null)
                    {
                        existingCategory.QbRecordId = category.QbRecordId;
                        existingCategory.ItemCount = category.ItemCount;
                    }
                    else
                    {
                        await _unitOfWork.category.Add(category);
                    }
                }

                await _unitOfWork.CompleteAsync();

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task DeleteAllCategories()
        {
            var allCategoriees = await _unitOfWork.category.GetAllAsync();

            if (allCategoriees != null)
            {
                await _unitOfWork.category.RemoveRange(allCategoriees);
                await _unitOfWork.CompleteAsync();
            }
        }
        private static List<CategoryDto> MapTypListToTypeDto(IEnumerable<WebRexErpAPI.Models.Category> list)
        {
            var dtoList = new List<CategoryDto>();
            foreach (var item in list)
            {
                var TypesDto = item.MapSameProperties<CategoryDto>();
                dtoList.Add(TypesDto);

            }

            return dtoList;
        }

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
