using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Teste.Tecnico.Mottu.Matheus.Willock.Infrastructure.DepencencyInjection;

namespace Teste.Tecnico.Mottu.Matheus.Willock.Infrastructure.KafkaInfra
{
    public class ConsumerInfra : BackgroundService
    {

        private readonly IConsumer<Ignore, string> _consumer;
        private readonly ILogger<ConsumerInfra> _logger;
        private readonly ConsumerConfig _consumerConfig;
        private readonly IConfiguration _configuration;
        private readonly ConsumerParameters _consumerParameters;

        public ConsumerInfra(ILogger<ConsumerInfra> logger, IConfiguration configuration)
        {            
            _logger = logger;

            _configuration = configuration;
            var bootstrapServers = _configuration.GetSection("KafkaConfig").GetSection("BootstrapServers").Value;
            var topicName = _configuration.GetSection("KafkaConfig").GetSection("TopicName").Value;
            var groupId = _configuration.GetSection("KafkaConfig").GetSection("GroupId").Value;

            _consumerParameters = new ConsumerParameters(bootstrapServers, topicName, groupId);
            
            _consumerConfig = new ConsumerConfig
            {
                BootstrapServers = _consumerParameters.BootstrapServers,
                GroupId = _consumerParameters.GroupId,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            _logger = logger;
            _configuration = configuration;

            _consumer = new ConsumerBuilder<Ignore, string>(_consumerConfig).Build();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Consumer is running");
            _consumer.Subscribe(_consumerParameters.TopicName);

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Run(() =>
                {
                    var result = _consumer.Consume(stoppingToken);

                    _logger.LogInformation($"Consumed message '{result.Message.Value}' at: '{result.TopicPartitionOffset}'.");
                });                
            }
        }

        public override Task StopAsync(CancellationToken stoppingToken)
        {
            _consumer.Close();
            _logger.LogInformation("Consumer is stopped");

            return Task.CompletedTask;
        }



    }
}
