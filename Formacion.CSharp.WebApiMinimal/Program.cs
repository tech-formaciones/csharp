using Formacion.CSharp.Database.Models;
using Formacion.CSharp.WebApiMinimal.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace Formacion.CSharp.WebApiMinimal
{

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            /////////////////////////////////////////////////////////
            // Add services to the container.
            /////////////////////////////////////////////////////////
            
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<NorthwindContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("Northwind")));

            var app = builder.Build();



            /////////////////////////////////////////////////////////
            // Configure the HTTP request pipeline.
            /////////////////////////////////////////////////////////
            
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.MapGet("/api/v1/productos", (NorthwindContext context) => context.Products.ToList());

            app.MapGet("/api/v2/productos", async (NorthwindContext context) => await context.Products.ToListAsync());

            app.MapGet("/api/v1/productos/{id}", (int id, NorthwindContext context) => {
                    var producto = context.Products
                        .Where(r => r.ProductID == id)
                        .FirstOrDefault();

                    if (producto == null) return Results.NotFound();
                    else return Results.Ok(producto);
                });

            app.MapGet("/api/v2/productos/{id}", async (int id, NorthwindContext context) => { 
                    var producto = await context.Products
                        .Where(r => r.ProductID == id)
                        .FirstOrDefaultAsync();

                    if (producto == null) return Results.NotFound();
                    else return Results.Ok(producto);
                });

            app.MapGet("/api/v2.1/productos/{id}", async (int id, NorthwindContext context) => 
                await context.Products.FindAsync(id) 
                    is Product producto
                        ? Results.Ok(producto)
                        : Results.NotFound())
            .WithName("Products")
            .WithOpenApi(options => {
                var parameter = options.Parameters[0];
                parameter.Description = "Referencia del Producto";
                parameter.AllowEmptyValue = false;

                return options;
            });

            app.MapPost("/api/v1/productos", (Product producto, NorthwindContext context) => { 
                context.Products.Add(producto);
                context.SaveChanges();

                return Results.Created($"/api/v1/productos/{producto.ProductID}", producto);
            });

            app.MapPost("/api/v2/productos", async (Product producto, NorthwindContext context) => {
                await context.Products.AddAsync(producto);
                await context.SaveChangesAsync();

                return Results.Created($"/api/v1/productos/{producto.ProductID}", producto);
            });

            app.MapPut("/api/v1/productos/{id}", (int id, Product producto, NorthwindContext context) => {
                if (producto.ProductID != id) return Results.BadRequest();

                context.Products.Update(producto);
                context.SaveChanges();

                return Results.NoContent();
            });

            app.MapPut("/api/v2/productos/{id}", async (int id, Product producto, NorthwindContext context) => {
                if (producto.ProductID != id) return Results.BadRequest();
                
                context.Products.Update(producto);
                await context.SaveChangesAsync();

                return Results.NoContent();
            });

            app.MapDelete("/api/v1/productos/{id}", (int id, NorthwindContext context) => {
                //var producto = context.Products.Where(x => x.ProductID == id).FirstOrDefault();
                var producto = context.Products.Find(id);
                if(producto == null)  return Results.NotFound();

                context.Products.Remove(producto);
                context.SaveChanges();

                return Results.Ok();
            });

            app.MapDelete("/api/v2/productos/{id}", async (int id, NorthwindContext context) => {

                if (await context.Products.FindAsync(id) is Product producto)
                {
                    context.Products.Remove(producto);
                    await context.SaveChangesAsync();

                    return Results.Ok();
                }
                else return Results.NotFound();
            });

            app.MapGet("/api/v1/demo/{id}", (string id, HttpContext context) => {
                app.Logger.LogInformation("Inicia el EndPoint GET");

                context.Request.Headers.TryGetValue("x-data", out var data);

                return Results.Ok(new { 
                    Message = $"El identificador es {id}",
                    Data = $"El contenido de datos es {data}"
                });
            })
            .AddEndpointFilter(async (context, next) => {
                app.Logger.LogInformation("Ejecución Antes 1....");
                context.HttpContext.Request.Headers.Add("x-data", "1234567890");

                var result = await next(context);

                app.Logger.LogInformation("Ejecución Después 1....");
                context.HttpContext.Response.Headers.Add("Filtro-1", "Pasa por el filtro 1");

                return result;
            })
            .AddEndpointFilter(async (context, next) => {
                app.Logger.LogInformation("Ejecución Antes 2....");
                var result = await next(context);
                app.Logger.LogInformation("Ejecución Después 2....");
                return result;
            })
            .WithName("Demos")
            .WithOpenApi(options => {
                var parameter = options.Parameters[0];
                parameter.Description = "Identificador de prueba";
                parameter.AllowEmptyValue = false;

                return options;
            });

            // Elemento de Middleware escrito sin clase, directamente en el Program
            app.Use(async (context, next) => {
                context.Response.Headers.Add("x-demo-use", "Pasa por el USE");
                await next(context);
            });

            app.Use(async (context, next) => {
                string clave = builder.Configuration.GetValue<string>("Clave");
                context.Request.Headers.TryGetValue("APIKey", out var apikey);

                if (apikey != clave)
                {
                    context.Response.Headers.Clear();
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;  // HTTP 401

                    await context.Response.WriteAsJsonAsync(new
                    {
                        Error = HttpStatusCode.Unauthorized.ToString(),
                        Message = "Unauthorized"
                    });                    
                }
                else await next(context);
            });


            app.Run();
        }
    }
}
