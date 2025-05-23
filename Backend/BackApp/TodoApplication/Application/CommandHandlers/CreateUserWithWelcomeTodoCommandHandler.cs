using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TodoList.BackApp.TodoApplication.TodoEntities.Entities;

namespace TodoList.BackApp.TodoApplication.Application.CommandHandlers
{
    public class CreateUserWithWelcomeTodoCommandHandler : IRequestHandler<CreateUserWithWelcomeTodoCommand, bool>
    {
        private readonly BackApp.TodoEntities.Entities.Entities.ApplicationDbContext _context;
        private readonly UserManager<UserEntity> _userManager;

        public CreateUserWithWelcomeTodoCommandHandler(BackApp.TodoEntities.Entities.Entities.ApplicationDbContext context,UserManager<UserEntity> userManager)
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
                UserEntity user = new UserEntity
                {
                    UserName = request.Username,
                    Email = request.Email
                };

                var result = await _userManager.CreateAsync(user, request.Password);
                if (!result.Succeeded)
                    throw new Exception("Kullanıcı oluşturulamadı");

                // 2. Hoş geldiniz todo
                var todoItem = new TodoItemEntity
                {
                    Title = "Hoş geldiniz!",
                    Description = "İlk göreviniz burada.",
                    CreatedAt = DateTime.UtcNow,
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
