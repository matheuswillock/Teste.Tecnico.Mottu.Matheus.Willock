using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teste.Tecnico.Mottu.Matheus.Willock.Domain.Models;

namespace Teste.Tecnico.Mottu.Matheus.Willock.Infrastructure.Repositories.RentalPlansRepository
{
    public interface IRentalPlansRepository
    {
        Task<RentalPlan?> Add(RentalPlan rentalPlan);
        Task<RentalPlan?> Get(int id);
        Task<RentalPlan?> Update(RentalPlan rentalPlan);
        Task Delete(int id);
    }
}
