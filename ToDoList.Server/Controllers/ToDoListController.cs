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
            var todo = _todoList.FirstOrDefault(x => x.Id == parentId);
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

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var todo = _todoList.FirstOrDefault(x => x.Id == id);
            if (todo == null)
            {
                return NotFound();
            }
            _todoList.Remove(todo);
            return NoContent();
        }
        [HttpDelete("{todoId}/subtodos/{subTodoId}")]
        public IActionResult DeleteSubTodo(int todoId, int subTodoId)
        {
            var todo = _todoList.FirstOrDefault(x => x.Id == todoId);
            if (todo == null)
            {
                return NotFound("Todo not found");
            }

            var subTodo = todo.SubTodos.FirstOrDefault(x => x.Id == subTodoId);
            if (subTodo == null)
            {
                return NotFound("SubTodo not found");
            }

            todo.SubTodos.Remove(subTodo);
            return NoContent();
        }


        [HttpPut("{id}/complete")]
        public IActionResult MarkAsCompleted(int id)
        {
            var todo = _todoList.FirstOrDefault(x => x.Id == id);
            if (todo == null)
            {
                return NotFound();
            }
            todo.IsCompleted = true;
            todo.SubTodos.ForEach(x => x.IsCompleted = true);
            return NoContent();
        }

        [HttpPut("{parentId}/subtodos/{subTodoId}/completesubtodo")]
        public IActionResult MarkSubTodoAsCompleted(int parentId, int subTodoId)
        {
            var todo = _todoList.FirstOrDefault(t => t.Id == parentId);
            if (todo == null)
            {
                return NotFound("Parent TODO not found");
            }

            var subTodo = todo.SubTodos.FirstOrDefault(st => st.Id == subTodoId);
            if (subTodo == null)
            {
                return NotFound("Sub TODO not found");
            }

            subTodo.IsCompleted = true;
            bool allSubTodosComplete = todo.SubTodos.All(st => st.IsCompleted);
            if (allSubTodosComplete)
            {
                todo.IsCompleted = true;
            }

            return NoContent();
        }


    }
}
