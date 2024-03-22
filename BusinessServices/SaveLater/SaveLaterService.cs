using WebRexErpAPI.Business.Industry.Dto;
using WebRexErpAPI.Business.SaveLater;
using WebRexErpAPI.Business.SaveLater.Dto;
using WebRexErpAPI.Data.UnitOfWork;
using WebRexErpAPI.Helper;
using WebRexErpAPI.Models;

namespace WebRexErpAPI.Business.Industry
{
    public class SaveLaterService : ISaveLaterService,IDisposable
    {
        private readonly IUnitOfWork  _unitOfWork;
        public SaveLaterService(IUnitOfWork unitOfWork)
        {
         _unitOfWork = unitOfWork;

        }

        public async Task SaveSaveLaterAsync(SaveLaterDto input)
        {
            try
            {
                var SaveLaterItem = new WebRexErpAPI.DataAccess.Models.SaveLater();

                var item = _unitOfWork.item.FindAll(a=>a.Id == input.ItemId).FirstOrDefault();
                SaveLaterItem.UserId = input.UserId;
                SaveLaterItem.ItemId = input.ItemId;
             


                if (SaveLaterItem != null)
                {
                    var isExit =  _unitOfWork.saveLaterRepository.FindAll(a => a.ItemId == SaveLaterItem.ItemId).FirstOrDefault();
                    if(isExit == null)
                    {
                        await _unitOfWork.saveLaterRepository.Add(SaveLaterItem);
                        await _unitOfWork.CompleteAsync();
                    }
                  
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<List<ItemDto>> getUserSaveLater(int userId)
        {
            var res = new List<ItemDto>();
            var shippingInfo = _unitOfWork.saveLaterRepository.FindAll(a => a.UserId == userId).ToList();

            if(shippingInfo != null)
            {
                foreach (var shipItem in shippingInfo)
                {
                    var prod =  _unitOfWork.item.FindAll( m => m.Id == shipItem.ItemId).FirstOrDefault();
                    if(prod != null)
                    {
                        var itemDto = prod.MapSameProperties<ItemDto>();
                        res.Add(itemDto);
                    }
                   
                } 
            }

            return Task.FromResult(res);

        }

        public async Task<bool> DeletSaveLaterAsync(int id)
        {
            var data = _unitOfWork.saveLaterRepository.FindAll(x => x.ItemId == id).FirstOrDefault();
            if(data != null && data.Id > 0)
            {
                await _unitOfWork.saveLaterRepository.Remove(data.Id);
                await _unitOfWork.CompleteAsync();
                return true;
            }
            else
            {
                return false;
            }
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

