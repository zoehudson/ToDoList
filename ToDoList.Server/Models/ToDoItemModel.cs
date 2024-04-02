using static ToDoList.Server.Controllers.ToDoListController;

namespace ToDoList.Server.Models
{
    public class ToDoItemModel
    {
        public int Id { get; set; }
        public string Task { get; set; }
        public DateTime Deadline { get; set; }
        public bool IsCompleted { get; set; }
        public List<SubToDoModel> SubTodos { get; set; } = new List<SubToDoModel>();
        public string? MoreDetails { get; set; }
        public bool ShowDetails { get; set; }
    }
}
