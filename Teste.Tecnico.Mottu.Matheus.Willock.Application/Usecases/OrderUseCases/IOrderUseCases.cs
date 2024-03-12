using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teste.Tecnico.Mottu.Matheus.Willock.Domain.Libs;
using Teste.Tecnico.Mottu.Matheus.Willock.Domain.Models;

namespace Teste.Tecnico.Mottu.Matheus.Willock.Application.Usecases.OrderUseCases
{
    public interface IOrderUseCases
    {
        Task<Output> AddOrderAsync(Order order);
        Task UpdateOrdesr(Guid id);
    }
}
