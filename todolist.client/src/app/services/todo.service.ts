import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Todo } from '../models/todoModel';
import { SubTodoModel } from '../models/subTodoModel';
import { TodoCreationModel } from '../models/todoCreationModel';
import { SubTodoCreationModel } from '../models/subTodoCreationModel';

@Injectable({
  providedIn: 'root'
})
export class TodoService {

  constructor(private http: HttpClient) { }

  getTodos(): Observable<Todo[]> {
    return this.http.get<Todo[]>('https://localhost:7078/api/todolist');

  }
  addTodo(todo: TodoCreationModel): Observable<Todo> {
    return this.http.post<Todo>('https://localhost:7078/api/todolist', todo);
  }
  addSubTodo(parentId: number, subTodo: SubTodoCreationModel): Observable<SubTodoModel> {
    return this.http.post<SubTodoModel>(`https://localhost:7078/api/todolist/${parentId}/subtodos`, subTodo);
  }

  updateTodo(todo: Todo): Observable<void> {
    return this.http.put<void>(`https://localhost:7078/api/todolist/${todo.id}`, todo);
  }

  updateSubTodo(subTodo: SubTodoModel): Observable<void> {
    return this.http.put<void>(`https://localhost:7078/api/subtodos/${subTodo.id}`, subTodo);
  }

  deleteTodo(id: number): Observable<void> {
    return this.http.delete<void>(`https://localhost:7078/api/todolist${id}`);
  }

  markAsCompleted(id: number): Observable<void> {
    return this.http.put<void>(`https://localhost:7078/api/todolist${id}/complete`, null);
  }

  markSubTodoAsCompleted(subTodoId: number): Observable<void> {
    return this.http.put<void>(`https://localhost:7078/api/subtodos/${subTodoId}/complete`, null);
  }

}
