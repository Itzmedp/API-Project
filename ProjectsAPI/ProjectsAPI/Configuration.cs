using Microsoft.AspNetCore.Identity;
using ProjectAPI.Data.Models;

namespace ProjectsAPI
{
    public static class Configuration
    {
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentityCore<IdentityUser>(x => x.User.RequireUniqueEmail = true);
            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), services);
            builder.AddEntityFrameworkStores<UserDbContext>().AddDefaultTokenProviders();
            builder.Services.Configure<DataProtectionTokenProviderOptions>(opt =>
            opt.TokenLifespan = TimeSpan.FromHours(2));
        }
    }
}
