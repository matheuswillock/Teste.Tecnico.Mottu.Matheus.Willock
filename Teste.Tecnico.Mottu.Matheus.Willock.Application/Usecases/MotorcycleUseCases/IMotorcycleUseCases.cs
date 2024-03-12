using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teste.Tecnico.Mottu.Matheus.Willock.Application.DTOs.Motorcycle;
using Teste.Tecnico.Mottu.Matheus.Willock.Domain.Libs;
using Teste.Tecnico.Mottu.Matheus.Willock.Domain.Models;

namespace Teste.Tecnico.Mottu.Matheus.Willock.Application.Usecases.MotorcycleUseCases
{
    public interface IMotorCycleUseCases
    {
        Task<Output> RegisterMotorcycle(MotorcycleDto motorcycle, string token);
        Task<Output> UpdateMotorcycle(MotorcycleDto motorcycle, string token);
        Task<Output> DeleteMotorcycle(string plate, string token);
        Task<Output> GetMotorcycleByPlate(string plate, string token);
        Task<Output> GetAllMotorcycles(string token);
    }
}
