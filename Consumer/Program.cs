

using Consumer;

ConsumerClass c = new();

c.ReceiveAsync(value => Console.WriteLine(value));