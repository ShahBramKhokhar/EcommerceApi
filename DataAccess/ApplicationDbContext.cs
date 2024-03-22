using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebRexErpAPI.DataAccess.Models;
using WebRexErpAPI.Models;

namespace WebRexErpAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        #region DbSets

        public DbSet<Industry> tblIndustries { get; set; }
        public DbSet<Category> tblCategories { get; set; }
        public DbSet<WebRexErpAPI.Models.Type> tblTypes { get; set; }
        public DbSet<ItemImageGallery> tblItemImageGalleries { get; set; }
        public DbSet<Item> tblItems { get; set; }
        public DbSet<Cart> tblCarts { get; set; }
        public DbSet<Order> tblOrders { get; set; }
        public DbSet<Visitor> tblVisitor { get; set; }
        public DbSet<VisitorMessage> tblVisitorMessages { get; set; }
        public DbSet<User> tblUser { get; set; }
        public DbSet<ShoppingInformation> tblShippingInformations { get; set; }
        public DbSet<UserContact> tblUserContacts { get; set; }
        public DbSet<Billing> tblBillings { get; set; }
        public DbSet<PaymentPreference> tblPaymentPerences { get; set; }
        public DbSet<SaveLater> tblSaveLater { get; set; }
        public DbSet<UserAddress> tblAddresses { get; set; }
        public DbSet<AllowItemOffer> tblallowItemOffers { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
   
     

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserType>().ToTable("UserTypes");
          

        }
        public override int SaveChanges()
        {
            SeedData();
            return base.SaveChanges();
        }

        private void SeedData()
        {
            if (!UserTypes.Any())
            {
                var userTypesToAdd = new List<UserType>
        {
            new UserType { TypeName = "Customer" },
            new UserType { TypeName = "Admin" },
            new UserType { TypeName = "Employee" },
            new UserType { TypeName = "Supplier" }
        };

                Set<UserType>().AddRange(userTypesToAdd);

                SaveChanges();
            }
        }

    }
}
