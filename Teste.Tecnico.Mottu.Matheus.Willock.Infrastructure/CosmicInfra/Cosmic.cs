using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Teste.Tecnico.Mottu.Matheus.Willock.Infrastructure.CosmicInfra.Dto;

namespace Teste.Tecnico.Mottu.Matheus.Willock.Infrastructure.CosmicInfra
{
    public class Cosmic : ICosmic
    {
        private readonly ILogger<Cosmic> _logger;

        public Cosmic(ILogger<Cosmic> logger)
        {
            _logger = logger;
        }

        public async Task<CosmicMediaDto?> SendImageAsync(CosmicImageDto cosmicImage)
        {
            try
            {
                Stream image = cosmicImage.Image.OpenReadStream();

                var client = new HttpClient();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "dZtW3cOGKsA8Nt0VXd7PDANuWxnIbcPH9KJNWUquWsdHMlxVBI");

                var request = new HttpRequestMessage(HttpMethod.Post, "file");
                var content = new MultipartFormDataContent
                {
                    { new StreamContent(image), "media", cosmicImage.Name }
                };

                request.Content = content;
                var resultRequest = await client.PostAsync("https://workers.cosmicjs.com/v3/buckets/teste-tecnico-mottu-production/media", request.Content);

                var response = await resultRequest.Content.ReadFromJsonAsync<ResponseCosmicDto>();

                return response?.Media;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending image to Cosmic");
                return null;
            }
        }

    }
}
