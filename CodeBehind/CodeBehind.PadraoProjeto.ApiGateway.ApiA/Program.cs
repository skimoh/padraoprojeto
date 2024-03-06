//***CODE BEHIND - BY RODOLFO.FONSECA***//

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.UseHttpsRedirection();

app.MapGet("/destino/{id}", (HttpRequest request) =>
{
    var id = request.RouteValues["id"];

    return $"cheguei no destino {id}";
});

app.Run();
