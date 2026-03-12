using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMQTest.Worker.Consumers;

public class CreateStudentConsumer : BackgroundService
{
    private readonly ILogger<CreateStudentConsumer> _logger;
    public CreateStudentConsumer(ILogger<CreateStudentConsumer> logger)
    {
        _logger = logger;
    }

    protected async override Task ExecuteAsync(CancellationToken cancellationToken)
    {
        //1. Configuracion de RabbitMQ
        var factory = new ConnectionFactory
        {
            HostName = "localhost"
        };
        var connection = await factory.CreateConnectionAsync();
        var channel = await connection.CreateChannelAsync();
        await channel.QueueDeclareAsync(queue: "students", durable: false, exclusive: false, autoDelete: false, arguments: null);
        
        var consumer = new AsyncEventingBasicConsumer(channel);
        consumer.ReceivedAsync += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            _logger.LogInformation("Received message: {Message}", message);
        };

        //2. Subscribir al consumidor
        await channel.BasicConsumeAsync(queue: "students", autoAck: true, consumer: consumer);
    }
}