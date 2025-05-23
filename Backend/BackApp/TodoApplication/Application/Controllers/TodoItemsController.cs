using MediatR;
using Microsoft.AspNetCore.Mvc;
using TodoList.BackApp.TodoApplication.Application.CommandHandlers;
using TodoList.BackApp.TodoApplication.Application.QueryHandlers;
using TodoList.BackApp.TodoApplication.TodoEntities.Entities;
using TodoList.BackApp.TodoApplication.TodoEntities.ItemDtos;
using TodoList.BackApp.TodoApplication.TodoEntities.Repositories;

[Route("Api/Todo")]
[ApiController]
public class TodoItemsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ITodoItemRepository _todoItemRepository;

    public TodoItemsController(IMediator mediator, ITodoItemRepository todoItemRepository)
    {
        _mediator = mediator;
        _todoItemRepository = todoItemRepository;
    }

    [HttpPost("Create")]
    public async Task<ActionResult<TodoItemEntity>> Create([FromBody] CreateTodoItemCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpGet("GetById{id}")]
    public async Task<ActionResult<TodoItemEntity?>> GetTodoItem(int id)
    {
        return await _todoItemRepository.GetAsync(id);
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllTodoItemsRequest());


        return Ok(result);
    }

    [HttpPut("Update{id}")]
    public async Task<IActionResult> PutTodoItem(int id, [FromBody] TodoItemEntity todoItem)
    {
        if (id != todoItem.Id)
            return BadRequest();

        await _todoItemRepository.UpdateAsync(todoItem);
        return NoContent();
    }

    [HttpDelete("Delete{id}")]
    public async Task<ActionResult> DeleteTodoItem(int id)
    {
        var todoItem = await _todoItemRepository.GetAsync(id);
        if (todoItem == null)
            return NotFound();

        await _todoItemRepository.DeleteAsync(id);
        return NoContent();
    }
}
