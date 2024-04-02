using static ToDoList.Server.Controllers.ToDoListController;

namespace ToDoList.Server.Models
{
    public class SubToDoModel
    {
        public int Id { get; set; }
        public int ParentId {  get; set; }
        public string Task { get; set; }
        public DateTime Deadline { get; set; }
        public bool IsCompleted { get; set; }
        public string? MoreDetails { get; set; }
        public bool ShowDetails { get; set; }
    }
}
