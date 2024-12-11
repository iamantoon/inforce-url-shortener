import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { Url } from '../../shared/models/url';

@Injectable({
  providedIn: 'root'
})
export class UrlService {
  private http = inject(HttpClient);
  public urls = signal<Url[]>([]);
  private baseUrl = 'http://localhost:5000/api/';

  public getUrls() {
    return this.http.get<Url[]>(this.baseUrl + 'urls').subscribe({
      next: urls => this.urls.set(urls)
    })
  }

  public addUrl(originalUrl: string) {
    return this.http.post<Url>(this.baseUrl + 'urls', {originalUrl});
  }

  public deleteUrl(id: number) {
    return this.http.delete(this.baseUrl + 'urls/' + id);
  }

  public deleteAll() {
    return this.http.delete(this.baseUrl + 'urls/all');
  }

  public getUrlDetails(id: number) {
    const razorPageUrl = 'http://localhost:5000/url/ShortUrlInfo/' + id;
    window.open(razorPageUrl);
  }
}
