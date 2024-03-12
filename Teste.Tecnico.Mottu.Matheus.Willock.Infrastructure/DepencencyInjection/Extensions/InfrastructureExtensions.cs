using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Teste.Tecnico.Mottu.Matheus.Willock.Infrastructure.CosmicInfra;
using Teste.Tecnico.Mottu.Matheus.Willock.Infrastructure.KafkaInfra;
using Teste.Tecnico.Mottu.Matheus.Willock.Infrastructure.KafkaInfra.Contracts;
using Teste.Tecnico.Mottu.Matheus.Willock.Infrastructure.Repositories.DeliveryManRepository;
using Teste.Tecnico.Mottu.Matheus.Willock.Infrastructure.Repositories.DocumentsRepository;
using Teste.Tecnico.Mottu.Matheus.Willock.Infrastructure.Repositories.MotorCycleRepository;
using Teste.Tecnico.Mottu.Matheus.Willock.Infrastructure.Repositories.OrderRepository;
using Teste.Tecnico.Mottu.Matheus.Willock.Infrastructure.Repositories.RentalPlansRepository;
using Teste.Tecnico.Mottu.Matheus.Willock.Infrastructure.Repositories.UserAdminRepository;

namespace Teste.Tecnico.Mottu.Matheus.Willock.Infrastructure.DepencencyInjection.Extensions
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IDeliveryManRepository, DeliveryManRepository>();
            services.AddScoped<IDocumentsRepository, DocumentsRepository>();
            services.AddScoped<IMotorCycleRepository, MotorCycleRepository>();
            services.AddScoped<IRentalPlansRepository, RentalPlansRepository>();
            services.AddScoped<IUserAdminRepository, UserAdminRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();

            services.AddScoped<ICosmic, Cosmic>();

            services.AddTransient<IProducerInfra, ProducerInfra>();

            services.AddHostedService<ConsumerInfra>();

            return services;
        }
    }
}
