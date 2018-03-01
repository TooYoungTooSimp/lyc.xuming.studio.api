using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace lyc.xuming.studio.api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHostingEnvironment Environment = null;
            WebHost.CreateDefaultBuilder(args)
                .ConfigureLogging((context, logging) =>
                {
                    Environment = context.HostingEnvironment;
                    if (Environment.IsProduction())
                        logging.SetMinimumLevel(LogLevel.Warning);
                })
                .ConfigureServices(services => services.AddMvc())
                .Configure(app =>
                {
                    if (Environment.IsDevelopment())
                        app.UseDeveloperExceptionPage();
                    app.UseMvc();
                })
                .Build().Run();
        }
    }
}
