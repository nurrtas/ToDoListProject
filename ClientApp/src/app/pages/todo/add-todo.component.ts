import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { TodoService } from '../../services/todo.service';
import { TodoItem } from '../../models/todo.model';

import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-add-todo',
  standalone: true,
  templateUrl: './add-todo.component.html',
  styleUrls: ['./add-todo.component.scss'],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule
  ]
})
export class AddTodoComponent {
  todoForm: FormGroup;

  constructor(private fb: FormBuilder, private todoService: TodoService) {
    this.todoForm = this.fb.group({
      title: ['', Validators.required],
      description: ['']
    });
  }

  onSubmit(): void {
    if (this.todoForm.valid) {
      const newTodo: TodoItem = {
        id: 0, // backend bunu değiştirecektir
        title: this.todoForm.value.title,
        description: this.todoForm.value.description,
        status: 'Bekliyor'
      };

      this.todoService.create(newTodo).subscribe({
        next: () => {
          alert('Yeni görev eklendi!');
          this.todoForm.reset();
        },
        error: (err) => {
          console.error('Görev eklenemedi:', err);
        }
      });
    }
  }
}
