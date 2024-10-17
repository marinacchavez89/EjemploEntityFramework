using System;
using System.Collections.Generic;

namespace APP_Alumno.Models;

public partial class Carrera
{
   public int Id { get; set; }
    public string Nombre { get; set; }    
    public virtual ICollection<Alumno> Alumnos { get; set; }
}
