using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teste.Tecnico.Mottu.Matheus.Willock.Domain.Models;

namespace Teste.Tecnico.Mottu.Matheus.Willock.Infrastructure.Repositories.DocumentsRepository
{
    public interface IDocumentsRepository
    {
        Task<DeliveryManDocument?> Create(DeliveryManDocument document);
        Task<DeliveryManDocument?> GetByCnhNumber(string cnhNumber);
        Task<DeliveryManDocument> Update(DeliveryManDocument document);
        Task Delete(int id);
        Task<DeliveryManDocument?> GetById(Guid id);
    }
}
