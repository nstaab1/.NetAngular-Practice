using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository repo;
        public AuthController(IAuthRepository repo)
        {
            this.repo = repo;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterdto) 
        {
            userForRegisterdto.Username = userForRegisterdto.Username.ToLower();

            if (await repo.UserExists(userForRegisterdto.Username)) {
                return BadRequest("Username already exists");
            }

            var userToCreate = new User {
                Username = userForRegisterdto.Username
            };

            var createdUser = await repo.Register(userToCreate, userForRegisterdto.Password);

            return StatusCode(201);
        }
    }
}