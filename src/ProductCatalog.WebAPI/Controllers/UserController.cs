using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProductCatalog.Application.DTOs.Account;
using ProductCatalog.Application.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProductCatalog.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        // private readonly IConfiguration _configuration;
        private readonly ITokenService _tokenService;

        public UserController(UserManager<IdentityUser> userManager, 
                              SignInManager<IdentityUser> signInManager, 
                              ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterUser([FromBody] UserDTO userDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new IdentityUser
            {
                UserName = userDTO.Email,
                Email = userDTO.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, userDTO.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok(new { Message = "User registered successfully." });
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByEmailAsync(loginDTO.Email);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "User not found.");
                return BadRequest(ModelState);
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDTO.Password, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                UserToken token = _tokenService.GenerateToken(loginDTO.Email);
                return Ok(token);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid Login");
                return BadRequest(ModelState);
            }
        }

        //[HttpPost("login")]
        //public async Task<ActionResult> Login([FromBody] UserDTO userDTO)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState.Values.SelectMany(e => e.Errors));
        //    }

        //    var result = await _signInManager.PasswordSignInAsync(userDTO.Email,
        //        userDTO.Password, isPersistent: false, lockoutOnFailure: false);

        //    if (result.Succeeded)
        //    {
        //        return Ok(GerarToken(userDTO));
        //    }
        //    else
        //    {
        //        ModelState.AddModelError(string.Empty, "Invalid login....");
        //        return BadRequest(ModelState);
        //    }
        //}

        //private UserToken GerarToken(UserDTO userDTO)
        //{
        //    var claims = new List<Claim>
        //    {
        //        new Claim(JwtRegisteredClaimNames.UniqueName, userDTO.Email),
        //        new Claim("meuPC", "teclado"),
        //        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        //    };

        //    if (userDTO.Email == "admin@localhost")
        //    {
        //        claims.Add(new Claim("DeletePermission", "true"));
        //    }

        //    var key = new SymmetricSecurityKey(
        //        Encoding.UTF8.GetBytes(_configuration["Jwt:key"]));
        //    var credenciais = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        //    var tokenExpiration = _configuration["TokenConfiguration:ExpireHours"];
        //    var expiration = DateTime.UtcNow.AddHours(double.Parse(tokenExpiration));

        //    JwtSecurityToken token = new JwtSecurityToken(
        //      issuer: _configuration["TokenConfiguration:Issuer"],
        //      audience: _configuration["TokenConfiguration:Audience"],
        //      claims: claims,
        //      expires: expiration,
        //      signingCredentials: credenciais);

        //    return new UserToken()
        //    {
        //        Authenticated = true,
        //        Token = new JwtSecurityTokenHandler().WriteToken(token),
        //        Expiration = expiration,
        //        Message = "Token JWT OK"
        //    };
        //}
    }
}
