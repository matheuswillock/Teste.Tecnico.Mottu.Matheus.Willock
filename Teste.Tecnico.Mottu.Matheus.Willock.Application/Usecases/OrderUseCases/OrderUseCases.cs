using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teste.Tecnico.Mottu.Matheus.Willock.Domain.Libs;
using Teste.Tecnico.Mottu.Matheus.Willock.Domain.Models;
using Teste.Tecnico.Mottu.Matheus.Willock.Infrastructure.KafkaInfra.Contracts;
using Teste.Tecnico.Mottu.Matheus.Willock.Infrastructure.Repositories.OrderRepository;

namespace Teste.Tecnico.Mottu.Matheus.Willock.Application.Usecases.OrderUseCases
{
    public class OrderUseCases : IOrderUseCases
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProducerInfra _producer;

        public OrderUseCases(IOrderRepository orderRepository, IProducerInfra producer)
        {
            _orderRepository = orderRepository;
            _producer = producer;
        }

        public async Task<Output> AddOrderAsync(Order order)
        {
            var output = new Output();
            try
            {
                await _orderRepository.Create(order);

                var getOrder = await _orderRepository.Get(order.Id);

                if (getOrder == null)
                {
                    output.AddErrorMessage("An error occurred while creating the order");
                    return output;   
                }

                var sendMessage = await _producer.SendMessage(order);

                if(!sendMessage.IsValid)
                {
                    output.AddErrorMessage("An error occurred while sending the order");
                    return output;
                }

                await _orderRepository.Create(order);

                output.AddMessage("Order sent successfully");
                return output;
            }
            catch (Exception ex)
            {
                output.AddErrorMessage(ex.Message);
                return output;
            }            
        }

        public async Task UpdateOrdesr(Guid id)
        {
            var getOrder = await _orderRepository.Get(id);

            if (getOrder != null)
            {
                getOrder.OrderIsDelivered = true;
                await _orderRepository.Update(getOrder);
            }
        }

    }
}
