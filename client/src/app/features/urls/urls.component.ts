import { Component, inject, OnInit } from '@angular/core';
import { UrlService } from '../../core/services/url.service';
import { UrlsFormComponent } from './urls-form/urls-form.component';
import { UrlsTableComponent } from './urls-table/urls-table.component';
import { AuthService } from '../../core/services/auth.service';
import { MatButton } from '@angular/material/button';

@Component({
  selector: 'app-urls',
  standalone: true,
  imports: [
    UrlsFormComponent,
    UrlsTableComponent,
    MatButton
  ],
  templateUrl: './urls.component.html',
  styleUrl: './urls.component.scss'
})
export class UrlsComponent implements OnInit {
  private urlService = inject(UrlService);
  public authService = inject(AuthService);

  public ngOnInit(): void {
    this.loadUrls();
  }

  private loadUrls(): void {
    this.urlService.getUrls();
  }
  
  public deleteAll(): void {
    this.urlService.deleteAll().subscribe({
      next: () => this.urlService.urls.set([])
    });
  }

  get areThereAnyLinks() {
    return this.urlService.urls().length > 0;
  }
}
