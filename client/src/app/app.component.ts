import { Component, inject, OnInit } from '@angular/core';
import { UrlsComponent } from "./features/urls/urls.component";
import { AuthService } from './core/services/auth.service';
import { NavbarComponent } from "./layout/navbar/navbar.component";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [UrlsComponent, NavbarComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit {
  private authService = inject(AuthService);

  public ngOnInit(): void {
    this.authService.getCurrentUser();
  }
}
