using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography.X509Certificates;
using Teste.Tecnico.Mottu.Matheus.Willock.Domain.Models;
using Newtonsoft.Json;
using Teste.Tecnico.Mottu.Matheus.Willock.Domain.Libs;
using Teste.Tecnico.Mottu.Matheus.Willock.Infrastructure.KafkaInfra.Contracts;
using Teste.Tecnico.Mottu.Matheus.Willock.Infrastructure.Repositories.OrderRepository;

namespace Teste.Tecnico.Mottu.Matheus.Willock.Infrastructure.KafkaInfra
{
    public class ProducerInfra : IProducerInfra
    {
        private readonly IConfiguration _configuration;
        private readonly ProducerConfig _producerConfig;
        private readonly ILogger<ProducerInfra> _logger;

        public ProducerInfra(IConfiguration configuration, ILogger<ProducerInfra> logger)
        {
            _configuration = configuration;
            var bootstrapServers = _configuration.GetSection("KafkaConfig").GetSection("BootstrapServers").Value;

            _producerConfig = new ProducerConfig
            {
                BootstrapServers = bootstrapServers
            };

            _logger = logger;
        }

        public async Task<Output> SendMessage(Order order)
        {
            var topic = _configuration.GetSection("KafkaConfig").GetSection("TopicName").Value;

            var output = new Output();
            try
            {
                using (var producer = new ProducerBuilder<string, string>(_producerConfig).Build())
                {
                    var message = JsonConvert.SerializeObject(order);

                    var Randomkey = GenerateRandomKey();

                    var result = await producer.ProduceAsync(topic: topic, new Message<string, string> { Key = Randomkey, Value = message });

                    if (result.Status != PersistenceStatus.Persisted)
                    {
                        output.AddErrorMessage("Failed to send the message.");
                        return output;
                    }

                    _logger.LogInformation(result.Status.ToString() + " - " + order.Id);

                    _logger.LogInformation($"Mensagem enviada para o tópico {topic} com a chave {result.Message.Key} e valor {result.Message.Value}");

                    output.AddMessage(result.Status.ToString() + " - " + order.Id);
                    output.AddResult(result.Message.Value);
                    return output;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to send the message - {e.Message}");
                output.AddErrorMessage($"Failed to send the message {e.Message}");
                return output;
            }

        }

        private string GenerateRandomKey()
        {
            var random = new Random();
            var keyLength = 10;
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var key = new char[keyLength];
            for (int i = 0; i < keyLength; i++)
            {
                key[i] = chars[random.Next(chars.Length)];
            }
            return new string(key);
        }
    }
}
