using BrregAPI.Handlers.Services;
using Hangfire;
using Hangfire.PostgreSql;

namespace BrregAPI.Handlers
{
    public class HangfireHandler
    {
        public static void SetupHangfire(IServiceCollection services)
        {
             services.AddHangfireServer(new Action<BackgroundJobServerOptions>(o => {
                o.WorkerCount = 1;
            }));

            services.AddHangfire(config =>
            {
                config.UsePostgreSqlStorage(Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING"));
            });

            services.AddTransient<FetcherService, FetcherService>();
        }

        public static void SetupTasks()
        {
            RecurringJob.AddOrUpdate<FetcherService>("FetchCompaniesAll", x=> x.FetchCompanies(5000, 0, null), Cron.Never());
        }        
    }    
}