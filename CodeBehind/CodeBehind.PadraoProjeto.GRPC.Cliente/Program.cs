using CodeBehind.PadraoProjeto.GRPC;
using Grpc.Net.Client;

var channel = GrpcChannel.ForAddress("http://localhost:5184/");

var client = new Contrato.ContratoClient(channel);
var req = new ClienteRequest
{
    Name = "JOAO"
};

var reply = await client.SayHelloAsync(req);

Console.WriteLine("Saudacao: " + reply.Message);
Console.WriteLine("Pressione qualquer coisa para sair...");
Console.ReadKey();