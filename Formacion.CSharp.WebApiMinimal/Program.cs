using Formacion.CSharp.Database.Models;
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
                        : Results.NotFound());

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


            app.Run();
        }
    }
}
