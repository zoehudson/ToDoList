import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Todo } from '../../models/todoModel';
import { SubTodoModel } from '../../models/subTodoModel';
import { TodoService } from '../../services/todo.service';
import { TodoCreationModel } from '../../models/todoCreationModel';
import { SubTodoCreationModel } from '../../models/subTodoCreationModel';

@Component({
  selector: 'app-to-do-list',
  templateUrl: './to-do-list.component.html',
  styleUrl: './to-do-list.component.css'
})
export class ToDoListComponent implements OnInit{
  public todoList: Todo[] = [];
  public newTodo: TodoCreationModel = new TodoCreationModel();
  public subTodo: SubTodoModel = new SubTodoModel();
  constructor(private todoService: TodoService) { }

  ngOnInit() {
    this.fetchTodoList();
  }

  fetchTodoList() {
    this.todoService.getTodos().subscribe(
      (todoList: Todo[]) => {
      this.todoList = todoList;
      },
      (error: any) => {
        console.error(error);
      })
  }
  addTodo() {
    this.todoService.addTodo(this.newTodo).subscribe((newTodo) => {
      this.fetchTodoList();
      this.newTodo.task = ''; // Reset task input
      this.newTodo.moreDetails = '';
    });
  }

  addSubTodo(todo: Todo) {
    // Create a new SubTodoCreationModel instance with appropriate values
    const newSubTodo: SubTodoCreationModel = {
      task: this.subTodo.task,
      deadline: this.subTodo.deadline,
      moreDetails: this.subTodo.moreDetails
    };

    // Call the service method to add the new subtodo
    this.todoService.addSubTodo(todo.id, newSubTodo).subscribe((subTodo) => {
      // If successful, update the todo list and reset input fields
      this.fetchTodoList();
      this.subTodo = new SubTodoModel(); // Reset subTodo object or input fields
    });
  }

  newSubTodo(todo: Todo) {
    this.newTodo.task = ''; // Reset task input
    this.newTodo.moreDetails = '';
    todo.subTodos.push(this.subTodo);
  }

  updateTodo(todo: Todo) {
    this.todoService.updateTodo(todo).subscribe(() => {
      // Optional: You can refresh the todo list if needed
      // this.fetchTodoList();
    });
  }

  updateSubTodo(subTodo: SubTodoModel) {
    this.todoService.updateSubTodo(subTodo).subscribe(() => {
      // Optional: You can refresh the todo list if needed
      // this.fetchTodoList();
    });
  }
  toggleDetails(todo: Todo) {
    todo.showDetails = !todo.showDetails;
  }

  deleteTodo(id: number) {
    this.todoService.deleteTodo(id).subscribe(
      () => {
        this.todoList = this.todoList.filter(todo => todo.id !== id);
      },
      (error) => {
        console.error('Error deleting todo:', error);
      }
    );
  }
  deleteSubTodo(todo: Todo, subTodo: SubTodoModel) {
    if (todo.subTodos) { // Ensure subTodos is not undefined
      const index = todo.subTodos.indexOf(subTodo);
      if (index !== -1) {
        todo.subTodos.splice(index, 1);
      }
    }
  }
  markSubTodoAsCompleted(subTodoModel: SubTodoModel) {
    this.todoService.markSubTodoAsCompleted(subTodoModel.id).subscribe(() => {
      // If necessary, update the todoList after marking sub TODO as completed
      this.fetchTodoList();
    });
  }

  markAsCompleted(id: number) {
    this.todoService.markAsCompleted(id).subscribe(() => {
      this.fetchTodoList();
    });
  }


}




