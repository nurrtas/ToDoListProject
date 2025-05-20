using MediatR;

namespace TodoList.BackApp.TodoEntities.Entities.EntityConfigurations.TodoApplication.Application.CommandHandlers
{
    public class CreateUserWithWelcomeTodoCommand : IRequest<bool>
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
