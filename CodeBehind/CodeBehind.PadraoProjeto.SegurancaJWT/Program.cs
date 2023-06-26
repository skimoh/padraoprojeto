//***CODE BEHIND - BY RODOLFO.FONSECA***//

using CodeBehind.PadraoProjeto.SegurancaJWT;
using CodeBehind.PadraoProjeto.SegurancaJWT.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var sv = builder.Services;
sv.AddEndpointsApiExplorer();
sv.AddControllers();

var key = Encoding.ASCII.GetBytes(TokenService._chave);
sv.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
           //Habilita a autentica��o do portador JWT usando o esquema AuthenticationScheme padr�o.
           .AddJwtBearer(x =>
           {
               x.RequireHttpsMetadata = false; //Obt�m ou define se HTTPS � necess�rio para o endere�o ou autoridade de metadados
               x.SaveToken = true; //� uma propriedade que define se o token do portador deve ser armazenado em AuthenticationProperties ap�s uma autoriza��o bem-sucedida
               x.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuerSigningKey = true, //Obt�m ou define um booleano que controla se a valida��o do SecurityKey que assinou o securityToken � chamada.
                   IssuerSigningKey = new SymmetricSecurityKey(key), //Obt�m ou define o SecurityKey que deve ser usado para valida��o de assinatura.
                   ValidateIssuer = false,//Obt�m ou define um booleano para controlar se o emissor ser� validado durante a valida��o do token.
                   ValidateAudience = false //Obt�m ou define um booleano para controlar se o p�blico(destinat�tio) ser� validado durante a valida��o do token.
               };
           });

sv.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api JTW", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization ",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});




sv.AddScoped<IUsuarioRepository, UsuarioRepository>();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "CodeBehind v1");
});

app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.MapControllers();
app.Run();
