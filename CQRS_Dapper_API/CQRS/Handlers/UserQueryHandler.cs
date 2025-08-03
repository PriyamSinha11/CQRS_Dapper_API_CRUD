using CQRS_Dapper_API.CQRS.Queries;
using CQRS_Dapper_API.Data;
using CQRS_Dapper_API.Models;
using Dapper;

namespace CQRS_Dapper_API.CQRS.Handlers
{
    public class UserQueryHandler
    {
        private readonly DapperContext _context;
        public UserQueryHandler(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> Handle(GetAllUsersQuery query)
        {
            var sql = "SELECT * FROM Users";
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<User>(sql);
        }

        public async Task<User> Handle(GetUserByIdQuery query)
        {
            var sql = "SELECT * FROM Users WHERE Id = @Id";
            using var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<User>(sql, new { query.Id });
        }
    }
}
