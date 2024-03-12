using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teste.Tecnico.Mottu.Matheus.Willock.Domain.Models;

namespace Teste.Tecnico.Mottu.Matheus.Willock.Infrastructure.Repositories.OrderRepository
{
    public interface IOrderRepository
    {
        Task<Order> Create(Order order);
        Task<Order?> Get(Guid id);
        Task Update(Order order);
        Task Delete(Guid id);
    }
}
