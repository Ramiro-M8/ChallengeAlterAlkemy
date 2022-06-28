# Bienvenido a mi proyecto Challenge Alternativo de Iconos Geograficos

**Este proyecto esta hecho con:**
+ NET 6.0
+ Visual Studio 2022

**NuGet's Instalados:**
+ Microsoft.AspNetCore.Authentication.JwtBearer      
  + Version = "6.0.6"
  
+ Microsoft.AspNetCore.Identity.EntityFrameworkCore
  + Version = "6.0.6"
  
+ Microsoft.EntityFrameworkCore
  + Version = "6.0.6"
  
+ Microsoft.EntityFrameworkCore.Design 
  + Version = "6.0.6"
  
+ Microsoft.EntityFrameworkCore.SqlServer
  + Version = "6.0.6"
  
+ Microsoft.EntityFrameworkCore.Tools
  + Version = "6.0.6"
  
+ Microsoft.Extensions.Identity.Core
  + Version = "6.0.6"
  
+ Swashbuckle.AspNetCore
  + Version = "6.2.3"
  
+ System.Linq.Dynamic.Core
  + Version="1.2.18"
  
  
### En caso de usar este codigo y vayan a ejecutarlo, favor de realizar estos cambios: 
**Archivos en los que deben realizar los cambios:**
  + Program.cs
    + Modificar las variables:
      + ValidAudience = "https://localhost:xxxx", //Cambiar esto por el localhost que tengas
      + ValidIssuer = "https://localhost:xxxx", //Cambiar esto por el localhost que tengas
  + appsettings.json
    + Modificar los ConnectionStrings:
      + "APIGeoConnection" - Escribiendo el nombre de su servidor
      + "APIUsuConnection" - Escribiendo el nombre de su servidor

  + AutenticacionControlador.cs
    + Modificar las variables:
      + ValidAudience = "https://localhost:xxxx", //Cambiar esto por el localhost que tengas
      + ValidIssuer = "https://localhost:xxxx", //Cambiar esto por el localhost que tengas
