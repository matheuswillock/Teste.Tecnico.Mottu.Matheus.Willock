using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teste.Tecnico.Mottu.Matheus.Willock.Infrastructure.DepencencyInjection
{
    public class ConsumerParameters
    {
        public string BootstrapServers { get; set; }
        public string TopicName { get; set; }
        public string GroupId { get; set; }

        public ConsumerParameters(string bootstrapServers, string topicName, string groupId)
        {
            BootstrapServers = bootstrapServers;
            TopicName = topicName;
            GroupId = groupId;
        }
    }
}
