using Microsoft.AspNetCore.Mvc;
using CQRS_Dapper_API.CQRS.Commands;
using CQRS_Dapper_API.CQRS.Queries;
using CQRS_Dapper_API.CQRS.Handlers;

namespace CQRS_Dapper_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserCommandHandler _commandHandler;
        private readonly UserQueryHandler _queryHandler;

        public UsersController(UserCommandHandler commandHandler, UserQueryHandler queryHandler)
        {
            _commandHandler = commandHandler;
            _queryHandler = queryHandler;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _queryHandler.Handle(new GetAllUsersQuery());
            return Ok(users);
            }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _queryHandler.Handle(new GetUserByIdQuery { Id = id });
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
        {
            var result = await _commandHandler.Handle(command);
            return result > 0 ? Ok("User created") : BadRequest("Failed to create user");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateUserCommand command)
        {
            if (id != command.Id)
                return BadRequest("ID mismatch");

            var result = await _commandHandler.Handle(command);
            return result > 0 ? Ok("User updated") : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _commandHandler.Handle(new DeleteUserCommand { Id = id });
            return result > 0 ? Ok("User deleted") : NotFound();
        }
    }
}
