using APP_Alumno.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

[Route("api/[controller]")]
[ApiController]
public class CarrerasController : ControllerBase
{
    private readonly AlumnoBdContext _db;

    public CarrerasController(AlumnoBdContext db)
    {
        _db = db;
    }

    // GET: api/carreras
    [HttpGet]
    public IActionResult GetCarreras()
    {
        var carreras = _db.Carreras.ToList();
        return Ok(carreras);
    }

    // GET: api/carreras/{id}
    [HttpGet("{id}")]
    public IActionResult GetCarrera(int id)
    {
        var carrera = _db.Carreras.Find(id);

        if (carrera == null)
        {
            return NotFound();
        }

        return Ok(carrera);
    }

    // POST: api/carreras
    [HttpPost]
    public IActionResult CreateCarrera([FromBody] Carrera nuevaCarrera)
    {
        if (nuevaCarrera == null || string.IsNullOrEmpty(nuevaCarrera.Nombre))
        {
            return BadRequest("El nombre de la carrera no puede estar vacío.");
        }

        _db.Carreras.Add(nuevaCarrera);
        _db.SaveChanges();

        return CreatedAtAction(nameof(GetCarrera), new { id = nuevaCarrera.Id }, nuevaCarrera);
    }

    // PUT: api/carreras/{id}
    [HttpPut("{id}")]
    public IActionResult UpdateCarrera(int id, [FromBody] Carrera carreraActualizada)
    {
        var carrera = _db.Carreras.Find(id);

        if (carrera == null)
        {
            return NotFound();
        }

        if (string.IsNullOrEmpty(carreraActualizada.Nombre))
        {
            return BadRequest("El nombre de la carrera no puede estar vacío.");
        }

        carrera.Nombre = carreraActualizada.Nombre;
        _db.SaveChanges();

        return NoContent();
    }

    // DELETE: api/carreras/{id}
    [HttpDelete("{id}")]
    public IActionResult DeleteCarrera(int id)
    {
        var carrera = _db.Carreras.Find(id);

        if (carrera == null)
        {
            return NotFound();
        }

        _db.Carreras.Remove(carrera);
        _db.SaveChanges();

        return NoContent();
    }
}