using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SampleStoreDI.Models;
using Microsoft.EntityFrameworkCore;

namespace SampleStoreDI
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
            services.AddMvc();

            //Configuração da Injeção de Dependência do Repositório
            services.AddScoped<IProductRepository, ProductRepository>();

            //Configuração da Conexão 
            services.AddDbContext<ProductDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("ProductDbContext")));

            //Configuração das permissões de origens distintas
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigin",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    );
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("AllowAllOrigin");

            app.UseMvc();
        }
    }
}
