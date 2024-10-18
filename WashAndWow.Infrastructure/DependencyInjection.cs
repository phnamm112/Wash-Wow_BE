using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wash_Wow.Domain.Common.Interfaces;
using Wash_Wow.Domain.Repositories;
using Wash_Wow.Infrastructure.Persistence;
using Wash_Wow.Infrastructure.Repositories;
using WashAndWow.Domain.Repositories;
using WashAndWow.Infrastructure.Repositories;

namespace Wash_Wow.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.UseSqlServer(
                configuration.GetConnectionString("local"),
                b =>
                {
                    b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                    b.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                });
            options.UseLazyLoadingProxies();
        });
        // scope/htmlclient ở đây
        services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<ApplicationDbContext>());
        // repo inject ở đây
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IFormRepository, FormRepository>();
        services.AddTransient<IFormImageRepository, FormImageRepository>();
        services.AddTransient<IFormTemplateRepository, FormTemplateRepository>();
        services.AddTransient<IFormTemplateContentRepository, FormTemplateContentRepository>();
        services.AddTransient<IFormFieldValueRepository, FormFieldValueRepository>();
        services.AddTransient<ILaundryShopRepository, LaundryShopRepository>();
        services.AddScoped<IEmailVerifyRepository, EmailVerifyRepository>();
        services.AddTransient<IShopServiceRepository, ShopServiceRepository>();
        services.AddTransient<IBookingItemRepository, BookingItemRepository>();
        services.AddTransient<IBookingRepository, BookingRepository>();
        services.AddTransient<IVoucherRepository, VoucherRepository>();
        services.AddTransient<IRatingRepository, RatingRepository>();
        services.AddTransient<IPaymentRepository, PaymentRepository>();
        services.AddTransient<INotificationRepository, NotificationRepository>();
        
        return services;
    }
}
