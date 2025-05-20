using MediatR;
using Microsoft.AspNetCore.Identity;

namespace TodoList.BackApp.TodoEntities.Entities.EntityConfigurations.TodoApplication.Application.CommandHandlers
{
    public class CreateUserWithWelcomeTodoCommandHandler : IRequestHandler<CreateUserWithWelcomeTodoCommand, bool>
    {
        private readonly Entities1.ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public CreateUserWithWelcomeTodoCommandHandler(Entities1.ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<bool> Handle(CreateUserWithWelcomeTodoCommand request, CancellationToken cancellationToken)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // 1. Kullanıcı oluştur
                var user = new AppUser
                {
                    UserName = request.Username,
                    Email = request.Email
                };

                var result = await _userManager.CreateAsync(user, request.Password);
                if (!result.Succeeded)
                    throw new Exception("Kullanıcı oluşturulamadı");

                // 2. Hoş geldiniz todo
                var todoItem = new TodoItem
                {
                    Title = "Hoş geldiniz!",
                    Description = "İlk göreviniz burada.",
                    CreatedAt = DateTime.UtcNow,
                    UserId = user.Id // eğer UserId yoksa TodoItem modeline eklenmeli
                };

                _context.TodoItems.Add(todoItem);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();

                var existingUser = await _userManager.FindByNameAsync(request.Username);
                if (existingUser != null)
                    await _userManager.DeleteAsync(existingUser);

                return false;
            }
        }
    }
}
