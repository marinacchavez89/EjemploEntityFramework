using System;
using System.Collections.Generic;

namespace APP_Alumno.Models;

public partial class Alumno
{
    public int Id { get; set; }

    public string Nombre { get; set; }

    public string Apellido { get; set; }

    public string Carrera { get; set; }

    public DateOnly FechaNac { get; set; }

    public int? Edad { get; set; }

    public int? CarreraId { get; set; }

    public virtual Carrera CarreraNavigation { get; set; }
}
