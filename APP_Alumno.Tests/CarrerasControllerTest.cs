using APP_Alumno.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP_Alumno.Tests
{
    public class CarrerasControllerTest
    {
        private DbContextOptions<AlumnoBdContext> GetInMemoryDatabaseOptions(string dbName)
        {
            return new DbContextOptionsBuilder<AlumnoBdContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;
        }

        [Fact]
        public void GetCarreras_ReturnsAllCarreras()
        {
            // Configurar DbContext en memoria
            var options = GetInMemoryDatabaseOptions("TestDatabase");

            using (var context = new AlumnoBdContext(options))
            {
                // Preparar datos
                context.Carreras.AddRange(new Carrera
                {
                    Id = 1,
                    Nombre = "Ingeniería Informática"
                }, new Carrera
                {
                    Id = 2,
                    Nombre = "Licenciatura en Psicología"
                });
                context.SaveChanges();

                // Instanciar el controlador con el DbContext
                var controller = new CarrerasController(context);

                // Ejecutar la acción
                var result = controller.GetCarreras();

                // Validar el resultado
                var okResult = Assert.IsType<OkObjectResult>(result);
                var carreras = Assert.IsType<System.Collections.Generic.List<Carrera>>(okResult.Value);
                Assert.Equal(2, carreras.Count);
            }
        }

        [Fact]
        public void GetCarrera_ById_ReturnsCarrera()
        {
            // Configurar DbContext en memoria
            var options = GetInMemoryDatabaseOptions("TestDatabase");

            using (var context = new AlumnoBdContext(options))
            {
                // Preparar datos
                context.Carreras.Add(new Carrera { Id = 1, Nombre = "Ingeniería Informática" });
                context.SaveChanges();

                // Instanciar el controlador con el DbContext
                var controller = new CarrerasController(context);

                // Ejecutar la acción
                var result = controller.GetCarrera(1);

                // Validar el resultado
                var okResult = Assert.IsType<OkObjectResult>(result);
                var carrera = Assert.IsType<Carrera>(okResult.Value);
                Assert.Equal(1, carrera.Id);
            }
        }

        [Fact]
        public void GetCarrera_ById_ReturnsNotFound_WhenNotExist()
        {
            // Configurar DbContext en memoria
            var options = GetInMemoryDatabaseOptions("TestDatabase");

            using (var context = new AlumnoBdContext(options))
            {
                // Instanciar el controlador con el DbContext
                var controller = new CarrerasController(context);

                // Ejecutar la acción
                var result = controller.GetCarrera(1);

                // Validar el resultado
                Assert.IsType<NotFoundResult>(result);
            }
        }

        [Fact]
        public void CreateCarrera_ReturnsCreatedAtAction()
        {
            // Configurar DbContext en memoria
            var options = GetInMemoryDatabaseOptions("TestDatabase");

            using (var context = new AlumnoBdContext(options))
            {
                // Instanciar el controlador con el DbContext
                var controller = new CarrerasController(context);

                // Crear nueva carrera
                var nuevaCarrera = new Carrera { Nombre = "Abogacía" };

                // Ejecutar la acción
                var result = controller.CreateCarrera(nuevaCarrera);

                // Validar el resultado
                var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
                var carrera = Assert.IsType<Carrera>(createdAtActionResult.Value);
                Assert.Equal("Abogacía", carrera.Nombre);
            }
        }

        [Fact]
        public void CreateCarrera_ReturnsBadRequest_WhenInvalidData()
        {
            // Configurar DbContext en memoria
            var options = GetInMemoryDatabaseOptions("TestDatabase");

            using (var context = new AlumnoBdContext(options))
            {
                // Instanciar el controlador con el DbContext
                var controller = new CarrerasController(context);

                // Crear nueva carrera con nombre vacío
                var nuevaCarrera = new Carrera { Nombre = "" };

                // Ejecutar la acción
                var result = controller.CreateCarrera(nuevaCarrera);

                // Validar el resultado
                Assert.IsType<BadRequestObjectResult>(result);
            }
        }

        [Fact]
        public void UpdateCarrera_ReturnsNoContent()
        {
            // Configurar DbContext en memoria
            var options = GetInMemoryDatabaseOptions("TestDatabase");

            using (var context = new AlumnoBdContext(options))
            {
                // Preparar datos
                context.Carreras.Add(new Carrera { Id = 1, Nombre = "Ingeniería Informática" });
                context.SaveChanges();

                // Instanciar el controlador con el DbContext
                var controller = new CarrerasController(context);

                // Crear carrera actualizada
                var carreraActualizada = new Carrera { Nombre = "Ingeniería Electrónica" };

                // Ejecutar la acción
                var result = controller.UpdateCarrera(1, carreraActualizada);

                // Validar el resultado
                Assert.IsType<NoContentResult>(result);
            }
        }

        [Fact]
        public void UpdateCarrera_ReturnsNotFound_WhenCarreraDoesNotExist()
        {
            // Configurar DbContext en memoria
            var options = GetInMemoryDatabaseOptions("TestDatabase");

            using (var context = new AlumnoBdContext(options))
            {
                // Instanciar el controlador con el DbContext
                var controller = new CarrerasController(context);

                // Crear carrera actualizada
                var carreraActualizada = new Carrera { Nombre = "Ingeniería Electrónica" };

                // Ejecutar la acción
                var result = controller.UpdateCarrera(1, carreraActualizada);

                // Validar el resultado
                Assert.IsType<NotFoundResult>(result);
            }
        }

        [Fact]
        public void DeleteCarrera_ReturnsNoContent()
        {
            // Configurar DbContext en memoria
            var options = GetInMemoryDatabaseOptions("TestDatabase");

            using (var context = new AlumnoBdContext(options))
            {
                // Preparar datos
                context.Carreras.Add(new Carrera { Id = 1, Nombre = "Ingeniería Informática" });
                context.SaveChanges();

                // Instanciar el controlador con el DbContext
                var controller = new CarrerasController(context);

                // Ejecutar la acción
                var result = controller.DeleteCarrera(1);

                // Validar el resultado
                Assert.IsType<NoContentResult>(result);
            }
        }

        [Fact]
        public void DeleteCarrera_ReturnsNotFound_WhenCarreraDoesNotExist()
        {
            // Configurar DbContext en memoria
            var options = GetInMemoryDatabaseOptions("TestDatabase");

            using (var context = new AlumnoBdContext(options))
            {
                // Instanciar el controlador con el DbContext
                var controller = new CarrerasController(context);

                // Ejecutar la acción
                var result = controller.DeleteCarrera(1);

                // Validar el resultado
                Assert.IsType<NotFoundResult>(result);
            }
        }
    }
}

