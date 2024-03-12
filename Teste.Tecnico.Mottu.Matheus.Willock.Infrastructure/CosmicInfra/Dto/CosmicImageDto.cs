using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teste.Tecnico.Mottu.Matheus.Willock.Infrastructure.CosmicInfra.Dto
{
    public record CosmicImageDto(string Name, IFormFile Image)
    {
    }
}
