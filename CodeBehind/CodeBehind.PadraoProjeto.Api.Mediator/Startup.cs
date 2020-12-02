using System;
using System.IO;
using System.Reflection;
using AutoMapper;
using mediator.application;
using mediator.dto;
using mediator.repository;
using mediator.repository.Entity;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.PlatformAbstractions;

namespace mediator
{
    public class Startup
    {

        public void ConfigureServices(IServiceCollection services)
        {            
            services.AddMvc(x => x.EnableEndpointRouting = false);
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddMediatR(typeof(Startup));

            services.AddMediatR(typeof(ClienteConsultarCommand).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(ClienteInserirCommand).GetTypeInfo().Assembly);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Cliente, ClienteDto>().ReverseMap();
            });
            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new Microsoft.OpenApi.Models.OpenApiInfo
                    {
                        Title = "Api Mediator",
                        Version = "v1",
                        Description = "Api Mediator",
                        Contact = new Microsoft.OpenApi.Models.OpenApiContact
                        {
                            Name = "rodolfo.fonseca",
                            Url = new Uri("https://www.youtube.com/channel/UCAIEPeFzWA9d7ukfvbw82gQ")
                        }
                    });

                string caminhoAplicacao = PlatformServices.Default.Application.ApplicationBasePath;
                string nomeAplicacao = PlatformServices.Default.Application.ApplicationName;
                string caminhoXmlDoc = Path.Combine(caminhoAplicacao, $"{nomeAplicacao}.xml");

                c.IncludeXmlComments(caminhoXmlDoc);
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseMvc();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Ola Mundo");
                });
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mediator Api");
            });

        }
    }
}
