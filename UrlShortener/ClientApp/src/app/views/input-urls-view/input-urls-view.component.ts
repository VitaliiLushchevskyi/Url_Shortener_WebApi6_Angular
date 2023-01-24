import { Component, Inject, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { UrlShortener } from 'src/app/services/url.service';
import { UrlDetail } from 'src/app/shared/urlDetail';

@Component({
  selector: 'app-inputUrl',
  templateUrl: 'input-urls-view.component.html',
  styleUrls: ['input-urls-view.component.css'],
})
export class InputUrlsViewComponent {
  constructor(public urlsService: UrlShortener) {}
  public code$: Observable<string> = this.urlsService.code$;
  public longUrl$: Observable<string> = this.urlsService.longUrl$;
  public url: UrlDetail = this.urlsService.url;
  public code: string = this.urlsService.code;
  public ErrorMessage = '';

  onInputUrl() {
    this.urlsService.getShortUrl(this.url).subscribe({
      next: () => {},
      error: (err: any) => {
        this.ErrorMessage = 'Failed to get short url';
      },
      complete: () => {},
    });
  }
  onRedirectUrl(code: string) {
    this.urlsService.redirectToOriginal(code).subscribe({
      next: () => {},
      error: (err: any) => {
        this.ErrorMessage = 'Failed to redirect';
      },
      complete: () => {},
    });
  }
}
