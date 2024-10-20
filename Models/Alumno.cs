﻿using System;
using System.Collections.Generic;

namespace APP_Alumno.Models;

public partial class Alumno
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public DateTime FechaNac { get; set; }
    public int Edad { get; set; }
    public int CarreraId { get; set; }     
    public virtual Carrera CarreraNavigation { get; set; }
}
