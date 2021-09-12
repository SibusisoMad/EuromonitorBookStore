import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';
import { ApiRoutes, environment } from '../../environments/environment';
import { Book } from '../interfaces/book';
import { BookSubscription } from '../interfaces/bookSubscription';

@Injectable({
  providedIn: 'root'
})
export class BooksService {

  constructor(public httpClient:HttpClient) {
 
  }

  getBooks():Observable<Book[]>{
    return this.httpClient.get<Book[]>(ApiRoutes.baseBookUrl);
  }

  getBookById(id:number):Observable<Book>{
    return this.httpClient.get<Book>(`${ApiRoutes.baseBookUrl}/${id}`);
  }

  subscribeBook(book:BookSubscription):Observable<any>{
    return this.httpClient.post<Book>(ApiRoutes.baseBookUrl, book);
  }
}
