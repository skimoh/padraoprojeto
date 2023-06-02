//***CODE BEHIND - BY RODOLFO.FONSECA***//
/*
 
Uma saga é uma transação de longa duração gerenciada por um coordenador. 
As sagas são iniciadas por um evento, as sagas orquestram eventos e as sagas mantêm o estado da transação geral. 
Sagas são projetadas para gerenciar a complexidade de uma transação distribuída sem travamento e consistência imediata.
 
 */
using CodeBehind.PadraoProjeto.MaquinaSaga;
using CodeBehind.PadraoProjeto.MaquinaSaga.Event;
using MassTransit;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var sv = builder.Services;

sv.AddControllers();
sv.AddEndpointsApiExplorer();
sv.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CodeBehind By Rodolfo Fonseca", Version = "v1" });
});

sv.AddMassTransit(cfg =>
{
    //armazenamento dos estados
    cfg.AddSagaStateMachine<OrderStateMachine, OrderState>()
        .MongoDbRepository(r =>
        {
            r.Connection = "mongodb://localhost:27017";
            r.DatabaseName = "dbTeste";
            r.CollectionName = "pedido";
        });

    //transporte
    //O transporte na memória destina-se a ser usado apenas em um único processo.(testes) Não pode ser usado para comunicação entre vários processos (mesmo que estejam na mesma máquina).
    cfg.AddBus(context => Bus.Factory.CreateUsingInMemory(cfg =>
    {
        cfg.ReceiveEndpoint("orderRequest", e =>
        {
            e.UseMessageRetry(r => r.Interval(5, 1000));
            e.UseInMemoryOutbox();

            e.ConfigureSaga<OrderState>(context);
        });
    }));
    
    var timeout = TimeSpan.FromSeconds(10);
    var serviceAddress = new Uri("loopback://localhost/orderRequest");
    cfg.AddRequestClient<IPedidoEnviado>(serviceAddress, timeout);
});

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "CodeBehind v1");
});

app.UseAuthorization();

app.MapControllers();
app.Run();
