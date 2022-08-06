using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using ADProject.Data;
using ADProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using ADProject.Token;
using ADProject.Interface;

namespace ADProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly EmployeesDBContext context;
        private readonly JWTSetting setting;
        private readonly IRefreshTokenGenerator tokenGenerator;
        public UsersController(EmployeesDBContext learn_DB, IOptions<JWTSetting> options, IRefreshTokenGenerator _refreshToken)
        {
            context = learn_DB;
            setting = options.Value;
            tokenGenerator = _refreshToken;
        }

        [NonAction]
        public TokenResponse Authenticate(int userId, Claim[] claims)
        {
            TokenResponse tokenResponse = new TokenResponse();
            var tokenkey = Encoding.UTF8.GetBytes(setting.securitykey);
            var tokenhandler = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(15),
                 signingCredentials: new SigningCredentials(new SymmetricSecurityKey(tokenkey), SecurityAlgorithms.HmacSha256)

                );
            tokenResponse.JWTToken = new JwtSecurityTokenHandler().WriteToken(tokenhandler);
            tokenResponse.RefreshToken = tokenGenerator.GenerateToken(userId);

            return tokenResponse;
        }

        [Route("Authenticate")]
        [HttpPost]
        public IActionResult Authenticate([FromBody] userCredential user)
        {
            TokenResponse tokenResponse = new TokenResponse();
            var _user = context.Users.FirstOrDefault(o => o.Name.Trim().ToLower() == user.userName.Trim().ToLower() && o.Password == user.password && o.IsActive == true);
            if (_user == null)
                return Unauthorized();

            var tokenhandler = new JwtSecurityTokenHandler();
            var tokenkey = Encoding.UTF8.GetBytes(setting.securitykey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                       new Claim(ClaimTypes.Name, _user.Id.ToString())
                    }
                ),
                Expires = DateTime.Now.AddMinutes(20),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenkey), SecurityAlgorithms.HmacSha256)
            };
            var token = tokenhandler.CreateToken(tokenDescriptor);
            string finaltoken = tokenhandler.WriteToken(token);

            tokenResponse.JWTToken = finaltoken;
           tokenResponse.RefreshToken = tokenGenerator.GenerateToken(_user.Id);

            return Ok(tokenResponse);
        }

        [Route("Refresh")]
        [HttpPost]
        public IActionResult Refresh([FromBody] TokenResponse token)
        {

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = (JwtSecurityToken)tokenHandler.ReadToken(token.JWTToken);
            var username = securityToken.Claims.FirstOrDefault(c => c.Type == "unique_name")?.Value;

            var id = 0;
            try
            {
                id = int.Parse(username);
            }
            catch
            {
                id = 0;
            }

            var user = context.Users.Where(u =>(id != 0 && u.Id == id) || (u.Name == username)).FirstOrDefault();
            //var username = principal.Identity.Name;
            var _reftable = context.refreshtokens.FirstOrDefault(o => o.UserId == user.Id && o.RefreshToken == token.RefreshToken);
            if (_reftable == null)
            {
                return Unauthorized();
            }
            TokenResponse _result = Authenticate(user.Id, securityToken.Claims.ToArray());
            return Ok(_result);
        }


        [HttpPost("Register")]
        public IActionResult Register([FromBody] User value)
        {
            string result = string.Empty;
            try
            {
                var _emp = context.Users.FirstOrDefault(o => o.Id == value.Id);
                if (_emp != null)
                {
                    result = string.Empty;
                }
                else
                {
                    User _User = new User()
                    {
                        Name = value.Name,
                        Email = value.Email,
                        Id = value.Id,
                        Password = value.Password,
                        IsActive = false
                    };
                    context.Users.Add(_User);
                    context.SaveChanges();
                    result = "pass";
                }
            }
            catch (Exception ex)
            {
                result = string.Empty;
            }
            return Ok(JsonConvert.SerializeObject(new { keycode = string.Empty, result = result }));
        }

    }
}
