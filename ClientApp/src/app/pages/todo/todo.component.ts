import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TodoService } from '../../services/todo.service';
import { TodoItem } from '../../models/todo.model';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { RouterModule } from '@angular/router';

@Component({
  standalone: true,
  selector: 'app-todo',
  templateUrl: './todo.component.html',
  styleUrls: ['./todo.component.scss'],
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule,
    RouterModule 
  ]
})
export class TodoComponent implements OnInit {
  todos: TodoItem[] = [];

  constructor(private todoService: TodoService) {}

  ngOnInit(): void {
    this.todoService.getAll().subscribe({
      next: (data) => {
        this.todos = data;
      },
      error: (err) => {
        console.error('TODO verisi alınamadı:', err);
      }
    });
  }

  completeTodo(todo: TodoItem): void {
    todo.status = 'Tamamlandı';
    this.todoService.update(todo.id, todo).subscribe({
      next: () => {
        console.log('Todo güncellendi');
      },
      error: (err) => {
        console.error('Güncelleme hatası:', err);
      }
    });
  }

  deleteTodo(id: number): void {
    this.todoService.delete(id).subscribe({
      next: () => {
        this.todos = this.todos.filter(t => t.id !== id);
        console.log('Todo silindi');
      },
      error: (err) => {
        console.error('Silme hatası:', err);
      }
    });
  }
}
