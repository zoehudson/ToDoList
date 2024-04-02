using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using ToDoList.Server.Controllers;
using ToDoList.Server.Models;

namespace ToDoList.Server.Tests
{
    [TestClass]
    public class TodoListControllerTests
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
            var expectedTodoList = new List<ToDoItemModel>
            {
                new ToDoItemModel { Id = 1, Task = "Task 1", Deadline = DateTime.Now.AddDays(1) },
                new ToDoItemModel { Id = 2, Task = "Task 2", Deadline = DateTime.Now.AddDays(2) }
            };
            _controller.Post(new ToDoCreationModel { Task = "Task 1", Deadline = DateTime.Now.AddDays(1) });
            _controller.Post(new ToDoCreationModel { Task = "Task 2", Deadline = DateTime.Now.AddDays(2) });

            // Act
            var actionResult = _controller.Get();

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<IEnumerable<ToDoItemModel>>));
            var result = actionResult as ActionResult<IEnumerable<ToDoItemModel>>;
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Value.SequenceEqual(expectedTodoList));
        }

        [TestMethod]
        public void Post_AddsNewTodoAndReturnsCreatedAtActionResult()
        {
            // Arrange
            var todoToAdd = new ToDoCreationModel { Task = "New Task", Deadline = DateTime.Now.AddDays(3) };

            // Act
            var result = _controller.Post(todoToAdd) as CreatedAtActionResult;
            var addedTodo = result?.Value as ToDoItemModel;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(201, result.StatusCode);
            Assert.IsNotNull(addedTodo);
            Assert.AreEqual(todoToAdd.Task, addedTodo.Task);
            Assert.AreEqual(todoToAdd.Deadline, addedTodo.Deadline);
        }

        // Write similar tests for other controller methods...

    }
}
