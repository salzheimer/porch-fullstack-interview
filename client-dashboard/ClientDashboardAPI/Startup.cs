
using System.Data;
using Dapper.FluentMap;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MySql.Data.MySqlClient;
using ClientDashboardAPI.Repository;

namespace ClientDashboardAPI
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
            string connString = Configuration.GetConnectionString("ConnectionStrings");
            // If no full connection string is provided, build one from environment variables
            if (string.IsNullOrWhiteSpace(connString))
            {
                var host = System.Environment.GetEnvironmentVariable("DBHOST") ?? Configuration["DBHOST"] ?? "localhost";
                var user = System.Environment.GetEnvironmentVariable("MYSQL_USER") ?? Configuration["ConnectionStrings:MYSQL_USER"] ?? "root";
                var password = System.Environment.GetEnvironmentVariable("MYSQL_PASSWORD") ?? Configuration["ConnectionStrings:MYSQL_PASSWORD"] ?? "";
                var database = System.Environment.GetEnvironmentVariable("MYSQL_DATABASE") ?? Configuration["ConnectionStrings:MYSQL_DATABASE"] ?? "";
                connString = $"Server={host};Database={database};Uid={user};Pwd={password};";
            }

            services.AddTransient<IDbConnection>((sp) => new MySqlConnection(connString));
            
            services.AddScoped<Interfaces.IClientRepository,ClientRepository>();
            services.AddScoped<Interfaces.IPhoneNumberRepository,PhoneNumberRepository>();
            services.AddScoped<Interfaces.IPhoneNumberTypeRepository,PhoneNumberTypeRepository>();
            
            services.AddCors();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "ClientDashboardAPI - WebApi",
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            FluentMapper.Initialize(config =>
            {
                config.AddMap(new ClientMap());
                config.AddMap(new PhoneNumberMap());
                config.AddMap(new PhoneNumberTypeMap());

            });
            app.UseCors(options => options
                .WithOrigins(
                    "http://localhost:8080",
                    "http://localhost:8082",
                    "http://127.0.0.1:8080",
                    "http://127.0.0.1:8082")
                .AllowAnyMethod()
                .AllowAnyHeader()
            );

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Once running in Docker, try going to http://localhost:5000/swagger/index.html to test your API
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ClientDashboardAPI.WebApi.xml");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
