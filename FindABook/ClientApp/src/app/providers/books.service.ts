import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';
import { ApiRoutes, environment } from '../../environments/environment';
import { Book } from '../interfaces/book';

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

  postBook(book:Book):Observable<any>{
    return this.httpClient.post<Book>(ApiRoutes.baseBookUrl, book);
  }
}
