using Microsoft.AspNetCore.Mvc;
using solvexProject.Data;
using Microsoft.EntityFrameworkCore;
using solvexProject.Models;

namespace solvexProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly SolvexDbContext _solvexDbContext;
        public UsersController(SolvexDbContext solvexDbContext)
        {
            _solvexDbContext = solvexDbContext;
        }
        [HttpGet]
        public async Task<IActionResult>  GetAllUsers()
        {
            var users =  await _solvexDbContext.Users.ToListAsync();

            return Ok(users);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] User userRequest)
        {
            userRequest.Id = Guid.NewGuid(); 

            await _solvexDbContext.Users.AddAsync(userRequest);
            await _solvexDbContext.SaveChangesAsync();

            return Ok(userRequest);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetEmployee([FromRoute] Guid id)
        {
            var user = await _solvexDbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] Guid id, User updateUser)
        {
            var user = await _solvexDbContext.Users.FindAsync(id);
            if(user == null)
            {
                return NotFound();
            }

            user.Name = updateUser.Name;
            user.Email = updateUser.Email;
            user.Password = updateUser.Password;

            await _solvexDbContext.SaveChangesAsync();
            return Ok(user);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
        {
            var user = await _solvexDbContext.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            _solvexDbContext.Users.Remove(user);
            await _solvexDbContext.SaveChangesAsync();
            return Ok(user);
        }
    }
}
