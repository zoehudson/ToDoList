using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoList.Server.Controllers;
using ToDoList.Server.Models;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;

namespace ToDoList.Server.Tests.Controllers
{
    [TestClass]
    public class ToDoListControllerTests
    {
        private ToDoListController _controller;

        [TestInitialize]
        public void Setup()
        {
            _controller = new ToDoListController();
        }

        [TestMethod]
        public void Get_ReturnsOkResultWithTodoList()
        {
            var now = DateTime.Now;
            // Arrange
            var expectedTodoList = new List<ToDoItemModel>
    {
        new ToDoItemModel { Id = 0, Task = "Task 1", Deadline = now.AddDays(1) },
        new ToDoItemModel { Id = 1, Task = "Task 2", Deadline = now.AddDays(2) }
    };
            _controller.Post(new ToDoCreationModel { Task = "Task 1", Deadline = now.AddDays(1) });
            _controller.Post(new ToDoCreationModel { Task = "Task 2", Deadline = now.AddDays(2) });


            // Act
            var actionResult = _controller.Get();


            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<IEnumerable<ToDoItemModel>>));
            var result = ((OkObjectResult)actionResult.Result).Value as IEnumerable<ToDoItemModel>;
            Assert.IsNotNull(result);
            foreach (var expectedItem in expectedTodoList)
            {
                var actualItem = result.FirstOrDefault(item => item.Id == expectedItem.Id);


                Assert.IsNotNull(actualItem);
                Assert.AreEqual(expectedItem.Task, actualItem.Task);
                Assert.AreEqual(expectedItem.Deadline, actualItem.Deadline);
            }
        }

        // Write similar tests for other controller actions...

        private void SetPrivateField(string fieldName, object value)
        {
            var field = _controller.GetType().GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
            field.SetValue(_controller, value);
        }
    }
}
