using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teste.Tecnico.Mottu.Matheus.Willock.Domain.Models;

namespace Teste.Tecnico.Mottu.Matheus.Willock.Infrastructure.Repositories.MotorCycleRepository
{
    public interface IMotorCycleRepository
    {
        Task<Motorcycle?> GetByPlateAsync(string plate);
        Task AddAsync(Motorcycle motorcycle);
        Task<List<Motorcycle>> GetAll();
        Task<Motorcycle> UpdateAsync(Motorcycle motorcycle);
        Task DeleteAsync(Motorcycle motorcycle);
        Task<Motorcycle?> GetMotorcycleIsntRented();
    }
}
