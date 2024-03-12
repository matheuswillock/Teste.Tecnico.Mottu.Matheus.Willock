using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teste.Tecnico.Mottu.Matheus.Willock.Domain.Models;

namespace Teste.Tecnico.Mottu.Matheus.Willock.Application.DTOs.DeliveryMan
{
    public record InputDeliveryManDto(
        string Name, 
        string Email,
        string Password,
        string CnhNumber,
        CNH CnhType,
        string Cnpj, 
        DateTime Birthday, 
        IFormFile CnhPhoto
    )
    {
    }
}
