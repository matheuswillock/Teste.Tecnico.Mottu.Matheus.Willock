using Microsoft.Extensions.DependencyInjection;
using Teste.Tecnico.Mottu.Matheus.Willock.Domain.Libs;

namespace Teste.Tecnico.Mottu.Matheus.Willock.Domain.DependencyInjection.Extensions
{
    public static class DomainExtensions
    {
        public static IServiceCollection AddServicesAndLibs(this IServiceCollection services)
        {
            services.AddScoped<IOutput, Output>();

            return services;
        }
    }
}
