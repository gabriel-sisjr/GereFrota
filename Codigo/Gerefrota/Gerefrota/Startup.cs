using Domain.Entities.Context;
using Gerefrota.Extensions.Injections;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Gerefrota
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Gerefrota", Version = "v1" });
            });

            /*
             * Foi utilizado ContextPool para que a instancia do contexto seja gerenciada de maneira mais inteligente.
             * P.S. Sempre que gerar o contexto novamente (via Scaffold) deve-se deletar o construtor vazio, para que funcione.
             * A issue j� foi aberta no repositorio do Entity.
             */
            services.AddDbContextPool<ContextDB>(x => x.UseMySQL(Configuration.GetConnectionString("MySql")));

            // Extensions
            services
                .AddServiceInjection()
                .AddRepositoryInjection()
                .AddAutoMapperInjection();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gerefrota v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
