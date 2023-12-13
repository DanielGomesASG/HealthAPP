using Entities.Entities;
using Entities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Security.Claims;
using System.Text;
using WebAPIs.Models;
using WebAPIs.Token;

namespace WebAPIs.Controllers
{
    // Criando a controladora de usuários
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public UsersController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // Criando a função geradora de tokens
        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("/api/CreateIdentityToken")]
        public async Task<IActionResult> CreateIdentityToken([FromBody] Login login)
        {
            if (string.IsNullOrWhiteSpace(login.email) || string.IsNullOrWhiteSpace(login.password))
            {
                return Unauthorized();
            }

            var result = await
                _signInManager.PasswordSignInAsync(login.email, login.password, false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                // Recuperando usuário logado
                var userCurrent = await _userManager.FindByEmailAsync(login.email);
                var userId = userCurrent.Id;

                var token = new TokenJWTBuilder()
                    .AddSecurityKey(JwtSecurityKey.Create("Secret_Key-12345678"))
                .AddSubject("Empresa - Health APP")
                .AddIssuer("Teste.Securiry.Bearer")
                .AddAudience("Teste.Securiry.Bearer")
                .AddClaim("userId", userId)
                .AddClaim("userType", userCurrent.Type.ToString())
                .AddExpiry(60)
                .Builder();

                return Ok(token.value);
            }
            else
            {
                return Unauthorized();
            }


        }

        // Criando a função que adiciona usuários regulares
        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("/api/AddRegularIdentityUser")]
        public async Task<IActionResult> AddRegularIdentityUser([FromBody] Login login)
        {
            if (string.IsNullOrWhiteSpace(login.email) || string.IsNullOrWhiteSpace(login.password))
                return Ok("Forneça os dados");


            var user = new ApplicationUser
            {
                UserName = login.email,
                Email = login.email,
                CPF = login.cpf,
                Type = UserType.Regular,
            };

            var result = await _userManager.CreateAsync(user, login.password);

            if (result.Errors.Any())
            {
                return Ok(result.Errors);
            }


            // Gerando confirmação caso necessário
            var userId = await _userManager.GetUserIdAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            // Retornando email 
            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result2 = await _userManager.ConfirmEmailAsync(user, code);

            if (result2.Succeeded)
                return Ok("Usuário adicionado com sucesso");
            else
                return Ok("Erro ao adicionar usuário");

        }

        // Criando a função que adiciona usuários admin
        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("/api/AddIdentityUser")]
        public async Task<IActionResult> AddAdminIdentityUser([FromBody] Login login)
        {
            if (string.IsNullOrWhiteSpace(login.email) || string.IsNullOrWhiteSpace(login.password))
                return Ok("Forneça os dados");


            var user = new ApplicationUser
            {
                UserName = login.email,
                Email = login.email,
                CPF = login.cpf,
                Type = UserType.Admin,
            };

            var result = await _userManager.CreateAsync(user, login.password);

            if (result.Errors.Any())
            {
                return Ok(result.Errors);
            }


            // Gerando confirmação caso necessário
            var userId = await _userManager.GetUserIdAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            // Retornando email 
            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result2 = await _userManager.ConfirmEmailAsync(user, code);

            if (result2.Succeeded)
                return Ok("Usuário adicionado com sucesso");
            else
                return Ok("Erro ao adicionar usuário");

        }

        // tentando validar o type
    }
}
