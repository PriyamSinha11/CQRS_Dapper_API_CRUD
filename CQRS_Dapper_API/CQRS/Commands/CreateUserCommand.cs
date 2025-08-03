namespace CQRS_Dapper_API.CQRS.Commands
{
    public class CreateUserCommand
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
    }
}
