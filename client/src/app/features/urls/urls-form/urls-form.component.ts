import { Component, inject } from '@angular/core';
import { UrlService } from '../../../core/services/url.service';
import { FormControl, ReactiveFormsModule, Validators } from '@angular/forms';
import { TextInputComponent } from "../../../shared/components/text-input/text-input.component";
import { SnackbarService } from '../../../core/services/snackbar.service';
import { MatButton } from '@angular/material/button';

@Component({
  selector: 'app-urls-form',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    TextInputComponent,
    MatButton
],
  templateUrl: './urls-form.component.html',
  styleUrl: './urls-form.component.scss'
})
export class UrlsFormComponent {
  private urlService = inject(UrlService);
  private snackbar = inject(SnackbarService);
  public addUrlControl: FormControl = new FormControl('', 
    [Validators.required, Validators.pattern(/^(ftp|http|https):\/\/[^ "]+$/)]);

  public addUrl(): void {
    const url = this.addUrlControl.value;
    this.urlService.addUrl(url).subscribe({
      next: newUrl => {
        this.urlService.urls.update(urls => [...urls, newUrl]);
        this.snackbar.success('Link was added');
        this.resetInput();
      },
      error: (error) => {
        this.snackbar.error(error.message);
      }
    });
  }

  private resetInput(): void {
    this.addUrlControl.setValue('');
    this.addUrlControl.reset();
  }
}
