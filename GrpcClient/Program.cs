// See https://aka.ms/new-console-template for more information

using Grpc.Net.Client;
using GrpcClient;

//var input = new HelloRequest { Name = "osiiiii" };
var input = new CustomerLookupModel { UserId = 1 };
var channel = GrpcChannel.ForAddress("https://localhost:7088");
//var client = new Greeter.GreeterClient(channel);
var client = new Customer.CustomerClient(channel);
//var reply = await client.SayHelloAsync(input);
var customer = await client.GetCustomerInfoAsync(input);
Console.WriteLine($"{customer.FirstName}-{customer.LastName}");


using var call = client.GetNewCustomers(new NewCustomerRequest());
while (await call.ResponseStream.MoveNext(default))
{
    var currentCustomer = call.ResponseStream.Current;

    Console.WriteLine($"{currentCustomer.FirstName}-{currentCustomer.LastName}-{currentCustomer.EmailAddress}");
}



Console.ReadLine();


