using Microsoft.AspNetCore.Mvc;


namespace techPracticeApiDS.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private static List<User> users = new List<User>
        {
            new User { Id = 1, Name = "Alice Johnson", Email = "alice@example.com" },
            new User { Id = 2, Name = "Bob Smith", Email = "bob@example.com" },
            new User { Id = 3, Name = "Charlie Davis", Email = "charlie@example.com" }
        };
        
        [HttpPost]
        public IActionResult CreateUser([FromBody] User user)
        {
            user.Id = users.Count + 1;
            users.Add(user);
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            return Ok(users);
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

    public record User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}