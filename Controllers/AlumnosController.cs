using APP_Alumno.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class AlumnosController : ControllerBase
{
    private readonly AlumnoBdContext _db;

    public AlumnosController(AlumnoBdContext db)
    {
        _db = db;
    }

    [HttpGet]
    public IActionResult GetAlumnos()
    {
        var alumnos = _db.Alumnos.ToList();
        return Ok(alumnos);
    }

    // Aqui se pueden agregar otros métodos para Delete, Update, etc.
    [HttpGet("{id}")]
    public IActionResult GetAlumno(int id)
    {
        var alumno = _db.Alumnos.Find(id);

        if (alumno == null)
        {
            return NotFound();
        }

        return Ok(alumno);
    }

    [HttpPost]
    public IActionResult CreateAlumno([FromBody] Alumno nuevoAlumno)
    {
        _db.Alumnos.Add(nuevoAlumno);
        _db.SaveChanges();

        return CreatedAtAction(nameof(GetAlumno), new { id = nuevoAlumno.Id }, nuevoAlumno);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateAlumno(int id, [FromBody] Alumno alumnoActualizado)
    {
        var alumno = _db.Alumnos.Find(id);

        if (alumno == null)
        {
            return NotFound();
        }

        alumno.Nombre = alumnoActualizado.Nombre;
        alumno.Apellido = alumnoActualizado.Apellido;
        alumno.Carrera = alumnoActualizado.Carrera;
        alumno.FechaNac = alumnoActualizado.FechaNac;
        alumno.Edad = alumnoActualizado.Edad;
        alumno.CarreraId = alumnoActualizado.CarreraId;

        _db.SaveChanges();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteAlumno(int id)
    {
        var alumno = _db.Alumnos.Find(id);

        if (alumno == null)
        {
            return NotFound();
        }

        _db.Alumnos.Remove(alumno);
        _db.SaveChanges();

        return NoContent();
    }
}