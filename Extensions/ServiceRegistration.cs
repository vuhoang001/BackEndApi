using BackEndApi.Data;
using BackEndApi.Services;
using BackEndApi.Services.Categories;
using BackEndApi.Services.Review;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BackEndApi.Extensions;

public static class ServiceRegistration
{
    public static void InitAppServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection") ??
                               throw new InvalidOperationException("No connection string found");

        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICategoriesService, CategoriesService>();
        services.AddScoped<IReviewService, ReviewService>();
    }
}