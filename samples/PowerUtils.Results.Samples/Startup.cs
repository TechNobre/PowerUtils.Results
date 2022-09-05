using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PowerUtils.Results.Samples.Repositories;
using PowerUtils.Results.Samples.Services;

namespace PowerUtils.Results.Samples;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddSwaggerGen();

        services.AddSingleton<IProductsRepository, ProductsRepository>();
        services.AddScoped<IProductsService, ProductsService>();
    }

    public void Configure(IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseRouting();

        app.UseEndpoints(endpoints
            => endpoints.MapControllers() // Mapping all controller
        );
    }
}
