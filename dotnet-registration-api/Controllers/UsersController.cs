using dotnet_registration_api.Data.Entities;
using dotnet_registration_api.Data.Models;
using dotnet_registration_api.Helpers;
using dotnet_registration_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_registration_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;
        public UsersController(UserService userService)
        {
            _userService = userService;
        }
        [HttpPost("login")]
        public async Task<ActionResult<User>> Login([FromBody]LoginRequest model)
        {
            throw new NotImplementedException();
        }
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register([FromBody]RegisterRequest model)
        {
            if (string.IsNullOrEmpty(model.Username) && string.IsNullOrEmpty(model.Password)) 
            {
                return BadRequest();
            }
            var user = await _userService.Register(model);
            return Ok(user);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            var users= await _userService.GetAll();
            return Ok(users);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetById(int id)
        {
           var user= await _userService.GetById(id);
            if (user is null) 
            {
                return NotFound();
            }
           return Ok(user);

        }
        [HttpPut("{id}")]
        public async Task<ActionResult<User>> Update(int id, [FromBody]UpdateRequest model)
        {
            var user = await _userService.GetById(id);
            if (user is null) 
            {
                return NotFound();
            }

            try
            {
                return Ok(await _userService.Update(id, model));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var user = await _userService.GetById(id);
            if (user is null)
            {
                return NotFound();
            }
            await _userService.Delete(id);
            return Ok();
        }
    }
}
