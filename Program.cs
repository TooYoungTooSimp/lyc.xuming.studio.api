using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace lyc.xuming.studio.api
{
    public class Program
    {
        public static void Main(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureServices((ctx, svc) =>
                {
                    var redisConnStr = ctx.Configuration["ConnStr:Redis"];
                    svc.AddMvc();
                    svc.AddSingleton<IConnectionMultiplexer>(_ => ConnectionMultiplexer.Connect(redisConnStr));
                })
                .Configure(app =>
                {
                    var env = app.ApplicationServices.GetRequiredService<IHostingEnvironment>();
                    if (env.IsDevelopment())
                        app.UseDeveloperExceptionPage();
                    app.UseMvc();
                })
                .Build().Run();
    }
}
