using System;
namespace Shared.Constants
{
    public class Settings
    {
        //public const String RabbitMQEndpoint = "amqp://localhost:5672";
        public const String RabbitMQEndpoint = "amqp://rabbitmq:5672"; // change host accordingly
        public const String RabbitMQExchangeName = "microservice.exg";
        public const String ServiceBusName = "Microservice.Bus";
    }
}

