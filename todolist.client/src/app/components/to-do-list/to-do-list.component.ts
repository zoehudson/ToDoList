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
  public currentDate: Date = new Date();
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
      this.newTodo.task = ''; 
      this.newTodo.moreDetails = '';
      this.newTodo.deadline = new Date();
    });
  }

  addSubTodo(todo: Todo) {
    const newSubTodo: SubTodoCreationModel = {
      task: this.subTodo.task,
      deadline: this.subTodo.deadline,
      moreDetails: this.subTodo.moreDetails
    };

    this.todoService.addSubTodo(todo.id, newSubTodo).subscribe((subTodo) => {
      this.fetchTodoList();
      this.subTodo = new SubTodoModel();
    });
  }

  newSubTodo(todo: Todo) {
    this.newTodo.task = ''; 
    this.newTodo.moreDetails = '';
    todo.subTodos.push(this.subTodo);
  }
  isOverdue(deadline: Date): boolean {
    return new Date(deadline) < this.currentDate;
  }

  updateTodo(todo: Todo) {
    this.todoService.updateTodo(todo).subscribe(() => {
      this.fetchTodoList();
    });
  }

  updateSubTodo(parentId: number, subTodoId: number, subTodo: SubTodoModel) {
    this.todoService.updateSubTodo(parentId, subTodoId, subTodo).subscribe(() => {
      this.fetchTodoList();
    });
  }

  updateMoreDetails(todo: Todo, moreDetails: string) {
    this.todoService.updateMoreDetails(todo.id, moreDetails).subscribe(() => {
      this.fetchTodoList();
    });
  }


  updateSubTodoDetails(todoId: number, subTodoId: number, moreDetails: string) {
    this.todoService.updateSubTodoDetails(todoId, subTodoId, moreDetails).subscribe(() => {
      this.fetchTodoList();
    });
  }

  toggleDetails(todo: Todo) {
    todo.showDetails = !todo.showDetails;
  }
  
  toggleSubTodoDetails(subTodo: SubTodoModel) {
    subTodo.showDetails = !subTodo.showDetails;
  }

  toggleAddSubTodo(todo: Todo) {
    todo.showAddSubTodo = !todo.showAddSubTodo;
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
    if (todo.subTodos) {
      const index = todo.subTodos.indexOf(subTodo);
      if (index !== -1) {
        todo.subTodos.splice(index, 1);
        this.todoService.deleteSubTodo(todo.id, subTodo.id).subscribe(
          () => {
          },
          (error) => {
            console.error('Error deleting subtodo:', error);
          }
        );
      }
    }
  }

  markSubTodoAsCompleted(parentId: number, subTodoId: number) {
    this.todoService.markSubTodoAsCompleted(parentId, subTodoId).subscribe(() => {
      this.fetchTodoList();
    });
  }

  markAsCompleted(id: number) {
    this.todoService.markAsCompleted(id).subscribe(() => {
      this.fetchTodoList();
    });
  }


}




