import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiRoutes } from '../../environments/environment';
import { Book } from '../interfaces/book';
import { BookSubscription } from '../interfaces/bookSubscription';

@Injectable({
  providedIn: 'root'
})
export class BookSubscriptionService {

  constructor(public httpClient:HttpClient) {
 
  }

  getBooks():Observable<BookSubscription[]>{
    return this.httpClient.get<BookSubscription[]>(ApiRoutes.BookSubscriptionAPI);
  }

  getBookById(id:number):Observable<BookSubscription>{
    return this.httpClient.get<Book>(`${ApiRoutes.BookSubscriptionAPI}/${id}`);
  }

  subscribe(bookId:number){
    console.log(bookId);
    return this.httpClient.post(ApiRoutes.BookSubscriptionAPI,{BookId:bookId});
  }
}
