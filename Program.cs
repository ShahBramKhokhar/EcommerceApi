using Hangfire;
using WebRexErpAPI.Authentication;
using WebRexErpAPI.Business.Cart;
using WebRexErpAPI.Business.Category;
using WebRexErpAPI.Business.CheckOut;
using WebRexErpAPI.Business.Industry;
using WebRexErpAPI.Business.Order;
using WebRexErpAPI.Business.ShippingInformation;
using WebRexErpAPI.Business.Type;
using WebRexErpAPI.BusinessServices.AzureStorage.Dto;
using WebRexErpAPI.BusinessServices.AzureStorage;
using WebRexErpAPI.Data;
using WebRexErpAPI.Data.UnitOfWork;
using WebRexErpAPI.Services.Account.User;
using WebRexErpAPI.Services.Hangfire.QBHangfire;
using WebRexErpAPI.Services.VisitorMessage;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebRexErpAPI.Services.QuickBase;
using WebRexErpAPI.Business.Billing;
using WebRexErpAPI.Business.PaymentPreference;
using WebRexErpAPI.Business.SaveLater;
using WebRexErpAPI.BusinessServices.ShipEngine;
using WebRexErpAPI.Business.Address;
using WebRexErpAPI.Business.UserAddress;
using WebRexErpAPI.Business.Email;
using WebRexErpAPI.Business.Sitemap;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using WebRexErpAPI.Business.ChatGPT;
using WebRexErpAPI.Controllers;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString)); // UseSqlServer instead of UseNpgsql

        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        builder.Services.AddControllers();

        // Hangfire
        builder.Services.AddHangfire(x => x.UseSqlServerStorage(connectionString)); // UseSqlServerStorage instead of UsePostgreSqlStorage
        builder.Services.AddHangfireServer();
        // add swagger
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddHttpClient();
        // Add LifeLine of Service

        builder.Services.AddScoped<ICategoryService, CategoryService>();
        builder.Services.AddScoped<IItemService, ItemService>();
        builder.Services.AddScoped<IVisitorMessageService, VisitorMessageService>();
        builder.Services.AddScoped<IIndustryService, IndustryService>();
        builder.Services.AddScoped<ITypeService, TypeService>();
        builder.Services.AddScoped<JwtAuthenticationManager>();
        builder.Services.AddScoped<IUserService, UserServices>();
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped<ICartService, CartService>();
        builder.Services.AddScoped<IShippingInformationService, ShippingInformationService>();
        builder.Services.AddScoped<IOrderService, OrderService>();
        builder.Services.AddScoped<ICheckOutService, CheckOutService>();
        builder.Services.AddScoped<ISitemapService, SitemapService>();
        builder.Services.AddScoped<IEmailService, EmailService>();
        builder.Services.AddScoped<IChatGPTService, ChatGPTService>();

        builder.Services.AddTransient<IAzureStorage, AzureStorage>();
        builder.Services.AddTransient<IQuickBaseService, QuickBaseService>();
        builder.Services.AddTransient<IBillingService, BillingService>();
        builder.Services.AddTransient<IPaymentPreferenceService, PaymentPreferenceService>();
        builder.Services.AddTransient<ISaveLaterService, SaveLaterService>();
        builder.Services.AddTransient<IShipEngineService, ShipEngineService>();
        builder.Services.AddTransient<IUserAddressService, UserAddressService>();
        builder.Services.AddScoped<UserServices>();

        
        builder.Services.AddScoped<Admin>();




        builder.Services.AddHttpContextAccessor();
        builder.Services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Token"])),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });


        builder.WebHost.ConfigureKestrel((context, options) =>
        {
            options.ListenAnyIP(7064, listenOptions =>
            {
                listenOptions.Protocols = HttpProtocols.Http2;
                listenOptions.UseHttps();
            });
        });



        builder.Services.AddResponseCompression(options =>
        {
            options.EnableForHttps = true;
        });


        builder.Services.AddCors(options => options.AddPolicy(name: "NgOrigins",
            policy =>
            {
                policy.WithOrigins(
                    "http://localhost:3000",
                    "http://www.localhost:3000",
                    "http://localhost",
                    "https://kingsurplusapp.azurewebsites.net",
                    "https://kingsurplus.com",
                    "http://www.kingsurplus.com",
                    "https://www.kingsurplus.com"

                    )
                .AllowAnyMethod().AllowAnyHeader();
            }));


        var app = builder.Build();

        app.UseStaticFiles();
        if (app.Environment.IsDevelopment())
        {
        }
        app.UseResponseCompression();
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseMiddleware<QBJobService>();
        app.UseHangfireDashboard();
        app.UseHttpsRedirection();
        app.UseCors("NgOrigins");
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}