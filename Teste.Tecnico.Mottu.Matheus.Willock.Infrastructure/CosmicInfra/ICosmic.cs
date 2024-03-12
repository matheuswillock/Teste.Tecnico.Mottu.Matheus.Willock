using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teste.Tecnico.Mottu.Matheus.Willock.Infrastructure.CosmicInfra.Dto;

namespace Teste.Tecnico.Mottu.Matheus.Willock.Infrastructure.CosmicInfra
{
    public interface ICosmic
    {
        Task<CosmicMediaDto?> SendImageAsync(CosmicImageDto cosmicImage);
    }
}
