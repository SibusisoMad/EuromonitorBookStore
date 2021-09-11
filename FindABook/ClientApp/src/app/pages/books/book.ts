import { Component } from '@angular/core';
import { Book } from '../../interfaces/book';
import { BooksService } from '../../providers/books.service';

@Component({
  selector: 'book-list',
  templateUrl: 'book.html',
  styleUrls: ['./book.scss'],
})
export class BookPage {
  speakers: any[] = [];
  books:Book[] = [];

  constructor(public booksService:BooksService) {}

  ionViewDidEnter() {
      this.booksService.getBooks().subscribe((data:Book[])=>{
        data.forEach(item=>{
          this.books.push(item);
        })  
        
          console.log(data);
      });

  }
}
