import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Todo } from '../models/todo.model';



@Injectable({
  providedIn: 'root'
})
export class TodoService {
  private baseUrl = 'https://localhost:56626/api/Todo';

  constructor(private http: HttpClient) {}

  getAll(): Observable<Todo[]> {
    return this.http.get<Todo[]>(`${this.baseUrl}/GetAll`);
  }

  create(todo: Todo): Observable<Todo> {
    return this.http.post<Todo>(`${this.baseUrl}/Create`, todo);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/Delete/${id}`);
  }

  update(id: number, todo: Todo): Observable<Todo> {
    return this.http.put<Todo>(`${this.baseUrl}/Update/${id}`, todo);
  }
}

