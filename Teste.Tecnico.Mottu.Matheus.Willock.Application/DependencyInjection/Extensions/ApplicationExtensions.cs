using Microsoft.Extensions.DependencyInjection;
using Teste.Tecnico.Mottu.Matheus.Willock.Application.Usecases.DeliveryManUsecases;
using Teste.Tecnico.Mottu.Matheus.Willock.Application.Usecases.MotorcycleUseCases;
using Teste.Tecnico.Mottu.Matheus.Willock.Application.Usecases.UserAdminUsecases;

namespace Teste.Tecnico.Mottu.Matheus.Willock.Application.DependencyInjection.Extensions
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            services.AddScoped<IUserAdminUseCase, UserAdminUseCase>();
            services.AddScoped<IMotorCycleUseCases, MotorCycleUseCases>();
            services.AddScoped<IDeliveryManUseCase, DeliveryManUseCase>();

            return services;
        }
    }
}
