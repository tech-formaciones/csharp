using Formacion.CSharp.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

            app.MapGet("/api/v1/demo/{id}", (string id, [FromHeader] string? data) => {
                app.Logger.LogInformation("Inicia el EndPoint");

                return Results.Ok(new { 
                    Message = $"El identificador es {id}",
                    Data = $"El identificador es {data}"
                });
            })
            .AddEndpointFilter(async (httpcontext, next) => {
                app.Logger.LogInformation("Ejecución Antes 1....");
                httpcontext.HttpContext.Request.Headers.Add("data", "1234567890");

                var result = await next(httpcontext);
                app.Logger.LogInformation("Ejecución Después 1....");
                httpcontext.HttpContext.Response.Headers.Add("Filtro-1", "Pasa por el filtro 1");

                return result;
            })
            .AddEndpointFilter(async (httpcontext, next) => {
                app.Logger.LogInformation("Ejecución Antes 2....");
                var result = await next(httpcontext);
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


            app.Run();
        }
    }
}
