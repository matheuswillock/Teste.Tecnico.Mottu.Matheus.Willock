using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teste.Tecnico.Mottu.Matheus.Willock.Domain.Models;

namespace Teste.Tecnico.Mottu.Matheus.Willock.Infrastructure.Repositories.DeliveryManRepository
{
    public interface IDeliveryManRepository
    {
        Task<DeliveryMan?> Create(DeliveryMan deliveryMan);
        Task<DeliveryMan?> GetByEmail(string email);
        Task<List<DeliveryMan>> GetAll();
        Task<DeliveryMan> Update(DeliveryMan deliveryMan);
        Task<DeliveryMan?> GetByCnpj(string cnpj);
        Task Delete(int id);
        Task<DeliveryMan?> GetLoginAsync(string email, string hashedPassword);
    }
}
