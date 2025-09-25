using AutoMapper;
using FablabWebAPI.DTOs;
using FablabWebAPI.DTOs.Autenticacion;
using FablabWebAPI.Entities;
using FablabWebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security;
using System.Security.Claims;
using System.Text;

namespace FablabWebAPI.Controllers.Autenticacion
{


    [ApiController]
    [Route("api/autenticacion/usuarios")]
    public class UsuariosAutenticacionController: ControllerBase
    {
        private readonly UserManager<Usuario> userManager;
        private readonly IConfiguration configuration;
        private readonly SignInManager<Usuario> signInManager;
        private readonly IMapper autoMapper;
        private readonly IServicioUsuarios servicioUsuarios;

        public UsuariosAutenticacionController(UserManager<Usuario> userManager, IConfiguration configuration, 
            SignInManager<Usuario> signInManager, IMapper autoMapper, IServicioUsuarios servicioUsuarios)
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.signInManager = signInManager;
            this.autoMapper = autoMapper;
            this.servicioUsuarios = servicioUsuarios;
        }

        [HttpPost("registro")]
        public async Task<ActionResult<AutenticacionRespuestaDto>> RegistrarUsuario(CredencialesRegistroDto credencialesDto)
        {

            var usuario = new Usuario
            {
                UserName = credencialesDto.Nombre.Replace(" ",""), //Reemplazar espacios
                Nombre = credencialesDto.Nombre, //de lab
                Email = credencialesDto.Email,
                CorreoInstitucional = credencialesDto.Email, //de lab
                Apellido = credencialesDto.Apellido,
                Rut = credencialesDto.Rut,
                Carrera = credencialesDto.Carrera,
                PhoneNumber = credencialesDto.Telefono,

            };

            var crearUsuario = await userManager.CreateAsync(usuario,credencialesDto.Contrasena!);


            if (crearUsuario.Succeeded)
            {

                //Crear token
                var token = await ConstruirJwt(credencialesDto.Email);

                return token;

            }
            else
            {
                foreach(var err in crearUsuario.Errors)
                {
                    ModelState.AddModelError(string.Empty, err.Description);

                }
                return ValidationProblem();


            }

        }

        [HttpPost("login")]

        public async Task<ActionResult<AutenticacionRespuestaDto>> login(CredencialesLoginDto credencialesLoginDto)
        {
            var usuario = await userManager.FindByEmailAsync(credencialesLoginDto.Email);


            if(usuario is null)
            {
                return BadRequest();

            }


            var resultado = await signInManager.CheckPasswordSignInAsync(usuario, credencialesLoginDto.Contrasena,lockoutOnFailure: false);

            if (resultado.Succeeded)
            {
                var token = await ConstruirJwt(credencialesLoginDto.Email);

                return token;
            }
            else
            {

                return BadRequest(); //TODO: Error personalizado
            }
            
        }


  

        [HttpGet("check-status")]
        public async Task<ActionResult<AutenticacionRespuestaDto>> CheckearStatusToken()
        {
            var usuario = await servicioUsuarios.ObtenerUsuario();


            if(usuario is null)
            {
                return NotFound();
            }

            return await ConstruirJwt(usuario.Email!);
        }




        private async Task<AutenticacionRespuestaDto> ConstruirJwt(string email)

        {
            //Se agrego datos de usuario al token, conversar con equipo para modificacion


            var claims = new List<Claim>
            { 
                new Claim("email", email)
            };

            var usuario = await userManager.FindByEmailAsync(email);
            var usuarioDto = autoMapper.Map<UsuarioDto>(usuario);
            var claimsUsuarioDB = await userManager.GetClaimsAsync(usuario!);

            claims.AddRange(claimsUsuarioDB);

            var llaveJwt = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["llavejwt"]!));
            var credencialesFirma = new SigningCredentials(llaveJwt, SecurityAlgorithms.HmacSha256);

            var expiracionJwt = DateTime.UtcNow.AddDays(1);

            //Construccion de jwt

            var JWT = new JwtSecurityToken(issuer: null, audience: null, claims: claims, expires: expiracionJwt, signingCredentials: credencialesFirma);

            //escribir token

            var token = new JwtSecurityTokenHandler().WriteToken(JWT);


            return new AutenticacionRespuestaDto
            {
                token = token,
                Expiracion = expiracionJwt,
                Usuario = usuarioDto
            };












        }
    }
}
