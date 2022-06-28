using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using IconosGeograficos.Modelos;
using IconosGeograficos.VistasModelos.Auth.Registro;
using IconosGeograficos.VistasModelos.Auth.Login;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace IconosGeograficos.Controladores
{
    [ApiController]
    [Route(template:"api/[controller]")]
    public class AutenticacionControlador : ControllerBase
    {
        private readonly UserManager<Usuario> _userManager;

        private readonly SignInManager<Usuario> _signInManager;

        public AutenticacionControlador(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        //Registro
        [HttpPost]
        [Route(template:"register")]
        //public async Task<IActionResult> Registro(string nombre, string password)
        public async Task<IActionResult> Registro(ModeloRequestRegistro modelo)
        {
            //Revisar si existe el usuario
            //var usuarioExiste = await _userManager.FindByNameAsync(nombre);
            var usuarioExiste = await _userManager.FindByNameAsync(modelo.Usuario);

            //Si existe, Devolver un error
            if (usuarioExiste != null) return StatusCode(StatusCodes.Status400BadRequest);

            //Si no existe, registrar al usuario
            var usuario = new Usuario
            {
                UserName = modelo.Usuario,
                Email = modelo.Email,
                isActive = true
            };

            var resultado = await _userManager.CreateAsync(usuario, modelo.Password);

            if (!resultado.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    status = "Error",
                    Message = $"Fallo la creacion de Usuario! Errores: {string.Join(",", resultado.Errors.Select(x => x.Description))}"
                });
            } 
            return Ok(new
            {
                status = "Success",
                Message = "Usuario creado satisfactoriamente!"
            });
        }

        //Login
        [HttpPost]
        [Route(template:"Login")]
        public async Task<IActionResult> Login(ModeloRequestLogin modelo)
        {
            //Revisar si existe el usuario
            var resultado = await _signInManager.PasswordSignInAsync(modelo.Usuario, modelo.Password, false, false);

            //Revisar que la contraseña que nos pasan sea la correcta
            if (resultado.Succeeded)
            {
                var usuarioActual = await _userManager.FindByNameAsync(modelo.Usuario);

                if (usuarioActual.isActive)
                {
                    //Generear Token

                    //Devolver Token Creado
                    return Ok(await GetToken(usuarioActual));
                }
            }

            //Si no existe, Devolver un error
            return StatusCode(StatusCodes.Status401Unauthorized, new
            {
                status = "Error",
                Message = $"El usuario {modelo.Usuario} no esta autorizado, favor de volver a verificar!"
            });

        }

        private async Task<ModeloResponseLogin> GetToken(Usuario usuarioActual)
        {
            var rolesUsuario = await _userManager.GetRolesAsync(usuarioActual);

            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, usuarioActual.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            authClaims.AddRange(rolesUsuario.Select(x=>new Claim(ClaimTypes.Role, x)));

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(s: "KeySecretaSuperLargaDeAUTORIZACION"));

            var token = new JwtSecurityToken(
                issuer: "https://localhost:44353", //Cambiar esto por el localhost que tengas
                audience: "https://localhost:44353", //Cambiar esto por el localhost que tengas
                expires: DateTime.Now.AddHours(1), //El token dura 1 hora
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));

            return new ModeloResponseLogin
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ValidTo = token.ValidTo
            };
        }
    }
}
