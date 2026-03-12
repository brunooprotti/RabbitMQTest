using System.Text;
using RabbitMQ.Client;
using Newtonsoft.Json; 

namespace RabbitMQTest.API.Services;

public class MessageProducer : IMessageProducer
{
    public async Task SendMessage<T>(T message)
    {
        var factory = new ConnectionFactory
        {
            HostName = "localhost"
        };
        var connection = await factory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();
        await channel.QueueDeclareAsync("students", exclusive: false);

        var json = JsonConvert.SerializeObject(message);
        var body = Encoding.UTF8.GetBytes(json);

        await channel.BasicPublishAsync(exchange: "", routingKey: "students", body: body);
    }
}