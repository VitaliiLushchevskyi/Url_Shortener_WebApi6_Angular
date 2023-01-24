import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UrlShortener } from 'src/app/services/url.service';

@Component({
  selector: 'app-table-urls-view',
  templateUrl: './table-urls-view.component.html',
  styleUrls: ['./table-urls-view.component.css']
})
export default class TableUrlsViewComponent implements OnInit {

  constructor(public urlsService: UrlShortener,public router:Router) {}

  page: number = 1;
  ngOnInit(): void {
    this.urlsService.loadUrls().subscribe(() => {});
  }

  urlById(id:number){
    this.urlsService.loadUrlById(id).subscribe({
      next: ()=> {
        this.router.navigate(['/moreInfo'])
      }
    })
  }
}
