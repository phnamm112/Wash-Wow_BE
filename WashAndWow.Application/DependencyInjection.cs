using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using System.Reflection;
using Wash_Wow.Application.Common.Behaviours;
using Wash_Wow.Application.Common.Validation;
using WashAndWow.Application.BackgroundJobs;

namespace Wash_Wow.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), lifetime: ServiceLifetime.Transient);
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                cfg.AddOpenBehavior(typeof(UnhandledExceptionBehaviour<,>));
                cfg.AddOpenBehavior(typeof(PerformanceBehaviour<,>));
                cfg.AddOpenBehavior(typeof(AuthorizationBehaviour<,>));
                cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));
                cfg.AddOpenBehavior(typeof(UnitOfWorkBehaviour<,>));
            });
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IValidatorProvider, ValidatorProvider>();

            //Add Background Services
            services.AddQuartz(q =>
            {
                q.UseMicrosoftDependencyInjectionJobFactory();

                // Register Payment Reminder Job
                q.AddJob<PaymentReminderJob>(opts => opts.WithIdentity("PaymentReminderJob"));
                q.AddTrigger(opts => opts
                    .ForJob("PaymentReminderJob")
                    .WithIdentity("PaymentReminderTrigger")
                    .WithSimpleSchedule(x => x
                      //.WithIntervalInMinutes(1) // For testing 
                        .WithIntervalInMinutes(25)
                        .WithRepeatCount(2))); // Reminder after 25 min, twice

                // Register Payment Expiration Job
                q.AddJob<PaymentExpireJob>(opts => opts.WithIdentity("PaymentExpirationJob"));
                q.AddTrigger(opts => opts
                    .ForJob("PaymentExpirationJob")
                    .WithIdentity("PaymentExpirationTrigger")
                    .WithCronSchedule("0 */10 * * * ?")); // Every 10 minutes
                    //.WithCronSchedule("0 */5 * * * ?")); // For testing 
            });

            // Register Quartz Hosted Service
            services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

            return services;
        }
    }
}
