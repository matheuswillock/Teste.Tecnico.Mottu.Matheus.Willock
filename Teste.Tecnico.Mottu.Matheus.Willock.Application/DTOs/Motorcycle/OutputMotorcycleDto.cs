using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teste.Tecnico.Mottu.Matheus.Willock.Application.DTOs.Motorcycle
{
    public record OutputMotorcycleDto(string Plate, string Year, string Model, bool IsRented)
    {
    }
}
