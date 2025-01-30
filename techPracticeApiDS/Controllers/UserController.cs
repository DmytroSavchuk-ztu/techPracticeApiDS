using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using techPracticeApiDS.Models;
using techPracticeApiDS.Services;


namespace techPracticeApiDS.Controllers
{
    [ApiController]
    [Route("api/users")]
    [Authorize] // 🔒 Захищаємо доступ
    public class UserController : ControllerBase
    {
        private static List<User> users = AuthService.Users;


        [HttpGet]
        public IActionResult GetAllUsers() => Ok(users);

        [HttpPost]
        public IActionResult CreateUser([FromBody] User user)
        {
            user.Id = users.Count + 1;
            users.Add(user);
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetUserById(int id)
        {
            var user = users.FirstOrDefault(u => u.Id == id);
            return user is not null ? Ok(user) : NotFound();
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateUser(int id, [FromBody] User updatedUser)
        {
            var user = users.FirstOrDefault(u => u.Id == id);
            if (user is null) return NotFound();

            user.Name = updatedUser.Name;
            user.Email = updatedUser.Email;
            return Ok(user);
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteUser(int id)
        {
            var user = users.FirstOrDefault(u => u.Id == id);
            if (user is null) return NotFound();

            users.Remove(user);
            return NoContent();
        }
    }
}