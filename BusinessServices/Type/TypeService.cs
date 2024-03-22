using WebRexErpAPI.Business.Type.Dto;
using WebRexErpAPI.Data.UnitOfWork;
using WebRexErpAPI.Helper;

namespace WebRexErpAPI.Business.Type
{

    public interface ITypeService
    {
        Task<List<TypeDto>> GetListAsync(int categoryId );
    }
    public class TypeService : ITypeService,IDisposable
    {

        private readonly IUnitOfWork  _unitOfWork;

        public TypeService(IUnitOfWork unitOfWork)
        {
         _unitOfWork = unitOfWork;

        }

      

        public async Task<List<TypeDto>> GetListAsync(int categoryId = 0)
        {
            var list = new List<WebRexErpAPI.Models.Type>();

            if(categoryId > 0)
            {
                list =  _unitOfWork.type.FindAll(a => a.CategoryId == categoryId).ToList();
            }
            else
            {
                list = await _unitOfWork.type.GetAllAsync();
            }
          
            return MapTypListToTypeDto(list);
        }

        private static List<TypeDto> MapTypListToTypeDto(IEnumerable<WebRexErpAPI.Models.Type> list)
        {
            var dtoList = new List<TypeDto>();
            foreach (var item in list)
            {
                var TypesDto = item.MapSameProperties<TypeDto>();
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
