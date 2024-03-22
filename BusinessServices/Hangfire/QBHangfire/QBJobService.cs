using Hangfire;
using WebRexErpAPI.Business.Category;
using WebRexErpAPI.Business.Industry;
using WebRexErpAPI.Business.Sitemap;
using WebRexErpAPI.Services.Pagednation;
using WebRexErpAPI.Services.QuickBase;

namespace WebRexErpAPI.Services.Hangfire.QBHangfire
{
    public class QBJobService
    {
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IRecurringJobManager _recurringJobManager;
        private readonly IServiceScopeFactory _serviceScopeFactory;
      

        private readonly RequestDelegate _next;
        static bool isFisrtCall = true;

        public QBJobService(
            IBackgroundJobClient backgroundJobClient,
            IRecurringJobManager recurringJobManager,
            IServiceScopeFactory serviceScopeFactory,
            RequestDelegate next
            
            )
        {
            _backgroundJobClient = backgroundJobClient;
            _recurringJobManager = recurringJobManager;
            _serviceScopeFactory = serviceScopeFactory;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var sideMapService = scope.ServiceProvider.GetRequiredService<ISitemapService>();
                RecurringJob.AddOrUpdate(() => sideMapService.GenerateAndUploadSitemap(), Cron.Daily);

                var industryService = scope.ServiceProvider.GetRequiredService<IIndustryService>();
                RecurringJob.AddOrUpdate(() => industryService.SaveAllIndustries(), Cron.Daily);

                var categoryService = scope.ServiceProvider.GetRequiredService<ICategoryService>();
                RecurringJob.AddOrUpdate(() => categoryService.SaveAllCategories(), Cron.Daily);

                await _next(context);
            }

            

        }

        public void DelayedJob()
        {
            _backgroundJobClient.Schedule(() => 
            Console.WriteLine("Hello from a Delayed job!"), TimeSpan.FromSeconds(60)); 
            
        }

        public void ContinuationJob()
        {
            Console.WriteLine("Hello from a Continuation job!");
        }


        public void GetAllIndustryOfQB(QuickBaseService qbService)
        {

            if (isFisrtCall)
            {

             _backgroundJobClient.Enqueue(() =>
             qbService.getAllIndustries());
            }
        }

        public void GetAllCategoryQB(QuickBaseService qbService)
        {
            if (isFisrtCall)
            {
             _backgroundJobClient.Enqueue(() =>
             qbService.getAllCategories());
            }
        }

        public void GetAllTypeQB(QuickBaseService qbService)
        {
            if (isFisrtCall)
            {
                _backgroundJobClient.Enqueue(() =>
                qbService.getAllTypes());
            }
        }

        public   void GetAllItemWithImageGalleryOFQB(QuickBaseService qbService)
        {
            var pagedResult = new PagedResult()
            {
                PageIndex = 1,
                PageSize = 10,
                Skip = 0,
                Totalpages = 0,

            };

            if (isFisrtCall)
            {
                _backgroundJobClient.Enqueue(() =>
                 qbService.GetItems(pagedResult)
                 ); ;
            }


        }

        public void ReccuringJob()
        {
           
            _recurringJobManager.AddOrUpdate("jobId", () => Console.WriteLine("Hello from a Scheduled job!"), Cron.Minutely);
            
        }
    }

}
