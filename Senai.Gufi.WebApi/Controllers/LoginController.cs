using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Senai.Gufi.WebApi.Domains;
using Senai.Gufi.WebApi.Interfaces;
using Senai.Gufi.WebApi.Repositories;
using Senai.Gufi.WebApi.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Senai.Gufi.WebApi.Controllers
{
    [Produces("application/json")]

    [Route("api/[controller]")]

    [ApiController]

    public class LoginController : ControllerBase
    {
        private IUsuarioRepository _usuarioRepository { get; set; }

        public LoginController()
        {
            _usuarioRepository = new UsuarioRepository();
        }

        [HttpPost]
        public IActionResult Post(LoginViewModel login)
        {

            Usuario usuarioBuscado = _usuarioRepository.(login.Email, login.Senha);


            if (usuarioBuscado == null)
            {

                return NotFound("E-mail ou senha inválidos");

            }

            var claims = new[]
                        {
                new Claim(JwtRegisteredClaimNames.Email, usuarioBuscado.Email),
                new Claim(JwtRegisteredClaimNames.Jti, usuarioBuscado.IdUsuario.ToString()),
                new Claim(ClaimTypes.Role, usuarioBuscado.IdTipoUsuario.ToString())
            };


            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("Gufi-chave-autenticacao"));


            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


            var token = new JwtSecurityToken(
                issuer: "Gufi.WebApi",                // emissor do token
                audience: "Gufi.WebApi",              // destinatário do token
                claims: claims,                          // dados definidos acima
                expires: DateTime.Now.AddMinutes(30),    // tempo de expiração
                signingCredentials: creds                // credenciais do token
            );


            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            });


        }
    }
}

