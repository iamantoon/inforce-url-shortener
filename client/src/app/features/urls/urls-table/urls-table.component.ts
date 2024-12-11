import { Component, inject } from '@angular/core';
import { UrlService } from '../../../core/services/url.service';
import { AuthService } from '../../../core/services/auth.service';
import { MatButton } from '@angular/material/button';

@Component({
  selector: 'app-urls-table',
  standalone: true,
  imports: [
    MatButton
  ],
  templateUrl: './urls-table.component.html',
  styleUrl: './urls-table.component.scss'
})
export class UrlsTableComponent {
  public urlService = inject(UrlService);
  public authService = inject(AuthService);

  public deleteUrl(id: number): void {
    this.urlService.deleteUrl(id).subscribe({
      next: () => {
        this.urlService.urls.update(urls => urls.filter(u => u.id !== id));
      }
    });
  }

  public getUrlDetails(id: number): void {
    this.urlService.getUrlDetails(id);
  }
}
