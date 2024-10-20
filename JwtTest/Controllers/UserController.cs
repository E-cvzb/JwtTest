using JwtTest.Context;

using JwtTest.Entities;
using JwtTest.Jwt;
using JwtTest.Requast;
using JwtTest.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace JwtTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly JwtTestDbContext _context;
        public UserController(JwtTestDbContext context)
        {
            _context = context;
        }



        [HttpPost("SingUp")]
        public async Task<IActionResult> CreateUser(UserRequest request)
        {

            var user = new UserEntitiy
            {
                Email = request.Email,
                Password = request.Password
            };
           
            await _context.SaveChangesAsync();
            return Ok(user);


        }
        [HttpPost("SingIn")]
        public async Task<IActionResult> SingIn(UserRequest request)
        {
            var user = _context.User.FirstOrDefault(x=>x.Email==request.Email);
            if(user is not null)
            {
                return BadRequest(new { message = "Hatalı kullanıcı adı veya şifre " });
            }

            if (user.Password is null)
            {
                return BadRequest(new { message = "Hatalı kullanıc adı veya şifer" });

            }
            var configuration = HttpContext.RequestServices.GetRequiredService<IConfiguration>();

            var token = JwtHelper.GenerateJwt(new JwtDto
            {

                Id=user.Id,
                Email=user.Email,
                UserType=user.UserType,
                SecretKey = configuration["Jwt:SecretKey"],
                Audience = configuration["Jwt:Audince"],
                Issuer = configuration["Jwt,Issuer"],
                ExpireMinutes = int.Parse(configuration["Jwt:ExpireMinutes"])



            });

            return Ok(new LoginResponse
            {
                Message="Giriş başarı ile yapıldı ",
                Token=token
            });
        }


    }
}
