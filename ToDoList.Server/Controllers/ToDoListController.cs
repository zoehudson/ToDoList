using ToDoList.Server.Models;
using Microsoft.AspNetCore.Mvc;

namespace ToDoList.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToDoListController : ControllerBase
    {

        private static List<ToDoItemModel> _todoList = [];
        private static int _currentId = 0;
        [HttpGet]
        public ActionResult<IEnumerable<ToDoItemModel>> Get()
        {
            return Ok(_todoList);
        }

        [HttpPost]
        public IActionResult Post([FromBody] ToDoCreationModel toDoCreationModel)
        {
            var newTodo = new ToDoItemModel
            {
                Id = _currentId++,
                Deadline = toDoCreationModel.Deadline,
                MoreDetails = toDoCreationModel.MoreDetails,
                Task = toDoCreationModel.Task
            };
            _todoList.Add(newTodo);
            return CreatedAtAction(nameof(Get), new { id = newTodo.Id }, newTodo);
        }

        [HttpPost]
        [Route("{parentId}/subtodos")]
        public IActionResult AddSubTodo([FromBody] SubTodoCreationModel subTodoCreationModel, int parentId)
        {
            var todo = _todoList.FirstOrDefault();
            if (todo == null) {
                throw new Exception("Invalid TODO Id");
            }
            var newTodo = new SubToDoModel
            {
                ParentId = parentId,
                Id = _currentId++,
                Deadline = subTodoCreationModel.Deadline,
                MoreDetails = subTodoCreationModel.MoreDetails,
                Task = subTodoCreationModel.Task
            };
            todo.SubTodos.Add(newTodo);
            return CreatedAtAction(nameof(Get), new { id = newTodo.Id }, newTodo);
        }

    }
}
