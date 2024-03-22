using WebRexErpAPI.Business.PaymentPreference;
using WebRexErpAPI.Business.PaymentPreference.Dto;
using WebRexErpAPI.Data.UnitOfWork;
using WebRexErpAPI.Helper;

namespace WebRexErpAPI.Business.Industry
{
    public class PaymentPreferenceService : IPaymentPreferenceService,IDisposable
    {
        private readonly IUnitOfWork  _unitOfWork;
        public PaymentPreferenceService(IUnitOfWork unitOfWork)
        {
         _unitOfWork = unitOfWork;

        }

        public async Task<WebRexErpAPI.Models.PaymentPreference> SavePaymentPreferenceAsync(PaymentPreferenceDto input)
        {
            try
            {
                var PaymentPreferenceItem = input.MapSameProperties<WebRexErpAPI.Models.PaymentPreference>();
                if (PaymentPreferenceItem != null)
                {
                    await _unitOfWork.paymentPreferenceRepository.Add(PaymentPreferenceItem);
                    await _unitOfWork.CompleteAsync();
                }

                return PaymentPreferenceItem;

            }
            catch (Exception)
            {
                throw;
            }
 

        }

        public Task<List<PaymentPreferenceDto>> getUserPaymentPreference(int userId)
        {
            var res = new List<PaymentPreferenceDto>();
            var shippingInfo = _unitOfWork.paymentPreferenceRepository.FindAll(a => a.UserId == userId).ToList();

            if(shippingInfo != null)
            {
                foreach (var shipItem in shippingInfo)
                {
                 var itemDto =   shipItem.MapSameProperties<PaymentPreferenceDto>();
                    res.Add(itemDto);
                }
              
            }

            return Task.FromResult(res);

        }

        public async Task UpdatePaymentPreferenceAsync(PaymentPreferenceDto input)
        {

            var data = _unitOfWork.paymentPreferenceRepository.FindAll(x => x.Id == input.Id).FirstOrDefault();
            if (data != null && data.Id > 0)
            {
                data.NameAlias = input.NameAlias;
                await _unitOfWork.paymentPreferenceRepository.Update(data);
                await _unitOfWork.CompleteAsync();
            }
        }

        public async Task<bool> DeletPaymentPreferenceAsync(int id)
        {
            var data = _unitOfWork.paymentPreferenceRepository.FindAll(x => x.Id == id).FirstOrDefault();
            if(data != null && data.Id > 0)
            {
                await _unitOfWork.paymentPreferenceRepository.Remove(data.Id);
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

