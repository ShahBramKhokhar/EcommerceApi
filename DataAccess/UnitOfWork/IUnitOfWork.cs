
using Microsoft.EntityFrameworkCore;
using WebRexErpAPI.Data.Repository;
using WebRexErpAPI.DataAccess.Models;

namespace WebRexErpAPI.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        #region interface
        IIndustryRepository industry { get; }
        ICategoryRepository category { get; }
        ITypeRepository type { get; }
  

        IItemRepository item { get; }
        IItemImageGalleryRepository itemImageGallery { get; }
        IUserRepository userRepository { get; }
        ICartRepository cartRepository { get; }
        IOrderRepository orderRepository { get; }
        IShoppingInformationRepository shoppingInformationRepository { get; }
        IBillingRepository billingRepository { get; }
        IPaymentPreferenceRepository paymentPreferenceRepository { get; }
        ISaveLaterRepository saveLaterRepository { get; }
        IUserAddressRepository userAddressRepositoy { get; }
        IAllowItemOfferRepository allowItemOfferRepository { get; }
        

        Task BeginTransactionAsync();
        #endregion

        Task CompleteAsync();
        void Dispose();
    }
}
