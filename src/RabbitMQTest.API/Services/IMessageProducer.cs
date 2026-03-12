namespace RabbitMQTest.API.Services;
public interface IMessageProducer
{
    Task SendMessage<T>(T message);
}