//***CODE BEHIND - BY RODOLFO.FONSECA***//
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace CodeBehind.PadraoProjeto.Api.Mediator
{
    /// <summary>
    /// CLASSE INICIAL
    /// </summary>
    public class Program
    {
        //METODO PRINCIPAL
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// CREATE SELF HOSTING
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
