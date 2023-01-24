import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, map, Observable, subscribeOn, tap } from 'rxjs';
import { LoginRequest, LoginResult } from '../shared/loginResult';
import { UrlDetail } from '../shared/urlDetail';
import { User } from '../shared/User';

@Injectable()
export class UrlShortener {
  private _code$: BehaviorSubject<string> = new BehaviorSubject('');
  public code$: Observable<string> = this._code$.asObservable();
  private _longUrl$: BehaviorSubject<string> = new BehaviorSubject('');
  public longUrl$: Observable<string> = this._longUrl$.asObservable();
  constructor(private http: HttpClient) {}

  public urls: UrlDetail[] = [];

  public url: UrlDetail = {
    id: 0,
    longUrl: '',
    code: '',
    createdDate: new Date(),
    user: new User()
  };

  public token = '';
  public expiration = new Date();

  public newUser: User = new User();

  public code = '';
  public originalUrl = '';

  get loginRequired(): boolean {
    return this.token.length === 0 || this.expiration > new Date();
  }

  loadUrls(): Observable<void> {
    return this.http.get<[]>('/api/url').pipe(
      map((data) => {
        this.urls = data;
      })
    );
  }

  loadUrlById(id:number) : Observable<UrlDetail>{
    return this.http.get<UrlDetail>('api/url/GetUrlById/' + id).pipe(
      tap(_=>console.log(`get url id=${id}`))
      );    
  }

  getShortUrl(url: UrlDetail) {
    const headers = new HttpHeaders().set(
      'Authorization',
      `Bearer ${this.token}`
    );
    console.log('from service before post', url);
    return this.http.post<any>('/api/url/Test', url, { headers: headers }).pipe(
      tap((data) => {
        console.log('from service after post', data);
        this._code$.next(data);
        this._code$.subscribe((x) => console.log(x));
      })
    );
  }

  redirectToOriginal(code: string) {
    console.log('from service2 before get', code);
    return this.http.get<any>(`/api/url/RedirectToOriginal/${code}`).pipe(
      map((data) => {
        console.log('from service2 after get', data);
        this._longUrl$.next(data);
        this._longUrl$.subscribe((x) => console.log(x));
        window.open(data.longUrl);
      })
    );
  }

  login(creds: LoginRequest) {
    return this.http.post<LoginResult>('/api/account/CreateToken', creds).pipe(
      map((data) => {
        this.token = data.token;
        this.expiration = data.expiration;
      })
    );
  }

  registration(user: User) {
    return this.http.post<User>('/api/account/registration', user).pipe(
      map(() => {
        this.newUser = new User();
      })
    );
  }
}
