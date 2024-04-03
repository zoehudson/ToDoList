using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoList.Server.Controllers;
using ToDoList.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

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
            // Arrange
            var now = DateTime.Now;
            var expectedTodoList = new List<ToDoItemModel>
            {
                new ToDoItemModel { Id = 0, Task = "Task 1", Deadline = now.AddDays(1) },
                new ToDoItemModel { Id = 1, Task = "Task 2", Deadline = now.AddDays(2) }
            };
            foreach (var todo in expectedTodoList)
            {
                _controller.Post(new ToDoCreationModel { Task = todo.Task, Deadline = todo.Deadline });
            }

            // Act
            var actionResult = _controller.Get();

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<IEnumerable<ToDoItemModel>>));
            var result = ((OkObjectResult)actionResult.Result).Value as IEnumerable<ToDoItemModel>;
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedTodoList.Count, result.Count());
            foreach (var expectedItem in expectedTodoList)
            {
                var actualItem = result.FirstOrDefault(item => item.Id == expectedItem.Id);
                Assert.IsNotNull(actualItem);
                Assert.AreEqual(expectedItem.Task, actualItem.Task);
                Assert.AreEqual(expectedItem.Deadline, actualItem.Deadline);
            }
        }

        [TestMethod]
        public void Post_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var todoCreationModel = new ToDoCreationModel { Task = "New Task", Deadline = DateTime.Now.AddDays(1) };

            // Act
            var actionResult = _controller.Post(todoCreationModel);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(CreatedAtActionResult));
        }

        [TestMethod]
        public void Delete_ReturnsNoContentResult()
        {
            // Arrange
            _controller.Post(new ToDoCreationModel { Task = "Task 1", Deadline = DateTime.Now.AddDays(1) });
            var todoList = ((OkObjectResult)_controller.Get().Result).Value as IEnumerable<ToDoItemModel>;
            var idToDelete = todoList.First().Id;

            // Act
            var actionResult = _controller.Delete(idToDelete);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult));
        }

        [TestMethod]
        public void DeleteSubTodo_ReturnsNoContentResult()
        {
            // Arrange
            _controller.Post(new ToDoCreationModel { Task = "Task 1", Deadline = DateTime.Now.AddDays(1) });
            var todoList = ((OkObjectResult)_controller.Get().Result).Value as IEnumerable<ToDoItemModel>;
            var todoId = todoList.First().Id;
            _controller.AddSubTodo(new SubTodoCreationModel { Task = "SubTask 1", Deadline = DateTime.Now.AddDays(1) }, todoId);
            var subTodoList = todoList.First().SubTodos;
            var subTodoId = subTodoList.First().Id;

            // Act
            var actionResult = _controller.DeleteSubTodo(todoId, subTodoId);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult));
        }

        [TestMethod]
        public void UpdateTodo_ReturnsNoContentResult()
        {
            // Arrange
            _controller.Post(new ToDoCreationModel { Task = "Task 1", Deadline = DateTime.Now.AddDays(1) });
            var todoList = ((OkObjectResult)_controller.Get().Result).Value as IEnumerable<ToDoItemModel>;
            var todoId = todoList.First().Id;
            var updatedTodo = new ToDoItemModel { Task = "Updated Task", Deadline = DateTime.Now.AddDays(2) };

            // Act
            var actionResult = _controller.UpdateTodo(todoId, updatedTodo);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult));
        }

        [TestMethod]
        public void UpdateSubTodo_ReturnsNoContentResult()
        {
            // Arrange
            _controller.Post(new ToDoCreationModel { Task = "Task 1", Deadline = DateTime.Now.AddDays(1) });
            var todoList = ((OkObjectResult)_controller.Get().Result).Value as IEnumerable<ToDoItemModel>;
            var todoId = todoList.First().Id;
            _controller.AddSubTodo(new SubTodoCreationModel { Task = "SubTask 1", Deadline = DateTime.Now.AddDays(1) }, todoId);
            var subTodoList = todoList.First().SubTodos;
            var subTodoId = subTodoList.First().Id;
            var updatedSubTodo = new SubToDoModel { Task = "Updated SubTask", Deadline = DateTime.Now.AddDays(2) };

            // Act
            var actionResult = _controller.UpdateSubTodo(todoId, subTodoId, updatedSubTodo);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult));
        }

        [TestMethod]
        public void UpdateTodoMoreDetails_ReturnsNoContentResult()
        {
            // Arrange
            _controller.Post(new ToDoCreationModel { Task = "Task 1", Deadline = DateTime.Now.AddDays(1) });
            var todoList = ((OkObjectResult)_controller.Get().Result).Value as IEnumerable<ToDoItemModel>;
            var todoId = todoList.First().Id;
            var moreDetailsUpdateRequest = new MoreDetailsUpdateRequest { MoreDetails = "Updated more details" };

            // Act
            var actionResult = _controller.UpdateTodoMoreDetails(todoId, moreDetailsUpdateRequest);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult));
        }

        [TestMethod]
        public void UpdateSubTodoDetails_ReturnsNoContentResult()
        {
            // Arrange
            _controller.Post(new ToDoCreationModel { Task = "Task 1", Deadline = DateTime.Now.AddDays(1) });
            var todoList = ((OkObjectResult)_controller.Get().Result).Value as IEnumerable<ToDoItemModel>;
            var todoId = todoList.First().Id;
            _controller.AddSubTodo(new SubTodoCreationModel { Task = "SubTask 1", Deadline = DateTime.Now.AddDays(1) }, todoId);
            var subTodoList = todoList.First().SubTodos;
            var subTodoId = subTodoList.First().Id;
            var moreDetailsUpdateRequest = new MoreDetailsUpdateRequest { MoreDetails = "Updated more details" };

            // Act
            var actionResult = _controller.UpdateSubTodoDetails(moreDetailsUpdateRequest, todoId, subTodoId);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult));
        }

        // Add tests for other controller actions...

        private void SetPrivateField(string fieldName, object value)
        {
            var field = _controller.GetType().GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
            field.SetValue(_controller, value);
        }
    }
}
