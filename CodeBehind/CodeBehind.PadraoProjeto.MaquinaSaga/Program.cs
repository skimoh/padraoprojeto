//***CODE BEHIND - BY RODOLFO.FONSECA***//
/*
 
Uma saga � uma transa��o de longa dura��o gerenciada por um coordenador. 
As sagas s�o iniciadas por um evento, as sagas orquestram eventos e as sagas mant�m o estado da transa��o geral. 
Sagas s�o projetadas para gerenciar a complexidade de uma transa��o distribu�da sem travamento e consist�ncia imediata.
 
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
    //O transporte na mem�ria destina-se a ser usado apenas em um �nico processo.(testes) N�o pode ser usado para comunica��o entre v�rios processos (mesmo que estejam na mesma m�quina).
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
