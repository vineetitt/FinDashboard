
using MQTTnet;
using MQTTnet.Client;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
namespace FinDashboard.API.Services
{
    public class MqttService
    {
        private readonly IMqttClient _mqttClient;

        public MqttService()
        {
            var factory = new MqttFactory();
            _mqttClient = factory.CreateMqttClient();
        }

        public async Task ConnectAsync()
        {
            var options = new MqttClientOptionsBuilder()
                .WithTcpServer("localhost", 1883)
                .Build();

            await _mqttClient.ConnectAsync(options);
        }

        public async Task PublishAsync(string topic, object payload)
        {
            if (!_mqttClient.IsConnected) return;

            var jsonPayload = JsonSerializer.Serialize(payload);
            var message = new MqttApplicationMessageBuilder()
                .WithTopic(topic)
                .WithPayload(Encoding.UTF8.GetBytes(jsonPayload))
                .Build();

            await _mqttClient.PublishAsync(message);
        }
    }
}
