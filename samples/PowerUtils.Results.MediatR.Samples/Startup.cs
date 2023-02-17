using System;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PowerUtils.Results.MediatR.Samples.Repositories;
using PowerUtils.Results.MediatR.Samples.Validations;

namespace PowerUtils.Results.MediatR.Samples;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddSwaggerGen();

        services.AddMediatR(o => o.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipeline<,>));

        services.AddSingleton<IProductsRepository, ProductsRepository>();
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
