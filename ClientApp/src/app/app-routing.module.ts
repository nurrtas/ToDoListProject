import { Routes } from '@angular/router';
import { AuthGuard } from './services/auth.guard';


export const routes: Routes = [
  {
    path: 'login',
    loadComponent: () => import('./pages/login/login.component').then(m => m.LoginComponent),
  },
  {
    path: 'register',
    loadComponent: () => import('./app/pages/register/register.component').then(m => m.RegisterComponent),
  },
  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full',
  },
  {
  path: 'todo',
  loadComponent: () => import('./pages/todo/todo.component').then(m => m.TodoComponent),
  canActivate: [AuthGuard],
},
{
  path: 'todo/ekle',
  loadComponent: () => import('./pages/todo/add-todo.component').then(m => m.AddTodoComponent),
  canActivate: [AuthGuard] 
},
{
  path: 'add-todo',
  loadComponent: () => import('./pages/todo/add-todo.component').then(m => m.AddTodoComponent),
  canActivate: [AuthGuard]
}


];
