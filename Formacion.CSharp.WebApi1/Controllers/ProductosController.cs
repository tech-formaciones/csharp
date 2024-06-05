using Formacion.CSharp.Database.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Formacion.CSharp.WebApi1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly NorthwindContext _context;

        // GET /api/productos
        [HttpGet()]
        public ActionResult Get()
        {
            var productos = _context.Products
                .ToList();

            return Ok(productos);
        }

        // GET /api/productos/11
        [HttpGet("{id}")]
        public ActionResult Get(decimal id)
        {
            if (id < 1) return BadRequest(new { 
                Message = "Las referencias tienen que ser número positivos.",
                Data = id.ToString()
            });

            var producto = _context.Products
                .Where(r => r.ProductID == id)
                .FirstOrDefault();

            return Ok(producto);
        }

        // POST /api/productos
        [HttpPost()]        
        public ActionResult Post(Product producto)
        {
            if (_context.Products == null)
                return Problem("La entidad productos no existe (es Null)");

            try
            {
                _context.Products.Add(producto);
                _context.SaveChanges();                

                // Opción 1, retorna Creado 201, los datos recientemente insertado y la URL para consultar
                // el producto en la cabecera LOCATION
                return Created($"https://localhost:7082/api/Productos/{producto.ProductID}", producto);

                // Opción 2, es interesante cuando el registro puede cambiar facilmente por tener campos
                // autocalculados o verse afectado por trrigers
                return CreatedAtAction("Get", new { id = producto.ProductID }, producto);
            }
            catch (DbUpdateException e)
            {
                if (ProductoExiste(producto.ProductID))
                    return Conflict(new { Message = $"El producto {producto.ProductID} ya existe." });
                else 
                    return Conflict(new { Message = e.Message });
            }
            catch (Exception e)
            {
                return Conflict(new { Message = e.Message });
            }            
        }

        // PUT /api/productos
        [HttpPut()]
        public ActionResult Put()
        {
            return Ok();
        }

        // DELETE /api/productos
        [HttpDelete()]
        public ActionResult Delete()
        {
            return Ok();
        }

        private bool ProductoExiste(decimal id)
        {
            return _context.Products.Any(r => r.ProductID == id);
        }

        public ProductosController(NorthwindContext context)
        { 
            _context = context;
        }
    }
}
