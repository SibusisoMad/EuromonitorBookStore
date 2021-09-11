import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BookDetailPage } from './book-details';
import { BookDetailPageRoutingModule } from './book-details-routing.module';
import { IonicModule } from '@ionic/angular';

@NgModule({
  imports: [
    CommonModule,
    IonicModule,
    BookDetailPageRoutingModule
  ],
  declarations: [
    BookDetailPage,
  ]
})
export class BookDetailModule { }
