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
}