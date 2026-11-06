# Cuaderno de clientes

Aplicación de ejemplo construida con ASP.NET Core Razor Pages, Entity Framework Core, SQLite y ASP.NET Core Identity.

## Requisitos

- .NET 10 SDK

## Ejecutar

```bash
dotnet run
```

En desarrollo, el acceso inicial es `admin@clientes.local` con contraseña `Admin123*`. Cambia estos valores en `appsettings.Development.json` antes de compartir el entorno. En otros entornos configura `AdminUser__Email` y `AdminUser__Password` mediante variables de entorno y publica la aplicación detrás de HTTPS.

La base de datos `clientes.db` se crea automáticamente al iniciar. El formulario permite agregar, editar, buscar y eliminar clientes. `IdCliente` lo asigna SQLite. La búsqueda y el orden del directorio se ejecutan en EF Core con expresiones lambda (`Where` y `OrderBy`).