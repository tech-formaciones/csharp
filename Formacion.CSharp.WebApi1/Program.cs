using Microsoft.EntityFrameworkCore;
using Formacion.CSharp.Database.Models;
using Formacion.CSharp.WebApi1.Middleware;
using Microsoft.OpenApi.Models;

namespace Formacion.CSharp.WebApi1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            ////////////////////////////////////////////////////////
            // Add services to the container.
            ////////////////////////////////////////////////////////

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options => 
                options.AddSecurityDefinition("APIKey", new OpenApiSecurityScheme { 
                    In = ParameterLocation.Header,
                    Name = "APIKey",
                    Type = SecuritySchemeType.ApiKey,
                    Description = "APIKey necesaria para acceder al servicio."
                }));

            builder.Services.AddDbContext<NorthwindContext>(options => 
                options.UseSqlServer(builder.Configuration.GetConnectionString("Northwind")));

            var app = builder.Build();


            ////////////////////////////////////////////////////////
            // Configure the HTTP request pipeline.
            ////////////////////////////////////////////////////////

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAutorizacion();

            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
