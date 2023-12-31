﻿using System;
using  APP_Alumno.Models;
using System.Linq;

namespace APP_Alumno
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = new AlumnoBdContext();
            var alumnos = db.Alumnos.ToList();
            foreach (var alu in alumnos){
                Console.WriteLine($"Id: {alu.Id} - Nombre: {alu.Nombre}, Apellido: {alu.Apellido}, " +
                $"Carrera: {alu.Carrera},  Fecha de Nacimiento: {alu.FechaNac},  Edad: {alu.Edad} años.");
            }
        }
    }
}
