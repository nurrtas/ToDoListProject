using MediatR;
using System.ComponentModel.DataAnnotations;
using TodoList.BackApp.TodoApplication.TodoEntities.Entities;

namespace TodoList.BackApp.TodoApplication.TodoEntities.ItemDtos

{

    public class GetAllTodoItemsRequest : IRequest<List<TodoItemEntity>>
    {

    }
}