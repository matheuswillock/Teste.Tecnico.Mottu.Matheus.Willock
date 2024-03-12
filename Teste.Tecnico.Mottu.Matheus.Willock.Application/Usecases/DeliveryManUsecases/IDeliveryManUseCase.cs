using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teste.Tecnico.Mottu.Matheus.Willock.Application.DTOs.DeliveryMan;
using Teste.Tecnico.Mottu.Matheus.Willock.Domain.Libs;

namespace Teste.Tecnico.Mottu.Matheus.Willock.Application.Usecases.DeliveryManUsecases
{
    public interface IDeliveryManUseCase
    {
        Task<Output> RegisterDeliveryMan(InputDeliveryManDto input);
        Task<Output> ChooseRentalPlan(InputChooseRentalPlan input);
        Task<Output> LoginDeliveryMan(InputLoginDeliveryMan request);
    }
}
