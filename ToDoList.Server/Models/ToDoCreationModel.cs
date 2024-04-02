using static ToDoList.Server.Controllers.ToDoListController;

namespace ToDoList.Server.Models
{
    public class ToDoCreationModel
    {
        public string Task { get; set; }
        public DateTime Deadline { get; set; }
        public string MoreDetails { get; set; }
    }
}
