using System;
using  APP_Alumno.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;


namespace APP_Alumno
{
    class Program
    {
        static void Main(string[] args)
        {
           using (var db = new AlumnoBdContext())
            {
                var alumnos = db.Alumnos.Include(a => a.CarreraNavigation).ToList();
                foreach (var alu in alumnos)
                {
                    Console.WriteLine($"Id: {alu.Id} - Nombre: {alu.Nombre}, Apellido: {alu.Apellido}, " +
                    $"Carrera: {alu.CarreraNavigation?.Nombre},  Fecha de Nacimiento: {alu.FechaNac},  Edad: {alu.Edad} años.");
                }
            }
        }
    }
}
