import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UrlShortener } from 'src/app/services/url.service';
import { UrlDetail } from 'src/app/shared/urlDetail';

@Component({
  selector: 'app-more-info-url-view',
  templateUrl: './more-info-url-view.component.html',
  styleUrls: ['./more-info-url-view.component.css'],
})
export class MoreInfoUrlViewComponent implements OnInit {
  public url: UrlDetail = new UrlDetail();
  constructor(
    public urlsService: UrlShortener,
    private activatedRoute: ActivatedRoute
  ) {}

  ngOnInit() {
    let urlId = this.activatedRoute.snapshot.paramMap.get('id');
    this.urlsService.loadUrlById(+urlId!).subscribe((u) => {
      this.url = u;
    });
  }
}
