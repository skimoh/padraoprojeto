//***CODE BEHIND - BY RODOLFO.FONSECA***//
using CodeBehind.PadraoProjeto.UnidadeTrabalho.Repositorio;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var sv = builder.Services;

sv.AddControllers();
sv.AddDbContext<DBContext>(opt => opt.UseInMemoryDatabase(databaseName: "DBMemoria"),
               ServiceLifetime.Scoped,
               ServiceLifetime.Scoped);

sv.AddEndpointsApiExplorer();
sv.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CodeBehind By Rodolfo Fonseca", Version = "v1" });
});
sv.AddScoped<IUnitOfWork, UnitOfWork>();


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "CodeBehind v1");
});

app.UseAuthorization();

app.MapControllers();

app.Run();
