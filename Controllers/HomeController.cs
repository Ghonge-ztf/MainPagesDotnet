using System.Diagnostics;
using Escolar.Models;
using Microsoft.AspNetCore.Mvc;

namespace Escolar.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Estudiantes()
    {
        var estudiantes = new List<Estudiante>
        {
            new Estudiante { Nombre = "Ana Perez Ruiz", Grado = "10A", Promedio = 4.6, Estado = "Regular" },
            new Estudiante { Nombre = "Luis Gomez Diaz", Grado = "10A", Promedio = 3.9, Estado = "Regular" },
            new Estudiante { Nombre = "Maria Lopez Soto", Grado = "10B", Promedio = 4.8, Estado = "Destacado" },
            new Estudiante { Nombre = "Carlos Ramirez Vega", Grado = "10B", Promedio = 3.2, Estado = "En recuperacion" }
        };

        return View(estudiantes);
    }

    public IActionResult Materias()
    {
        var materias = new List<Materia>
        {
            new Materia { Nombre = "Matematicas", Docente = "Ing. Sofia Marin", HorasSemana = 6 },
            new Materia { Nombre = "Lengua Castellana", Docente = "Lic. Diego Nieto", HorasSemana = 5 },
            new Materia { Nombre = "Ciencias Naturales", Docente = "BSc. Elena Cabrera", HorasSemana = 4 },
            new Materia { Nombre = "Historia", Docente = "Lic. Raul Ortiz", HorasSemana = 3 },
            new Materia { Nombre = "Informatica", Docente = "Ing. Paola Medina", HorasSemana = 3 },
            new Materia { Nombre = "Educacion Fisica", Docente = "Prof. Andres Vega", HorasSemana = 2 }
        };

        return View(materias);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
