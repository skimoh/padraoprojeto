//***CODE BEHIND - BY RODOLFO.FONSECA***//

//MassTransit é uma estrutura de aplicativo distribuído de software livre para .NET

using CodeBehind.PadraoProjeto.Maquina;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

//Executa as ações fornecidas para inicializar o host
await Host.CreateDefaultBuilder(args)
    //configurar os serviços do aplicativo.
    .ConfigureServices((hostContext, services) =>
    {
        services.AddMassTransit(x =>
        {
            //consumidor
            x.AddConsumer<MessageConsumer>();

            //transporte
            x.UsingInMemory((context, cfg) =>
            {
                cfg.ConfigureEndpoints(context);
            });

        });

        services.AddHostedService<Worker>();
    }).RunConsoleAsync();
