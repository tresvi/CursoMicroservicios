using Confluent.Kafka;

Console.WriteLine("Ejecutando Consumer simple. Auditoria de Clientes...");

var config = new ConsumerConfig
{
    GroupId = "auditoria-group",
    BootstrapServers = "localhost:9092",
    AutoOffsetReset = AutoOffsetReset.Earliest
};

using var consumer = new ConsumerBuilder<Null, string>(config).Build();
consumer.Subscribe("auditoria-clientes");
CancellationTokenSource token = new();

try
{
    while (true)
    {
        var response = consumer.Consume(token.Token);
        if (response.Message != null)
        {
            Console.WriteLine($"Cliente Insertado!: {response.Message.Value}");
            Thread.Sleep(10);
        }
    }
}
catch (Exception ex)
{
    Console.Error.WriteLine($"Error: {ex.Message}");
    throw;
}
finally { 
    consumer.Close(); 
}




