using BecasApi.Entidades.DTOS;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BecasApi.Controllers
{
    [Route("api/cuenta")]
    [ApiController]
    public class CuentaCotroller:ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConfiguration configuration;
        private readonly SignInManager<IdentityUser> signInManager;
        
        public CuentaCotroller(UserManager<IdentityUser> userManager,IConfiguration configuration,SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.signInManager = signInManager;
        }
        [HttpPost("crear")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<RespuestaAutenticacion>> Crear([FromBody] CredencialesDTO credenciales)
        {
            var usuario = new IdentityUser { UserName = credenciales.correo, Email = credenciales.correo };
            var resultado = await userManager.CreateAsync(usuario, credenciales.contrasena);
            if (resultado.Succeeded)
            {
                return await ConstruirToken(credenciales);
            }
            else
            {
                return BadRequest(resultado.Errors);
            }
        }
        [HttpPost("login")]
        public async Task<ActionResult<RespuestaAutenticacion>> login([FromBody] CredencialesDTO credenciales)
        {
            var resultado = await signInManager.PasswordSignInAsync(credenciales.correo, credenciales.contrasena, isPersistent: false,
                lockoutOnFailure: false);
            if (resultado.Succeeded)
            {
                return await ConstruirToken(credenciales);
            }
            else
            {
                return BadRequest("Loging Incorrecto");
            }
        }
            private async Task<RespuestaAutenticacion> ConstruirToken(CredencialesDTO credenciales)
        {
            var claims = new List<Claim>()
            {
                new Claim("email",credenciales.correo),

            };

            var usuario = await userManager.FindByEmailAsync(credenciales.correo);
            var claimsDB = await userManager.GetClaimsAsync(usuario);
            claims.AddRange(claimsDB);
            var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["llavejwt"]));
            var creds = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256);
            var expiracion = DateTime.UtcNow.AddDays(1);

            var token = new JwtSecurityToken(issuer: null, audience: null, claims: claims, expires: expiracion, signingCredentials: creds);

            return new RespuestaAutenticacion()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiracion = expiracion
            };
        }
       
    }
}
