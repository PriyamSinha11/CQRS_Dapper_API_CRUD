using CQRS_Dapper_API.CQRS.Commands;
using CQRS_Dapper_API.Data;
using Dapper;

namespace CQRS_Dapper_API.CQRS.Handlers
{
    public class UserCommandHandler
    {
        private readonly DapperContext _context;
        public UserCommandHandler(DapperContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateUserCommand command)
        {
            var sql = "INSERT INTO Users (Name, Email, Age) VALUES (@Name, @Email, @Age)";
            using var connection = _context.CreateConnection();
            return await connection.ExecuteAsync(sql, command);
        }

        public async Task<int> Handle(UpdateUserCommand command)
        {
            var sql = "UPDATE Users SET Name = @Name, Email = @Email, Age = @Age WHERE Id = @Id";
            using var connection = _context.CreateConnection();
            return await connection.ExecuteAsync(sql, command);
        }

        public async Task<int> Handle(DeleteUserCommand command)
        {
            var sql = "DELETE FROM Users WHERE Id = @Id";
            using var connection = _context.CreateConnection();
            return await connection.ExecuteAsync(sql, command);
        }
    }
}
