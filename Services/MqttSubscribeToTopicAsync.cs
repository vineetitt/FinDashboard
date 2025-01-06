using MQTTnet;
using MQTTnet.Client;
using System.Text;

namespace FinDashboard.API.Services
{
    public class MqttSubscribeToTopicAsync
    {
        private IMqttClient _mqttClient;
        public MqttSubscribeToTopicAsync(string topic)
        {
            var factory = new MqttFactory();
            _mqttClient = factory.CreateMqttClient();

            var options = new MqttClientOptionsBuilder()
                .WithTcpServer("localhost", 1883)
                .WithCleanSession()
                .Build();

            _mqttClient.ApplicationMessageReceivedAsync +=async e =>
            {
                var message = Encoding.UTF8.GetString(e.ApplicationMessage.PayloadSegment.ToArray());
                Console.WriteLine($"Received message: {message} on topic: {e.ApplicationMessage.Topic}");
            };

             _mqttClient.ConnectAsync(options);
             _mqttClient.SubscribeAsync(new MqttTopicFilterBuilder().WithTopic(topic).Build());
        }
    }
}
