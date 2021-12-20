using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(WebSite.Areas.Identity.IdentityHostingStartup))]
namespace WebSite.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}