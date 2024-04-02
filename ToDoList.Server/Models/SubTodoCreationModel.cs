namespace ToDoList.Server.Models
{
    public class SubTodoCreationModel
    {
        public string Task { get; set; }
        public DateTime Deadline { get; set; }
        public bool IsCompleted { get; set; }
        public string? MoreDetails { get; set; }
        public bool ShowDetails { get; set; }
    }
}
