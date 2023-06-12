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


/*Hacerlo todo en consolas separadas y dejarlas corriendo
 * 
0)Instalacion: https://www.apache.org/dyn/closer.cgi?path=/kafka/2.1.0/kafka_2.11-2.1.0.tgz   (instalar el JDK de Java 8 tambien) 
Descomprimir la carpeta en C:\tools 
 
1)Levantar coordinador de transaccion "Zookeeper" (en puerto 2181 por defecto):
PS C:\tools\kafka_2.11-2.1.0> .\bin\windows\zookeeper-server-start.bat .\config\zookeeper.properties 

2) Levantar kafka (puerto 9092)
PS C:\tools\kafka_2.11-2.1.0> .\bin\windows\kafka-server-start.bat .\config\server.properties

3) Crear un topico
PS C:\tools\kafka_2.11-2.1.0\bin\windows> .\kafka-topics.bat --create --zookeeper localhost:2181 --replication-factor 1 --partitions 1 --topic auditoria-clientes

4)(OPCIONAL) Verificar si se creó (se listan TODOS los topicos activos) 
PS C:\tools\kafka_2.11-2.1.0\bin\windows> .\kafka-topics.bat --list --zookeeper localhost:2181

*/