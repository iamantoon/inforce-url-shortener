import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { User } from '../../shared/models/user';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private http = inject(HttpClient);
  public user = signal<User | null>(null);
  private baseUrl = 'http://localhost:5000/api/auth';
  
  public getCurrentUser() {
    return this.http.get<User | null>(this.baseUrl).subscribe({
      next: user => this.user.set(user)
    });
  }
}
