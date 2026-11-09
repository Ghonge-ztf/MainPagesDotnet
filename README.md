# Escolar - Master Page con CSS (.NET 8)

Proyecto ASP.NET Core MVC (.NET 8) que implementa el equivalente de una
**Master Page** en MVC: la vista `Views/Shared/_Layout.cshtml`, donde todas las
paginas renderizan su contenido mediante `@RenderBody()`.

## Estructura

```
Escolar/
├── Controllers/HomeController.cs        # Acciones: Index, Estudiantes, Materias
├── Models/
│   ├── Estudiante.cs                    # Modelo de la vista de estudiantes
│   ├── Materia.cs                       # Modelo de la vista de materias
│   └── ErrorViewModel.cs
├── Views/
│   ├── _ViewImports.cshtml              # @using + Tag Helpers
│   ├── _ViewStart.cshtml                # Define el Layout por defecto
│   ├── Home/
│   │   ├── Index.cshtml                 # Portada (hero + tarjetas)
│   │   ├── Estudiantes.cshtml           # Tabla de estudiantes
│   │   └── Materias.cshtml              # Tarjetas de materias
│   └── Shared/
│       ├── _Layout.cshtml               # MASTER PAGE (encabezado, nav, footer)
│       └── Error.cshtml
└── wwwroot/css/site.css                 # CSS propio (sin Bootstrap)
```

## La Master Page (`_Layout.cshtml`)

Define la estructura comun de todas las vistas:

- `<header class="site-header">` con la marca del instituto.
- `<nav class="site-nav">` con el menu (Inicio, Estudiantes, Materias).
- `<main class="container site-main">` con `@RenderBody()`: aqui se inyecta el
  contenido de cada vista hija.
- `<footer class="site-footer">` con el pie de pagina.
- `@await RenderSectionAsync("Scripts", required: false)` para scripts
  opcionales por vista.

El enlace al CSS se hace con `asp-append-version="true"`, que agrega un hash a la
URL para evitar cache del navegador.

## CSS aplicado (`wwwroot/css/site.css`)

Se removio Bootstrap y jQuery: el estilo es CSS puro y propio.

- Variables CSS (`--color-primary`, `--color-accent`, `--radius`, etc.).
- Layout con flexbox: `body` es columna y `site-main` ocupa el espacio restante,
  de modo que el footer queda pegado al fondo.
- Componentes: `.hero`, `.cards` (CSS Grid con `auto-fit`), `.card`, `.table-wrapper`,
  `.btn`, `.site-nav a`.
- Responsive con `@media (max-width: 640px)`: el encabezado pasa a columna y la
  navegacion se compacta.

## Como ejecutarlo

```bash
cd /home/ghz/Documents/a/programs/dead_bosca/Escolar
dotnet run --launch-profile http
```

Y abrir `http://localhost:5008` (o `http://localhost:5008/Home/Estudiantes` y
`http://localhost:5008/Home/Materias`).

Para ejecutar sin perfil de lanzamiento, fijando el puerto manualmente:

```bash
ASPNETCORE_URLS=http://127.0.0.1:5088 dotnet run --no-launch-profile
```

## Verificacion realizada

- `dotnet build` -> compilacion correcta, 0 errores, 0 advertencias.
- `curl` sobre `/`, `/Home/Estudiantes`, `/Home/Materias` -> HTTP 200.
- `curl` sobre `/css/site.css` -> HTTP 200, `text/css`.
- Render revisado en navegador: el layout, el menu, la tabla y las tarjetas
  muestran los estilos aplicados correctamente.
- Log de arranque sin warnings: `UseHttpsRedirection()` solo se registra cuando
  el perfil activo expone un puerto HTTPS, por lo que correr en HTTP puro no
  genera el aviso "Failed to determine the https port for redirect".

## Nota sobre `Program.cs`

La redireccion a HTTPS es condicional:

```csharp
if (app.Urls.Any(u => u.StartsWith("https://", StringComparison.OrdinalIgnoreCase)))
{
    app.UseHttpsRedirection();
}
```

Con el perfil `http` (el recomendado en Linux) no hay puerto HTTPS definido, asi
que el middleware se omite en lugar de emitir un warning en cada arranque.
