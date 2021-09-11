import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IonicModule } from '@ionic/angular';
import { BookPage } from './book';
import { BookPageRoutingModule } from './book-routing.module';

@NgModule({
  imports: [
    CommonModule,
    IonicModule,
    BookPageRoutingModule
  ],
  declarations: [BookPage],
})
export class BookModule {}
