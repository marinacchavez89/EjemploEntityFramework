using APP_Alumno.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace APP_Alumno.Tests
{
    public class AlumnosControllerTests
    {
        [Fact]
        public void GetAlumnos_ReturnsAllAlumnos()
        {
            // Configurar DbContext en memoria
            var options = new DbContextOptionsBuilder<AlumnoBdContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new AlumnoBdContext(options))
            {
                // Preparar datos
                context.Alumnos.AddRange(new List<Alumno>
                {
                    new Alumno { Id = 1, Nombre = "Juan", Apellido = "Perez", Edad = 20, CarreraId = 1 },
                    new Alumno { Id = 2, Nombre = "Ana", Apellido = "Lopez", Edad = 22, CarreraId = 2 }
                });
                context.SaveChanges();

                // Instanciar el controlador con el DbContext
                var controller = new AlumnosController(context);

                // Ejecutar la acción
                var result = controller.GetAlumnos();

                // Validar el resultado
                var okResult = Assert.IsType<Microsoft.AspNetCore.Mvc.OkObjectResult>(result);
                var alumnos = Assert.IsType<List<Alumno>>(okResult.Value);
                Assert.Equal(2, alumnos.Count);
            }
        }

        [Fact]
        public void GetAlumno_ById_ReturnsAlumno()
        {
            // Configurar DbContext en memoria
            var options = new DbContextOptionsBuilder<AlumnoBdContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase1")
                .Options;

            using (var context = new AlumnoBdContext(options))
            {
                // Preparar datos
                context.Alumnos.Add(new Alumno { Id = 1, Nombre = "Juan", Apellido = "Perez", Edad = 20, CarreraId = 1 });
                context.SaveChanges();

                // Instanciar el controlador con el DbContext
                var controller = new AlumnosController(context);

                // Ejecutar la acción
                var result = controller.GetAlumno(1);

                // Validar el resultado
                var okResult = Assert.IsType<Microsoft.AspNetCore.Mvc.OkObjectResult>(result);
                var alumno = Assert.IsType<Alumno>(okResult.Value);
                Assert.Equal(1, alumno.Id);
            }
        }

        [Fact]
        public void GetAlumno_ReturnsAlumno_WhenAlumnoExists()
        {
            var options = new DbContextOptionsBuilder<AlumnoBdContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase2")
                .Options;

            using (var context = new AlumnoBdContext(options))
            {
                // Preparar datos
                context.Alumnos.Add(new Alumno { Id = 1, Nombre = "Carlos", Apellido = "Sanchez", Edad = 21, CarreraId = 1 });
                context.SaveChanges();

                var controller = new AlumnosController(context);

                // Ejecutar la acción
                var result = controller.GetAlumno(1);

                // Validar el resultado
                var okResult = Assert.IsType<Microsoft.AspNetCore.Mvc.OkObjectResult>(result);
                var alumno = Assert.IsType<Alumno>(okResult.Value);
                Assert.Equal("Carlos", alumno.Nombre);
            }
        }

        [Fact]
        public void CreateAlumno_AddsAlumno_ToDatabase()
        {
            var options = new DbContextOptionsBuilder<AlumnoBdContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase3")
                .Options;

            using (var context = new AlumnoBdContext(options))
            {
                var controller = new AlumnosController(context);

                // Crear un nuevo alumno
                var nuevoAlumno = new Alumno { Nombre = "Lucia", Apellido = "Gomez", Edad = 19, CarreraId = 1 };

                // Ejecutar la acción
                var result = controller.CreateAlumno(nuevoAlumno);

                // Validar el resultado
                var createdAtActionResult = Assert.IsType<Microsoft.AspNetCore.Mvc.CreatedAtActionResult>(result);
                var alumno = Assert.IsType<Alumno>(createdAtActionResult.Value);
                Assert.Equal("Lucia", alumno.Nombre);

                // Verificar que el alumno se haya agregado
                Assert.Equal(1, context.Alumnos.Count());
            }
        }
    }
}