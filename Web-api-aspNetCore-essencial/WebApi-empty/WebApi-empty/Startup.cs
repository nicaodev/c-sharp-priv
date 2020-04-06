using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WebApi_empty
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //Instalar O NewtonSoftJson por meio do comando
            //Install-Package Microsoft.AspNetCore.Mvc.NewtonsoftJson -Version 3.0
            //e registrar o serviço.
            services.AddControllers().AddNewtonsoftJson(); // Habilita o serviço dos controladores
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();


            //Configurando um novo EndPoint que é um endereço de onde vou acessar a API(URIs).
            app.UseEndpoints(endpoints => //Habilita o midlleware EndPoints
            {
                endpoints.MapControllers(); //Adiciona os endpoints aos métodos Actions. Mapeando para os controladores.
            });

        }
    }
}
