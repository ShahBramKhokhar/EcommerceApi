using WebRexErpAPI.Data.Repository;
using WebRexErpAPI.DataAccess.Models;

namespace WebRexErpAPI.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _context;
        //private readonly ILogger _logger;

        #region interface import
        public IIndustryRepository industry { get; private set; }
        public ICategoryRepository category { get; private set; }
        public ITypeRepository type { get; private set; }
        public IItemRepository item { get; private set; }
        public IItemImageGalleryRepository itemImageGallery { get; private set; }
        public IUserRepository userRepository { get; private set; }
        public ICartRepository cartRepository { get; private set; }
        public IOrderRepository orderRepository { get; private set; }
        public IShoppingInformationRepository shoppingInformationRepository { get; private set; }
        public IBillingRepository billingRepository { get; private set; }
        public IPaymentPreferenceRepository paymentPreferenceRepository { get; private set; }
        public ISaveLaterRepository saveLaterRepository { get; private set; }
        public IUserAddressRepository userAddressRepositoy  {get; private set; }
        public IAllowItemOfferRepository allowItemOfferRepository { get; private set; }
      
 




        #endregion

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            industry = new IndustryRepository(context);
            category = new CategoryRepository(context);
            type = new TypeRepository(context);
            item = new ItemRepository(context);
            itemImageGallery = new ItemImageGalleryRepository(context);
            userRepository = new UserRepository(context);
            cartRepository = new CartRepository(context);
            orderRepository = new OrderRepository(context);
            shoppingInformationRepository = new ShoppingInformationRepository(context);
            billingRepository = new BillingRepository(context);
            paymentPreferenceRepository= new PaymentPreferenceRepository(context);
            saveLaterRepository= new SaveLaterRepository(context);
            userAddressRepositoy = new UserAddressRepository(context);
            allowItemOfferRepository= new AllowItemOfferRepository(context);
           
           
        }


        
        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
        #region IDisposable Implementation

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                disposed = true;
            }
        }
        public async Task BeginTransactionAsync()
        {
               await _context.Database.BeginTransactionAsync();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }

        #endregion
    }
}
