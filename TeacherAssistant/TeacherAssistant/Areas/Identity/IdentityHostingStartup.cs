using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TeacherAssistant;
using TeacherAssistant.Data;

[assembly: HostingStartup(typeof(TeacherAssistant.Areas.Identity.IdentityHostingStartup))]
namespace TeacherAssistant.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<TeacherAssistantContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("TeacherAssistantContextConnection")));

                services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<TeacherAssistantContext>();

                services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, ApplicationUserClaimsPrincipalFactory>();
            });
        }
    }
}