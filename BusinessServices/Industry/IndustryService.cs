using WebRexErpAPI.Business.Industry.Dto;
using WebRexErpAPI.Data.UnitOfWork;
using WebRexErpAPI.Helper;

namespace WebRexErpAPI.Business.Industry
{
    public class IndustryService : IIndustryService,IDisposable
    {

        private readonly IUnitOfWork _unitOfWork;

        public IndustryService(IUnitOfWork unitOfWork)
        {
          _unitOfWork = unitOfWork;

        }


        public async Task<bool> SaveAllIndustries()
        {
            try
            {
                await DeleteAllIndustries();
                var allItems = await _unitOfWork.item.GetAllAsync();

                var industries = allItems
                    .GroupBy(a => a.IndustryName)
                    .Select(g => new WebRexErpAPI.Models.Industry
                    {
                        QbRecordId = g.Any(item => item.RelatedIndustrId != null) ? (int)g.First(item => item.RelatedIndustrId != null).RelatedIndustrId : 0,
                        Name = g.Key,
                        ItemCount = g.Count()
                    })
                    .ToList();

                foreach (var industry in industries)
                {
                    var existingIndustry = _unitOfWork.industry.FindAll(a => a.Name == industry.Name).FirstOrDefault();

                    if (existingIndustry != null)
                    {
                        existingIndustry.QbRecordId = industry.QbRecordId;
                        existingIndustry.ItemCount = industry.ItemCount;
                    }
                    else
                    {
                        await _unitOfWork.industry.Add(industry);
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

        private async Task DeleteAllIndustries()
        {
            var allIndus = await _unitOfWork.industry.GetAllAsync();

            if (allIndus != null)
            {
                await _unitOfWork.industry.RemoveRange(allIndus);
                await _unitOfWork.CompleteAsync();
            }
        }

        public async Task<List<IndustryDto>> GetListAsync()
        {
            List<IndustryDto> industryDtos = new List<IndustryDto>();
            var list = await _unitOfWork.industry.GetAllAsync();
            foreach ( var industry in list )
            {
                industryDtos.Add(industry.MapSameProperties<IndustryDto>());
            }
            return industryDtos;

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
