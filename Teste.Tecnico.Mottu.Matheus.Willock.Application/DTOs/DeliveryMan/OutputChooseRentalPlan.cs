using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teste.Tecnico.Mottu.Matheus.Willock.Domain.Models;

namespace Teste.Tecnico.Mottu.Matheus.Willock.Application.DTOs.DeliveryMan
{
    public record OutputChooseRentalPlan(
        string Plate, 
        string Year, 
        string Model, 
        string Days, 
        DateTime StartDay, 
        DateTime EndDay,
        decimal PlanCost)
    {
    }
}
