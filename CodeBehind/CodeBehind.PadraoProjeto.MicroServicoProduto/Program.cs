//***CODE BEHIND - BY RODOLFO.FONSECA***//
using AutoMapper;
using CodeBehind.PadraoProjeto.MicroServicoProduto.Application;
using CodeBehind.PadraoProjeto.MicroServicoProduto.Infrastructure;
using CodeBehind.PadraoProjeto.MicroServicoProduto.Infrastructure.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var sv = builder.Services;

sv.AddDbContext<ContextBase>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("ConexaoSQL")));

sv.AddMvc();
sv.AddEndpointsApiExplorer();
sv.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "ApiProduto",
            Description = "API de Produto",
            Version = "v1",
            Contact = new OpenApiContact()
            {
                Name = "Rodolfo Fonseca",
                Url = new Uri("https://github.com/skimoh"),
            },
            License = new OpenApiLicense()
            {
                Name = "MIT",
                Url = new Uri("http://opensource.org/licenses/MIT"),
            }
        });
});
sv.AddAutoMapper(typeof(Program));
sv.AddSingleton(provider => new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new ProfileMapper());
}).CreateMapper());

sv.AddScoped<IProdutoApp, ProdutoApp>();
sv.AddScoped<IProdutoRepository, ProdutoRepository>();
sv.AddSingleton<IMensageriaService, MensageriaService>();

var app = builder.Build();

app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiProduto v1");
});

app.Run();
